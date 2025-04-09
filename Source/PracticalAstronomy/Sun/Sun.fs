module PracticalAstronomy.Sun

open System
open Units
open MathHelper
open TimeDataTypes
open Time
open CoordinateSystems

let positionOfSun epoch dateTime =
    let jd = (dateTimeToJulianDate dateTime).julianDate
    let epochJd = 
        epochToDateTime epoch
        |> dateTimeToJulianDate
        |> fun jd -> jd.julianDate
    let t = (epochJd - 2_415_020.0) / 36_525.0

    let epsilion_g = 
        279.696_677_8 + 36_000.76892 * t + 0.000_302_5 * t * t
        |> reduceToRange 0.0 360.0
        |> (*) 1.0<deg>
    let omega_g = 
        281.220_844_4 + 1.719_175 * t + 0.000_452_778 * t * t
        |> (*) 1.0<deg>
    let e =
        0.016_751_04 - 0.000_041_8 * t - 0.000_000_126 * t * t

    let n = 
        (360.0<deg> / 365.242_191) 
        |> (*) (jd - epochJd) 
        |> reduceToRangeDeg 0.0 360.0
    let m = 
        n + epsilion_g - omega_g
        |> fun v -> if v < 0.0<deg> then v + 360.0<deg> else v

    let ec = 
        (360.0<deg> / Math.PI) * e * sinD m
    let sunLon = 
        n + ec + epsilion_g
        |> fun v -> if v > 360.0<deg> then v - 360.0<deg> else v

    sunLon

let positionOfSunEquatorial epoch dateTime =
    let sunLon = positionOfSun epoch dateTime
    eclToEq dateTime ({ eclLongitude = sunLon; eclLatitude = 0.0<deg> })