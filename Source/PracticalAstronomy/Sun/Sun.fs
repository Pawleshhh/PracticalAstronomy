module PracticalAstronomy.Sun

open System
open Units
open MathHelper
open TimeDataTypes
open Time
open CoordinateSystems

let inline private sunEclipticLon t =
    279.696_677_8 + 36_000.76892 * t + 0.000_302_5 * t * t
    |> reduceToRange 0.0 360.0
    |> (*) 1.0<deg>

let inline private sunEclipticLonPerigee t =
    281.220_844_4 + 1.719_175 * t + 0.000_452_778 * t * t
    |> reduceToRange 0.0 360.0
    |> (*) 1.0<deg>

let inline private sunEccentricityOfOrbit t =
    0.016_751_04 - 0.000_041_8 * t - 0.000_000_126 * t * t

let positionOfSun epoch dateTime =
    let jd = (dateTimeToJulianDate dateTime).julianDate
    let epochJd = 
        epochToDateTime epoch
        |> dateTimeToJulianDate
        |> fun jd -> jd.julianDate
    let t = (epochJd - 2_415_020.0) / 36_525.0

    let epsilion_g = sunEclipticLon t
    let omega_g = sunEclipticLonPerigee t
    let e = sunEccentricityOfOrbit t

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

let positionOfSunEquatorialPrecise dateTime =
    let jd = (dateTimeToJulianDate dateTime).julianDate
    let t = (jd - 2_415_020.0) / 36_525.0

    let epsilion_g = sunEclipticLon t
    let omega_g = sunEclipticLonPerigee t
    let e = sunEccentricityOfOrbit t

    let meanAnomaly = 
        (epsilion_g - omega_g)
        |> reduceToRangeDeg 0.0 360.0
        |> degToRad

    let epsilion_g_rad = degToRad epsilion_g

    let rec routineR2 E =
        let delta = (E - e * sin E) * 1.0<rad> - meanAnomaly

        match abs(delta) with
        | v when v <= epsilion_g_rad -> (E * 1.0<rad>)
        | _ ->
            let E2 = delta / (1.0 - e * cos E) / 1.0<rad>
            routineR2 (E - E2)

    let E =
        (meanAnomaly / 1.0<rad>) 
        |> routineR2 
        |> radToDeg

    let v =
        ((1.0 + e) / (1.0 - e))
        |> sqrt
        |> (*) (tanD (E / 2.0))
        |> atanD
        |> (*) 2.0
        |> fun x -> if x < 0.0<deg> then x + 360.0<deg> else x

    let sunLon = 
        v + omega_g
        |> fun x -> if x > 360.0<deg> then x - 360.0<deg> else x

    eclToEq dateTime ({ eclLongitude = sunLon; eclLatitude = 0.0<deg> })