module PracticalAstronomy.Test.TimeTest

open NUnit.Framework
open PracticalAstronomy.Time
open PracticalAstronomy.Test.TestUtils

[<TestCase(2009,  6, 19, 0.75, 2_455_002.25)>]
[<TestCase(2035,  2,  1, 0.33, 2_464_359.83)>]
[<TestCase(1582, 10, 14, 0.90, 2_299_170.40)>]
let ``Test dateTimeToJulianDate`` y m d t (expectedJd: float) =
    let dateTime = createDateTime y m d t

    let result = dateTimeToJulianDate dateTime

    Assert.That(result.julianDate, Is.EqualTo(expectedJd))

[<TestCase(2_455_002.25, 2009,  6, 19, 0.75)>]
[<TestCase(2_464_359.83, 2035,  2,  1, 0.33)>]
[<TestCase(2_299_170.40, 1582, 10, 24, 0.90)>]
let ``Test julianDateToDateTime`` jd y m d t =
    let result = julianDateToDateTime ({ julianDate = jd })

    let expectedDateTime = createDateTime y m d t
    Assert.That(result.Date, Is.EqualTo(expectedDateTime.Date))
    Assert.That(result.TimeOfDay.TotalHours, Is.EqualTo(expectedDateTime.TimeOfDay.TotalHours).Within(1E-2))

[<TestCase(1980,  4, 22, 14, 36, 51, 670,  4.668_120)>]
[<TestCase(2033, 12, 31,  0,  0, 58, 999,  6.665_555)>]
[<TestCase(1912,  6,  3, 17, 32,  1,   0, 10.320_293)>]
let ``Test dateTimeToGst`` y m d h min s mil (gst: float) =
    let result = dateTimeToGst (ymdhmsToDateTime y m d h min s mil)
    Assert.That(result.TotalHours, Is.EqualTo(gst).Within(1E-6))

[<TestCase(1980,  4, 22,  4, 40,  5, 232, 14.614_353)>]
[<TestCase(2033, 12, 31,  6, 39, 55, 998,  0.016_388)>]
[<TestCase(1912,  6,  3, 10, 19, 13,  55, 17.533_611)>]
let ``Test gstToUt`` y m d h min s mil (ut: float) =
    let result = gstToUt (ymdhmsToDateTime y m d h min s mil)
    Assert.That(result.TotalHours, Is.EqualTo(ut).Within(1E-6))

[<TestCase( -64.0,  4, 40,  5, 230, 0.401_453)>]
[<TestCase(  13.0, 23, 59, 59, 999, 0.866_666)>]
[<TestCase(-179.0, 17,  3, 53,   0, 5.131_389)>]
let ``Test gstToLst`` longitude h m d mil (lst: float) =
    let result = gstToLst longitude (hmsToTimeSpan h m d mil)
    Assert.That(result.TotalHours, Is.EqualTo(lst).Within(1E-6))
    
[<TestCase( -64.0,  0, 24,  5, 231,  4.668_119)>]
[<TestCase(  13.0,  0, 51, 59, 998, 23.999_999)>]
[<TestCase(-179.0,  5,  7, 53,   0, 17.064_722)>]
let ``Test lstToGst`` longitude h m d mil (lst: float) =
    let result = lstToGst longitude (hmsToTimeSpan h m d mil)
    Assert.That(result.TotalHours, Is.EqualTo(lst).Within(1E-6))