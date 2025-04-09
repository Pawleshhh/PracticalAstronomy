module PracticalAstronomy.Test.SunTests

open System
open NUnit.Framework
open PracticalAstronomy.Units
open PracticalAstronomy.CoordinateDataTypes
open PracticalAstronomy.Sun
open PracticalAstronomy.TimeDataTypes
open PracticalAstronomy.Test.TestUtils

[<TestCase(2003, 7, 27, 0, 0, 0, 125.890_525_6, 19.353_980_8)>]
[<TestCase(2025, 3, 8, 16, 17, 41, 349.2788267, -4.6109991)>]
[<TestCase(2010, 1, 1, 0, 0, 0, 281.3739521, -23.0268581)>]
let positionOfSunEquatorial (y: int) m d h mm s ra dec =
    let dt = new DateTime(y, m, d, h, mm, s)
    let result = positionOfSunEquatorial (JEpoch(2010)) dt
    Assert.That((result.rightAscension, result.declination), Is.EqualTo((ra, dec)).Within(1E-5))