module PracticalAstronomy.Test.CoordinateSystemsTests

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