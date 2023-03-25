module PracticalAstronomy.Test.TimeTest

open System
open NUnit.Framework
open PracticalAstronomy.Time

[<TestCase(2009, 6, 19, 18, 2_455_002.25)>]
let ``Test dateTimeToJulianDate`` (year : int) (month : int) (day : int) (hour : int) (expectedJulianDate : float) =
    let result = dateTimeToJulianDate (new DateTime(year, month, day, hour, 0, 0))
    Assert.That(result.julianDate, Is.EqualTo(expectedJulianDate))

[<TestCase(2_455_002.25, 2009, 6, 19, 18)>]
let ``Test julianDateToDateTime`` (julianDate : float) (eyear : int) (emonth : int) (eday : int) (ehour : int)  =
    let result = julianDateToDateTime ({ julianDate = julianDate })
    Assert.That(result, Is.EqualTo(new DateTime(eyear, emonth, eday, ehour, 0, 0)))