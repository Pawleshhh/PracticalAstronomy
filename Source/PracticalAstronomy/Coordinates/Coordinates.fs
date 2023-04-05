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