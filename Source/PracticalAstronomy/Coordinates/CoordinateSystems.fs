module PracticalAstronomy.CoordinateSystems

open PracticalAstronomy.Units
open PracticalAstronomy.MathHelper
open PracticalAstronomy.Time
open PracticalAstronomy.CoordinateDataTypes

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

let horToEq latitude hor =
    let az, alt = hor.azimuth, hor.altitude

    let sinDec = (sinD alt * sinD latitude) + (cosD alt * cosD latitude * cosD az)
    let dec = asinD sinDec

    let cosHa = (sinD alt - sinD latitude * sinDec) / (cosD latitude * cosD dec)
    let ha' = acosD cosHa
    let sinAz = sinD az
    let ha =
        if sinAz < 0.0 then ha' else 360.0<deg> - ha'

    { hourAngle = ha; declination = dec }