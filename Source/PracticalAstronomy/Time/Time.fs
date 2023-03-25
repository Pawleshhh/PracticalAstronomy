module PracticalAstronomy.Time

open System
open TimeHelper

let private gregorianCalendarStart = new DateTime(1582, 10, 15)

let dateTimeToJulianDate (dateTime : DateTime) =
    let y, m, d = deconstructDateWithTime dateTime

    let y', m' =
        match m with
        | v when v = 1 || v = 2 -> (y - 1, m + 12)
        | _ -> (y, m)

    let B = 
        (match dateTime with
        | d when d > gregorianCalendarStart ->
            let a = ((y' |> float) / 100.0) |> truncate
            2.0 - a + ((a / 4.0) |> truncate)
        | _ -> 0) |> float

    let C =
        match y' with
        | v when v < 0 -> ((365.25 * (y' |> float)) - 0.75) |> truncate
        | _ -> (365.25 * (y' |> float)) |> truncate

    let D = (30.6001 * ((m' + 1) |> float)) |> truncate

    { JulianDate.julianDate = B + C + D + d + 1_720_994.5 }