module internal PracticalAstronomy.DateTimeHelper

open System

let deconstructYmd (dateTime : DateTime) =
    (dateTime.Year, dateTime.Month, dateTime.Day)

let deconstructYmdhms (dateTime : DateTime) =
    (dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second)

let deconstructYmdt (dateTime : DateTime) =
    (dateTime.Year, dateTime.Month, dateTime.Day, dateTime.TimeOfDay.TotalHours)