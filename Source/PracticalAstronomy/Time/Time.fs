﻿module PracticalAstronomy.Time

open System
open TimeHelper
open MathHelper

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

let julianDateToDateTime (julianDate : JulianDate) =
    let jd = julianDate.julianDate + 0.5
    let I, F = intAndFrac jd

    let B =
        match I with
        | i when i > 2_299_160 -> 
            (((I |> float) - 1_867_216.25) / 36_524.25)
            |> fun a -> I + (a |> int) - ((a / 4.0) |> truncate |> int) + 1
        | _ -> I

    let C = (B + 1524) |> float
    let D = ((C - 122.1) / 365.25) |> truncate
    let E = (365.25 * D) |> truncate
    let G = ((C - E) / 30.6001) |> truncate
    
    let d = C - E + F - ((30.6001 * G) |> truncate)
    let m =
        (match G with
        | g when g < 13.5 -> G - 1.0
        | _ -> G - 13.0) |> int
    let y =
        (match m with
        | m' when m' >= 3 -> D - 4716.0
        | _ -> D - 4715.0) |> int

    let id, fd = intAndFrac d

    let dateTime = new DateTime(y, m, id)

    dateTime.AddHours (fd * 24.0)