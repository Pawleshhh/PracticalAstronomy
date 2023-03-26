module internal PracticalAstronomy.TimeHelper

open System

let deconstructDateWithTime (dateTime : DateTime) =
    let fractionOfDay = dateTime.TimeOfDay.TotalHours / 24.0
    (dateTime.Year, dateTime.Month, (dateTime.Day |> float) + fractionOfDay)