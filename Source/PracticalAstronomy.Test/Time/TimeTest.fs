module PracticalAstronomy.Test.TimeTest

open System
open NUnit.Framework
open PracticalAstronomy.Time

[<TestCase(2009, 6, 19, 18, 2_455_002.25)>]
let ``Test dateTimeToJulianDate`` (year : int) (month : int) (day : int) (hour : int) (expectedJulianDate : float) =
    let result = dateTimeToJulianDate (new DateTime(year, month, day, hour, 0, 0))
    Assert.That(result.julianDate, Is.EqualTo(expectedJulianDate))