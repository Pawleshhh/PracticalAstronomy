﻿module PracticalAstronomy.Test.CoordinateSystemsTests

open System
open NUnit.Framework
open PracticalAstronomy.CoordinateSystems
open PracticalAstronomy.Test.TestUtils

[<TestCase(1980, 4, 22, 18.614_353,  -64.0, 278.087_505, 148.098_555)>]
[<TestCase(2034, 1, 31,  4.006_172, -120.0, 359.749_995,  70.799_010)>]
[<TestCase(2011, 4, 10, 15.060_897,    1.0,  81.179_166, 344.230_827)>]
let raToHa y m d t lon ra ha =
    let result = raToHa (createDateTimeWithDecimalHours y m d t) lon ra
    Assert.That(result, Is.EqualTo(ha).Within(1E-5))

[<TestCase(1980, 4, 22, 18.614_353,  -64.0, 148.098_555, 278.087_505)>]
[<TestCase(2034, 1, 31,  4.006_172, -120.0,  70.799_010, 359.749_995)>]
[<TestCase(2011, 4, 10, 15.060_897,    1.0, 344.230_827,  81.179_166)>]
let haToRa y m d t lon ha ra =
    let result = haToRa (createDateTimeWithDecimalHours y m d t) lon ha
    Assert.That(result, Is.EqualTo(ra).Within(1E-5))

[<TestCase( 52.0,  87.93333, 23.219_444, 283.271_027,  19.334_345)>]
[<TestCase(-88.0,  14.99583, 11.536_388, 345.100_442,  -9.604_051)>]
[<TestCase(  3.0, 330.26250, 66.059_444,  12.684_684,  23.559_398)>]
let eqToHor latitude ha dec az alt =
    let result = eqToHor latitude { hourAngle = ha; declination = dec }
    Assert.That((result.azimuth, result.altitude), Is.EqualTo((az, alt)).Within(1E-5))

[<TestCase( 52.0, 283.271_027,  19.334_345,  87.93333, 23.219_444)>]
[<TestCase(-88.0, 345.100_442,  -9.604_051,  14.99583, 11.536_388)>]
[<TestCase(  3.0,  12.684_684,  23.559_398, 330.26250, 66.059_444)>]
let horToEq latitude az alt ha dec =
    let result = horToEq latitude { azimuth = az; altitude = alt }
    Assert.That((result.hourAngle, result.declination), Is.EqualTo((ha, dec)).Within(1E-5))

[<TestCase(2009,  7,  6, 23.438_055)>]
[<TestCase(2029, 12, 13, 23.435_397)>]
[<TestCase(1856,  3,  6, 23.457_992)>]
let meanObliquity y m d ob =
    let result = meanObliquity (new DateTime(y, m, d))
    Assert.That(result, Is.EqualTo(ob).Within(1E-6))

[<TestCase(139.686_1111,  4.875_277_778, 2009,  7,  6, 143.722_5092, 19.535_699_24)>]
[<TestCase( 45.900_2777, 10.048_611_111, 2041, 12,  7,  40.215_4901, 26.188_384_41)>]
[<TestCase(  1.999_7222, 87.050_555_555, 1956,  8, 23, 277.404_8436, 66.484_051_86)>]
let eclToEq lon lat year month day ra dec =
    let result = eclToEq (new DateTime(year, month, day)) { longitude = lon; latitude = lat }
    Assert.That((result.rightAscension, result.declination), Is.EqualTo((ra, dec)).Within(1E-5))


[<TestCase(143.722_5092, 19.535_699_24, 2009,  7,  6, 139.686_1111, 4.875_277_778)>]
[<TestCase( 40.215_4901, 26.188_384_41, 2041, 12,  7,  45.900_2777, 10.048_611_111)>]
[<TestCase(277.404_8436, 66.484_051_86, 1956,  8, 23,   1.999_7222, 87.050_555_555)>]
let eqToEcl ra dec year month day lon lat =
    let result = eqToEcl (new DateTime(year, month, day)) { rightAscension = ra; declination = dec }
    Assert.That((result.longitude, result.latitude), Is.EqualTo((lon, lat)).Within(1E-5))