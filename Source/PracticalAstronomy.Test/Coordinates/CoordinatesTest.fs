module PracticalAstronomy.Test.CoordinatesTest

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

[<TestCase(5.862_222, 23.219_444, 52.0, 19.334_345, 283.271_027)>]
let ``Test equatorialToHorizontal`` (ha : float) (dec : float) (latitude : float) (alt : float) (az : float) =
    let result = equatorialToHorizontal latitude (ha, dec)
    Assert.That(result, Is.EqualTo((alt, az)).Within(1E-5))

[<TestCase(19.334_344, 283.271_028, 52.0, 5.862_222, 23.219_444)>]
let ``Test horizontalToEquatorial`` (alt : float) (az : float) (latitude : float) (ha : float) (dec : float) =
    let result = horizontalToEquatorial latitude (alt, az)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-5))

[<TestCase(2009, 7, 6, 23.438_055_31)>]
let ``Test meanObliquity`` (year : int) (month : int) (day : int) (meanObl : float) = 
    let result = meanObliquity (new DateTime(year, month, day))
    Assert.That(result, Is.EqualTo(meanObl).Within(1E-8))

[<TestCase(139.686_1111, 4.875_277_778, 2009, 7, 6, 9.581_500_61, 19.535_699_24)>]
let ``Test eclipticToEquatorial`` (lon : float) (lat : float) (year : int) (month : int) (day : int) (ra : float) (dec : float) =
    let result = eclitpicToEquatorial (new DateTime(year, month, day)) (lon, lat)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-3))