module PracticalAstronomy.Test.CoordinateSystemsTests

open NUnit.Framework
open PracitcalAstronomy.CoordinateSystems
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