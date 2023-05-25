module PracticalAstronomy.CoordinateSystems

open System
open PracticalAstronomy.Units
open PracticalAstronomy.MathHelper
open PracticalAstronomy.Time
open PracticalAstronomy.CoordinateDataTypes

let meanObliquity dateTime =
    dateTimeToJulianDate dateTime
    |> fun jd -> (jd.julianDate - 2_451_545.0) / 36_525.0
    |> fun t -> t * (46.815 + 0.0006 * t - 0.00181 * (t ** 2)) / 3600.0
    |> fun de -> (23.439_292 - de) * 1.0<deg>

let private nutationObliquity dateTime =

    let DJ = (dateTimeToJulianDate dateTime).julianDate - 2415020.0
    let T = DJ / 36525.0
    let T2 = T * T

    let A1 = 100.0021358 * T
    let B1 = 360.0 * (A1 - (fst (intAndFrac A1) |> float))
    let L1 = 279.6967 + 0.000303 * T2 + B1 |> (*) 1.0<deg>
    let l2 = 2.0 * degToRad L1 / 1.0<rad>

    let A2 = 1336.855231 * T
    let B2 = 360.0 * (A2 - (fst (intAndFrac A2) |> float))
    let D1 = 270.4342 - 0.001133 * T2 + B2 |> (*) 1.0<deg>
    let D2 = 2.0 * degToRad D1 / 1.0<rad>

    let A3 = 99.99736056 * T
    let B3 = 360.0 * (A3 - (fst (intAndFrac A3) |> float))
    let M1 = 358.4758 - 0.00015 * T2 + B3 |> (*) 1.0<deg>
    let M1 = degToRad M1 / 1.0<rad>

    let A4 = 1325.552359 * T
    let B4 = 360.0 * (A4 - (fst (intAndFrac A4) |> float))
    let M2 = 296.1046 + 0.009192 * T2 + B4 |> (*) 1.0<deg>
    let M2 = degToRad M2 / 1.0<rad>

    let A5 = 5.372616667 * T
    let B5 = 360.0 * (A5 - (fst (intAndFrac A5) |> float))
    let N1 = 259.1833 + 0.002078 * T2 - B5 |> (*) 1.0<deg>
    let N1' = degToRad N1 / 1.0<rad>
    let N2 = 2.0 * N1'

    let DDO1 = (9.21 + 0.00091 * T) * cos N1'
    let DDO2 = DDO1+ (0.5522 - 0.00029 * T) * cos l2 - 0.0904 * cos N2
    let DDO3 = DDO2 + 0.0884 * cos D2 + 0.0216 * cos (l2 + M1)
    let DDO4 = DDO3 + 0.0183 * cos (D2 - N1') + 0.0113 * cos (D2 + M2)
    let DDO5 = DDO4 - 0.0093 * cos (l2 - M1) - 0.0066 * cos (l2 - N1')

    DDO5 / 3600.0 * 1.0<deg>

let meanObliquityWithNutation dateTime =
    meanObliquity dateTime + nutationObliquity dateTime

let raToHa dateTime longitude ra =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - (ra / 15.0<deg>)
    |> fun h1 -> if h1 < 0.0 then h1 + 24.0 else h1
    |> (*) 15.0<deg>

let haToRa dateTime longitude ha =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - (ha / 15.0<deg>)
    |> fun ra -> if ra < 0.0 then ra + 24.0 else ra
    |> (*) 15.0<deg>

let eqToHor latitude eq =
    let ha, dec = eq.hourAngle, eq.declination

    let sinAlt = (sinD dec * sinD latitude) + (cosD dec * cosD latitude * cosD ha)
    let alt = asinD sinAlt

    let cosAz = (sinD dec - sinD latitude * sinAlt) / (cosD latitude * cosD alt)
    let az' = acosD cosAz
    let sinHa = sinD ha
    let az = 
        if sinHa < 0.0 then az' else 360.0<deg> - az'

    { azimuth = az; altitude = alt }

let horToEq latitude (hor: Horizon) =
    let az, alt = hor.azimuth, hor.altitude

    let sinDec = (sinD alt * sinD latitude) + (cosD alt * cosD latitude * cosD az)
    let dec = asinD sinDec

    let cosHa = (sinD alt - sinD latitude * sinDec) / (cosD latitude * cosD dec)
    let ha' = acosD cosHa
    let sinAz = sinD az
    let ha =
        if sinAz < 0.0 then ha' else 360.0<deg> - ha'

    { hourAngle = ha; declination = dec }

let eclToEq dateTime ecl =
    let lon, lat = ecl.eclLongitude, ecl.eclLatitude
    let meanObl = meanObliquityWithNutation dateTime

    let sinDec = sinD lat * cosD meanObl + cosD lat * sinD meanObl * sinD lon
    let dec = asinD sinDec

    let y = (sinD lon * cosD meanObl) - (tanD lat * sinD meanObl)
    let x = cosD lon
    let ra' = atan2D y x
    let ra = ra' / 1.0<deg> |> atan2DRemoveAmbiguity

    { rightAscension = ra * 1.0<deg>; declination = dec }

let eqToEcl dateTime eq =
    let ra, dec = eq.rightAscension, eq.declination
    let meanObl = meanObliquityWithNutation dateTime

    let sinLat = (sinD dec * cosD meanObl) - (cosD dec * sinD meanObl * sinD ra)
    let lat = asinD sinLat

    let y = (sinD ra * cosD meanObl) + (tanD dec * sinD meanObl)
    let x = cosD ra
    let lon' = atan2D y x
    let lon = lon' / 1.0<deg> |> atan2DRemoveAmbiguity

    { eclLongitude = lon * 1.0<deg>; eclLatitude = lat }

let eqToGal eq =
    let ra, dec = eq.rightAscension, eq.declination

    let sinLat = 
        (cosD dec * cosD 27.4<deg> * cosD (ra - 192.25<deg>)) + (sinD dec * sinD 27.4<deg>)
    let lat = asinD sinLat

    let y = sinD dec - (sinD lat * sinD 27.4<deg>)
    let x = cosD dec * sinD (ra - 192.25<deg>) * cosD 27.4<deg>
    let lon' = atan2D y x + 33.0<deg>
    let lon = lon' / 1.0<deg> |> atan2DRemoveAmbiguity

    { galLongitude = lon * 1.0<deg>; galLatitude = lat }

let galToEq gal =
    let lon, lat = gal.galLongitude, gal.galLatitude

    let sinDec = 
        (cosD lat * cosD 27.4<deg> * sinD (lon - 33.0<deg>)) + (sinD lat * sinD 27.4<deg>)
    let dec = asinD sinDec

    let y = cosD lat * cosD (lon - 33.0<deg>)
    let x = (sinD lat * cosD 27.4<deg>) - (cosD lat * sinD 27.4<deg> * sinD (lon - 33.0<deg>))
    let ra' = atan2D y x + 192.25<deg>
    let ra = ra' / 1.0<deg> |> atan2DRemoveAmbiguity

    { rightAscension = ra * 1.0<deg>; declination = dec }

let internal celestialAngle (coord1: ICoordinateSystem) (coord2: ICoordinateSystem) =
    let x1, y1 = coord1.x, coord1.y
    let x2, y2 = coord2.x, coord2.y

    (sinD y1 * sinD y2 + cosD y1 * cosD y2 * cosD (x1 - x2))
    |> acosD

let celestialAngleEq (eq1: EquatorialRightAscension) (eq2: EquatorialRightAscension) =
    celestialAngle eq1 eq2

let celestialAngleEcl (ecl1: Ecliptic) (ecl2: Ecliptic) =
    celestialAngle ecl1 ecl2

let risingAndSetting (dateTime: DateTime) v geo eq =
    let ra, dec = eq.rightAscension, eq.declination
    let lat, lon = geo.latitude, geo.longitude

    let cosH = - (sinD v + sinD lat * sinD dec) / (cosD lat * cosD dec)
    if cosH < -1.0 || cosH > 1.0 then
        None
    else
        let az = (sinD dec + sinD v * sinD lat) / (cosD v * cosD lat) |> acosD
        let azR = 
            az |> reduceToRangeDeg 0.0 360.0
        let azS = 
            360.0<deg> - az |> reduceToRangeDeg 0.0 360.0
        
        let h = acosD cosH
        let fromHours hrs = TimeSpan.FromHours (hrs / 15.0<deg>)

        let lstR = ra - h |> reduceToRangeDeg 0.0 360.0 |> fromHours
        let lstS = ra + h |> reduceToRangeDeg 0.0 360.0 |> fromHours

        let lstToUt lst = lstToGst lon lst |> fun gst -> gstToUt (dateTime.Date.Add(gst))
        let utR = lstToUt lstR
        let utS = lstToUt lstS

        Some({ rising =  { azimuth = azR; time = utR }; 
               setting = { azimuth = azS; time = utS }})

let precessionLowPrecision epoch dateTime eq =
    let ra, dec = eq.rightAscension, eq.declination

    let epochJd = 
        epochToDateTime epoch
        |> fun dt -> (dateTimeToJulianDate dt).julianDate
    let t = 
        (epochJd - 2_415_020.0) / 36525.0
    let m = 3.072_34 + (0.00186 * t)
    let n' = 20.0468 - (0.0085 * t)
    let n = ((dateTimeToJulianDate dateTime).julianDate - epochJd) / 365.25
    let s1 = ((m + (n' * sinD ra * tanD dec / 15.0)) * n) / 3600.0
    let raP = ((s1 * 15.0<deg>) + ra)

    let s2 = (n' * cosD ra * n) / 3600.0 * 1.0<deg>
    let decP = s2 + dec

    { new ICoordinateSystem with
        member this.x = raP
        member this.y = decP }

let nutation dateTime =
    let jd = dateTimeToJulianDateValue dateTime
    let t = (jd - 2_415_020.0) / 36_525.0
    let a = 100.002_136 * t
    let aInt, _ = floatAndFrac a
    let l = 279.6967 + 360.0 * (a - aInt)
    let b = 5.372_617 * t
    let bInt, _ = floatAndFrac b
    let moonNode = 
        259.1833 - 360.0 * (b - bInt)
        |> reduceToRange 0.0 360.0 
        |> (*) 1.0<deg>
    let nutationLon = -17.2 * sinD moonNode - 1.3 * sinD (2.0<deg> * l) |> (*) 1.0<deg>
    let nutationObl = 9.2 * cosD moonNode + 0.5 * cosD (2.0<deg> * l) |> (*) 1.0<deg>

    { nutationLongitude = nutationLon; nutationObliquity = nutationObl }