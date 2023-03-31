﻿module PracticalAstronomy.Test.CoordinatesTest

open System
open NUnit.Framework
open PracticalAstronomy.Coordinates

[<TestCase(18.539_167, -64.0, 1980, 4, 22, 18.614_353, 9.873_237)>]
let ``Test raToHa`` (ra : float) (longitude : float) (year : int) (month : int) (day : int) (time : float) (ha : float) =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = raToHa dateTime longitude ra
    Assert.That(result, Is.EqualTo(ha).Within(1E-6))

[<TestCase(9.873_239, -64.0, 1980, 4, 22, 18.614_353, 18.539_165)>]
let ``Test haToRa`` (ha : float) (longitude : float) (year : int) (month : int) (day : int) (time : float) (ra : float) =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = haToRa dateTime longitude ha
    Assert.That(result, Is.EqualTo(ra).Within(1E-6))