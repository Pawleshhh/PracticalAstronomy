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

let equatorialToHorizon (eq : Coord2D) latitude =
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