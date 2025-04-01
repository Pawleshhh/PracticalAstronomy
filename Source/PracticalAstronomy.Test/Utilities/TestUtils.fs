module PracticalAstronomy.Test.TestUtils

open System

let timeToHours t =
    TimeSpan.FromDays(t).TotalHours

let createDateTime (y: int) m d t =
    (new DateTime(y, m, d)).AddHours(timeToHours t)

let createDateTimeWithDecimalHours (y: int) m d t =
    (new DateTime(y, m, d)).AddHours(t)

let hmsToTimeSpan h m s mil =
    new TimeSpan(0, h, m, s, mil)

let ymdhmsToDateTime y m d h mm s (mil: int) =
    new DateTime(y, m, d, h, mm, s, mil)