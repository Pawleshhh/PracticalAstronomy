module PracticalAstronomy.Coordinates

open System
open MathHelper
open MatrixHelper
open Time
open CoordinateSystemTypes

let internal horizontalHourAngleMatrix latitude =
    array2D [ [ -sinD latitude; 0.0; cosD latitude ]
              [       0.0     ; -1 ;      0.0      ] 
              [  cosD latitude; 0.0; sinD latitude ] ]

let internal equatorialHourAngleMatrix (siderealTime : TimeSpan) =
    let st = siderealTime.TotalHours * 15.0
    array2D [ [ cosD st;  sinD st; 0.0]
              [ sinD st; -cosD st; 0.0] 
              [   0.0  ;   0.0   ; 1.0] ]

let internal eclipticToEquatorialMatrix mo =
    array2D [ [ 1.0;   0.0  ;   0.0   ]
              [ 0.0; cosD mo; -sinD mo] 
              [ 0.0; sinD mo;  cosD mo] ]

let internal equatorialToEclipticMatrix mo =
    array2D [ [ 1.0;   0.0   ;   0.0  ]
              [ 0.0;  cosD mo; sinD mo] 
              [ 0.0; -sinD mo; cosD mo] ]

let internal equatorialToGalacticMatrix =
    array2D [ [ -0.066_9887; -0.872_7558; -0.483_5389 ] 
              [  0.492_7285; -0.450_3470;  0.744_5846 ] 
              [ -0.867_6008; -0.188_3746;  0.460_1998 ] ]

let internal galacticToEquatorialMatrix =
    array2D [ [ -0.066_9888;  0.492_7285; -0.867_6008 ] 
              [ -0.872_7557; -0.450_3469; -0.188_3746 ] 
              [ -0.483_5389;  0.744_5846;  0.460_1998 ] ]

let rec internal convMatrices (conversion : CoordConversion) =
    match conversion with
    | HaToEq    st           -> [ equatorialHourAngleMatrix st ]
    | HaToHor   lat          -> [ horizontalHourAngleMatrix lat ]
    | HaToEcl  (st, mo)      -> convMatrices (HaToEq(st)) @ convMatrices (EqToEcl(mo))
    | HaToGal   st           -> convMatrices (HaToEq(st)) @ convMatrices (EqToGal)
    | EqToHa    st           -> [ equatorialHourAngleMatrix st ]
    | EqToHor  (st, lat)     -> convMatrices (EqToHa(st)) @ convMatrices (HaToHor(lat))
    | EqToEcl   mo           -> [ equatorialToEclipticMatrix mo ]
    | EqToGal                -> [ equatorialToGalacticMatrix ]
    | HorToHa   lat          -> [ horizontalHourAngleMatrix lat ]
    | HorToEq  (st, lat)     -> convMatrices (HorToHa(lat)) @ convMatrices (HaToEq(st)) 
    | HorToEcl (st, lat, mo) -> convMatrices (HorToEq(st, lat)) @ convMatrices (EqToEcl(mo))
    | HorToGal (st, lat)     -> convMatrices (HorToEq(st, lat)) @ convMatrices (EqToGal)
    | EclToEq   mo           -> [ eclipticToEquatorialMatrix mo ]
    | EclToHa  (mo, st)      -> convMatrices (EclToEq(mo)) @ convMatrices (EqToHa(st))
    | EclToHor (mo, st, lat) -> convMatrices (EclToEq(mo)) @ convMatrices (EqToHor(st, lat))
    | EclToGal  mo           -> convMatrices (EclToEq(mo)) @ convMatrices (EqToGal)
    | GalToEq                -> [ galacticToEquatorialMatrix ]
    | GalToHa   st           -> convMatrices (GalToEq) @ convMatrices (EqToHa(st))
    | GalToHor (st, lat)     -> convMatrices (GalToEq) @ convMatrices (EqToHor(st, lat))
    | GalToEcl  mo           -> convMatrices (GalToEq) @ convMatrices (EqToEcl(mo))

let internal columnCoordVector (coord : Coord2D) =
    let x, y = coord
    array2D [ [ cosD x * cosD y ]
              [ sinD x * cosD y ] 
              [      sinD y     ] ]

let generalisedTransformation (conversion : CoordConversion) (from : Coord2D) =
    let rec convertStep matrices coordVector =
        if List.isEmpty matrices then
            coordVector
        else
            let convMatrix = List.head matrices
            let newCoordVector = matrixMult convMatrix coordVector
            convertStep (List.tail matrices) newCoordVector

    let coordVector = convertStep (convMatrices conversion) (columnCoordVector from)
    let m, n, p = (coordVector[0, 0], coordVector[1, 0], coordVector[2, 0])
    let x = atan2D n m |> atan2DRemoveAmbiguity
    let y = asinD p

    Coord2D(x, y)

let raToHa dateTime longitude ra =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - (ra / 15.0)
    |> fun h1 -> if h1 < 0.0 then h1 + 24.0 else h1
    |> (*) 15.0

let haToRa dateTime longitude ha =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - (ha / 15.0)
    |> fun ra -> if ra < 0.0 then ra + 24.0 else ra
    |> (*) 15.0

let equatorialToHorizontal latitude (eq : Coord2D) =
    let ha = fst eq
    let dec = snd eq

    let sinAlt = (sinD dec * sinD latitude) + (cosD dec * cosD latitude * cosD ha)
    let alt = asinD sinAlt

    let cosAz = (sinD dec - sinD latitude * sinAlt) / (cosD latitude * cosD alt)
    let az' = acosD cosAz
    let sinHa = sinD ha
    let az = 
        if sinHa < 0.0 then az' else 360.0 - az'

    Coord2D(az, alt)

let horizontalToEquatorial latitude (hor : Coord2D) =
    let (az, alt) = hor
    let sinDec = (sinD alt * sinD latitude) + (cosD alt * cosD latitude * cosD az)
    let dec = asinD sinDec

    let cosHa = (sinD alt - sinD latitude * sinDec) / (cosD latitude * cosD dec)
    let ha' = acosD cosHa
    let sinAz = sinD az
    let ha =
        if sinAz < 0.0 then ha' else 360.0 - ha'

    Coord2D(ha, dec)

let meanObliquity dateTime =
    dateTimeToJulianDate dateTime
    |> fun jd -> (jd.jd - 2_451_545.0) / 36_525.0
    |> fun t -> (46.815 * t + 0.0006 * t * t - 0.00181 * t * t * t) / 3600.0
    |> (-) 23.439_292

let eclitpicToEquatorial dateTime (ecl : Coord2D) =
    let (lon, lat) = ecl
    let meanObl = meanObliquity dateTime

    let sinDec = (sinD lat * cosD meanObl) + (cosD lat * sinD meanObl * sinD lon)
    let dec = asinD sinDec

    let y = (sinD lon * cosD meanObl) - (tanD lat * sinD meanObl)
    let x = cosD lon
    let ra' = atan2D y x
    let ra = ra' |> atan2DRemoveAmbiguity

    Coord2D(ra, dec)

let equatorialToEcliptic dateTime (eq : Coord2D) =
    let (ra, dec) = (fst eq * 15.0, snd eq)
    let meanObl = meanObliquity dateTime

    let sinLat = (sinD dec * cosD meanObl) - (cosD dec * sinD meanObl * sinD ra)
    let lat = asinD sinLat

    let y = (sinD ra * cosD meanObl) + (tanD dec * sinD meanObl)
    let x = cosD ra
    let lon' = atan2D y x
    let lon = lon' |> atan2DRemoveAmbiguity

    Coord2D(lon, lat)

let equatorialToGalactic (eq : Coord2D) =
    let (ra, dec) = (fst eq, snd eq)

    let sinLat = (cosD dec * cosD 27.4 * cosD (ra - 192.25)) + (sinD dec * sinD 27.4)
    let lat = asinD sinLat

    let y = sinD dec - (sinD lat * sinD 27.4)
    let x = cosD dec * sinD (ra - 192.25) * cosD 27.4
    let lon' = atan2D y x + 33.0
    let lon = lon' |> atan2DRemoveAmbiguity

    Coord2D(lon, lat)

let galacticToEquatorial (gal : Coord2D) =
    let (lon, lat) = gal

    let sinDec = (cosD lat * cosD 27.4 * sinD (lon - 33.0)) + (sinD lat * sinD 27.4)
    let dec = asinD sinDec

    let y = cosD lat * cosD (lon - 33.0)
    let x = (sinD lat * cosD 27.4) - (cosD lat * sinD 27.4 * sinD (lon - 33.0))
    let ra' = atan2D y x + 192.25
    let ra = ra' |> atan2DRemoveAmbiguity

    Coord2D(ra, dec)

let celestialAngle (celestialObj1 : Coord2D) (celestialObj2 : Coord2D) =
    let (x1, y1) = celestialObj1
    let (x2, y2) = celestialObj2

    (sinD y1 * sinD y2) + (cosD y1 * cosD y2 * cosD (x1 - x2))
    |> acosD

let private cosHRisingAndSetting v lat dec =
    -((sinD v + sinD lat * sinD dec) / (cosD lat * cosD dec))

let neverRises v latitude declination =
    if cosHRisingAndSetting v latitude declination > 1.0 then true else false

let isCircumpolar v latitude declination =
    if cosHRisingAndSetting v latitude declination < -1.0 then true else false

let risingAndSetting (dateTime: DateTime) v (geo : Coord2D) (eq : Coord2D) =
    let (ra, dec) = eq
    let (lat, lon) = geo
    let cosH = cosHRisingAndSetting v lat dec

    if cosH < -1.0 || cosH > 1.0 then 
        (None, None)
    else
        let h = acosD cosH / 15.0
        let lstr = reduceToRange 0.0 24.0 (ra - h)
        let lsts = reduceToRange 0.0 24.0 (ra + h)
        let ar = 
            acosD ((sinD dec + sinD v * sinD lat) / (cosD v * cosD lat))
            |> reduceToRange 0.0 360.0
        let as' = 360.0 - ar
        
        let lstToUt lst =
            let gst = lstToGst lon (TimeSpan.FromHours(lst))
            gstToUt (dateTime.Date.AddHours gst.TotalHours)

        let utr = lstToUt lstr
        let uts = lstToUt lsts

        (Some((utr, ar)), Some((uts, as')))

let internal epochPrecession epoch =
    match epoch with
    | J1900 -> { PrecessionalConstant.n = 3.072_34; m = 1.336_45; n' = 20.046_8 }
    | J1950 -> { PrecessionalConstant.n = 3.073_27; m = 1.336_17; n' = 20.042_6 }
    | J2000 -> { PrecessionalConstant.n = 3.074_20; m = 1.335_89; n' = 20.038_3 }
    | J2050 -> { PrecessionalConstant.n = 3.075_13; m = 1.335_60; n' = 20.034_0 }

let internal epochToYear epoch =
    match epoch with
    | J1900 -> 1900
    | J1950 -> 1950
    | J2000 -> 2000
    | J2050 -> 2050

let precessionLowPrecision epoch year (eq : Coord2D) =
    let (ra, dec) = eq

    let n = year - (epochToYear epoch |> float)
    let s1 = ((3.073_27 + 1.33617 * sinD ra * tanD dec) * n) / 3600.0 * 15.0
    let raP = (s1 + ra)

    let s2 = (20.0426 * cosD ra) * n / 3600.0
    let decP = s2 + dec

    Coord2D(raP, decP)

let private precessionMatrix zeta z theta =
    let cx = cosD zeta
    let sx = sinD zeta
    let cz = cosD z
    let sz = sinD z
    let ct = cosD theta
    let st = sinD theta

    array2D [ [  cx * ct * cz - sx * sz;  cx * ct * sz + sx * cz;  cx * st ]
              [ -sx * ct * cz - cx * sz; -sx * ct * sz + cx * cz; -sx * st ] 
              [         -st * cz       ;         -st * sz       ;     ct   ] ]

let precessionRigorousMethod epoch year (eq : Coord2D) =
    
    let findMatrix epochJd =
        let t   = (epochJd.jd - 2_451_545.0) / 36_525.0
        let t'2 = t ** 2
        let t'3 = t ** 3
        let zeta  = 0.640_6161 * t + 0.000_0839 * t'2 + 0.000_0050 * t'3
        let z     = 0.640_6161 * t + 0.000_3041 * t'2 + 0.000_0051 * t'3
        let theta = 0.556_7530 * t - 0.000_1185 * t'2 - 0.000_0116 * t'3
        precessionMatrix zeta z theta

    let (ra, dec) = eq
    let epoch1 = epochToYear epoch
    let epoch1Jd = dateTimeToJulianDate (new DateTime(epoch1, 1, 1))

    let P' = findMatrix epoch1Jd
    let v = array2D [ [ cosD ra * cosD dec ]
                      [ sinD ra * cosD dec ]
                      [      sinD dec      ] ]
    let s = matrixMult P' v

    let (yearInt, yearF) = intAndFrac year
    let days = if DateTime.IsLeapYear(yearInt) then 366.0 * yearF else 365.0 * yearF
    // In the book the date time for epoch 2 is rounded to the month but I decided to be more precise
    let epoch2Jd = dateTimeToJulianDate ((new DateTime(yearInt, 1, 1)).AddDays(days))

    let P = findMatrix epoch2Jd |> transpose3x3
    let w = matrixMult P s

    let m, n, p = (w[0, 0], w[1, 0], w[2, 0])
    let ra' = atan2D n m |> atan2DRemoveAmbiguity
    let dec' = asinD p

    Coord2D(ra', dec')

let nutation dateTime =
    let jd = dateTimeToJulianDate dateTime
    let t = (jd.jd - 2_415_020.0) / 36_525.0

    let calculateCelestialPosition x y (op : float -> float -> float) =
        op x (360.0 * (y - truncate y)) |> reduceToRange 0.0 360.0

    let a = 100.002_136 * t
    let l = calculateCelestialPosition 279.6967 a (+)

    let b = 5.372_617 * t
    let moonNode = calculateCelestialPosition 259.1833 b (-)

    let longitude = -17.2 * sinD moonNode - 1.3 * sinD (2.0 * l)
    let obliquity = 9.2 * cosD moonNode + 0.5 * cosD (2.0 * l)

    Coord2D(longitude, obliquity)

let abberration sunLon (ecl : Coord2D) =
    let (lon, lat) = ecl
    let deltaLon = (-20.5 * cosD(sunLon - lon) / cosD lat) / 3600.0
    let deltaLat = (-20.5 * sinD(sunLon - lon) * sinD lat) / 3600.0

    Coord2D(lon + deltaLon, lat + deltaLat)

let refraction temperature pressure (hor: Coord2D) =
    let (az, alt) = hor
    let z = 90.0 - alt
    
    let r =
        match alt with
        | _ when alt > 15.0 -> 0.00452 * pressure * tanD z / (273.0 + temperature)
        | _ -> 
            (pressure * (0.1594 + 0.0196 * alt * 0.00002 * (alt ** 2))) / 
            ((273.0 + temperature) + (1.0 + 0.505 * alt + 0.0845 * (alt ** 2)))

    let alt' = alt + r

    Coord2D(az, alt')