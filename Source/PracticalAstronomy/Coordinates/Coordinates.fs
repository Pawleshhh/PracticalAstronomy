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

let equatorialToHorizontal (eq : Coord2D) latitude =
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

let horizontalToEquatorial (hor : Coord2D) latitude =
    let (alt, az) = hor
    let sinDec = (sinD alt * sinD latitude) + (cosD alt * cosD latitude * cosD az)
    let dec = asinD sinDec

    let cosHa = (sinD alt - sinD latitude * sinDec) / (cosD latitude * cosD dec)
    let ha' = acosD cosHa
    let sinAz = sinD az
    let ha =
        if sinAz < 0.0 then ha' else 360.0 - ha'

    Coord2D(ha / 15.0, dec)