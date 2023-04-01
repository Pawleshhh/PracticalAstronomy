module PracticalAstronomy.Coordinates

open MathHelper
open Time
open CoordinateSystemTypes

let raToHa dateTime longitude ra =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - ra
    |> fun h1 -> if h1 < 0.0 then h1 + 24.0 else h1

let haToRa dateTime longitude ha =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - ha
    |> fun ra -> if ra < 0.0 then ra + 24.0 else ra

let equatorialToHorizontal latitude (eq : Coord2D) =
    let ha = fst eq * 15.0
    let dec = snd eq

    let sinAlt = (sinD dec * sinD latitude) + (cosD dec * cosD latitude * cosD ha)
    let alt = asinD sinAlt

    let cosAz = (sinD dec - sinD latitude * sinAlt) / (cosD latitude * cosD alt)
    let az' = acosD cosAz
    let sinHa = sinD ha
    let az = 
        if sinHa < 0.0 then az' else 360.0 - az'

    Coord2D(alt, az)

let horizontalToEquatorial latitude (hor : Coord2D) =
    let (alt, az) = hor
    let sinDec = (sinD alt * sinD latitude) + (cosD alt * cosD latitude * cosD az)
    let dec = asinD sinDec

    let cosHa = (sinD alt - sinD latitude * sinDec) / (cosD latitude * cosD dec)
    let ha' = acosD cosHa
    let sinAz = sinD az
    let ha =
        if sinAz < 0.0 then ha' else 360.0 - ha'

    Coord2D(ha / 15.0, dec)

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
    let ra = ra' - (360.0 * ((ra' / 360.0) |> int |> float))

    Coord2D(ra / 15.0, dec)

let equatorialToEcliptic dateTime (eq : Coord2D) =
    let (ra, dec) = (fst eq * 15.0, snd eq)
    let meanObl = meanObliquity dateTime

    let sinLat = (sinD dec * cosD meanObl) - (cosD dec * sinD meanObl * sinD ra)
    let lat = asinD sinLat

    let y = (sinD ra * cosD meanObl) + (tanD dec * sinD meanObl)
    let x = cosD ra
    let lon' = atan2D y x
    let lon = lon' - (360.0 * ((lon' / 360.0) |> int |> float))

    Coord2D(lon, lat)