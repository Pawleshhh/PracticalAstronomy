module PracticalAstronomy.Coordinates

open System
open Time

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