module PracticalAstronomy.Test.TimeTest

open System
open NUnit.Framework
open PracticalAstronomy.Time

[<TestCase(2009, 6, 19, 18, 2_455_002.25)>]
let ``Test dateTimeToJulianDate`` (y : int) (m : int) (d : int) (h : int) (expectedJd : float) =
    let result = dateTimeToJulianDate (new DateTime(y, m, d, h, 0, 0))
    Assert.That(result.jd, Is.EqualTo(expectedJd))

[<TestCase(2_455_002.25, 2009, 6, 19, 18)>]
let ``Test julianDateToDateTime`` (jd : float) (ey : int) (em : int) (ed : int) (eh : int)  =
    let result = julianDateToDateTime ({ jd = jd })
    Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed, eh, 0, 0)))

[<TestCase(1980, 4, 22, 14, 36, 51, 670, 4.668_120)>]
let ``Test dateTimeToGst`` (y : int) (m : int) (d : int) (h : int) (min : int) (s : int) (mil : int) (gst : float) =
    let result = dateTimeToGst (new DateTime(y, m, d, h, min, s, mil))
    Assert.That(result.TotalHours, Is.EqualTo(gst).Within(1E-6))

[<TestCase(1980, 4, 22, 4, 40, 5, 230, 14.614_353)>]
let ``Test gstToUt`` (y : int) (m : int) (d : int) (h : int) (min : int) (s : int) (mil : int) (ut : float) =
    let result = gstToUt (new DateTime(y, m, d, h, min, s, mil))
    Assert.That(result.TotalHours, Is.EqualTo(ut).Within(1E-6))

[<TestCase(-64.0, 4, 40, 5, 230, 0.401_453)>]
let ``Test gstToLst`` (longitude : float) (h : int) (m : int) (d : int) (mil : int) (lst : float) =
    let result = gstToLst longitude (new TimeSpan(0, h, m, d, mil))
    Assert.That(result.TotalHours, Is.EqualTo(lst).Within(1E-6))

[<TestCase(-64.0, 0, 24, 5, 230, 4.668_119)>]
let ``Test lstToGst`` (longitude : float) (h : int) (m : int) (d : int) (mil : int) (lst : float) =
    let result = lstToGst longitude (new TimeSpan(0, h, m, d, mil))
    Assert.That(result.TotalHours, Is.EqualTo(lst).Within(1E-6))