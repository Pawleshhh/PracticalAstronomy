module PracticalAstronomy.Test.CoordinateSystemsTests

open System
open NUnit.Framework
open PracticalAstronomy.CoordinateSystems
open PracticalAstronomy.CoordinateDataTypes
open PracticalAstronomy.Time
open PracticalAstronomy.TimeDataTypes
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
    let result = eclToEq (new DateTime(year, month, day)) { eclLongitude = lon; eclLatitude = lat }
    Assert.That((result.rightAscension, result.declination), Is.EqualTo((ra, dec)).Within(1E-5))

[<TestCase(143.722_5092, 19.535_699_24, 2009,  7,  6, 139.686_1111, 4.875_277_778)>]
[<TestCase( 40.215_4901, 26.188_384_41, 2041, 12,  7,  45.900_2777, 10.048_611_111)>]
[<TestCase(277.404_8436, 66.484_051_86, 1956,  8, 23,   1.999_7222, 87.050_555_555)>]
let eqToEcl ra dec year month day lon lat =
    let result = eqToEcl (new DateTime(year, month, day)) { rightAscension = ra; declination = dec }
    Assert.That((result.eclLongitude, result.eclLatitude), Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(155.250_000, 10.053_056, 232.247_883,  51.122_267)>]
[<TestCase(  0.000_000,  0.000_000,  97.742_161, -60.181_024)>]
[<TestCase( 59.887_500, -5.190_000, 195.748_828, -39.598_461)>]
let eqToGal ra dec lon lat =
    let result = eqToGal { rightAscension = ra; declination = dec }
    Assert.That((result.galLongitude, result.galLatitude), Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(232.247_883,  51.122_267, 155.250_000, 10.053_056)>]
[<TestCase( 97.742_161, -60.181_024,   0.000_000,  0.000_000)>]
[<TestCase(195.748_828, -39.598_461,  59.887_500, -5.190_000)>]
let galToEq lon lat ra dec =
    let result = galToEq { galLongitude = lon; galLatitude = lat }
    Assert.That((result.rightAscension, result.declination), Is.EqualTo((ra, dec)).Within(1E-5))

[<TestCase(78.38208, -8.225_000, 101.05584, -16.686_389, 23.673_850)>]
let celestialAngle x1 y1 x2 y2 d =
    let result = celestialAngle { new ICoordinateSystem with
                                    member this.x = x1
                                    member this.y = y1 }
                                { new ICoordinateSystem with
                                    member this.x = x2
                                    member this.y = y2 }
    Assert.That(result, Is.EqualTo(d).Within(1E-5))

[<TestCase(2010,  8, 24, 0.5667, 30.0      , 64.0      , 354.833_325,  21.699_997,  64.362_348, 14.271_670, 295.637_652,  4.166_990)>]
[<TestCase(2033,  3,  3, 0.1423, 30.0      , 64.0      ,   6.569_042, -42.306_416, 140.876_897,  5.513_053, 219.123_103, 13.299_882)>] // Ankaa star from Phoenix constellation
[<TestCase(1973, 11, 23, 0.0000, 16.813_186, 15.287_672, 247.358_042, -26.432_972, 117.712_111,  5.909_790, 242.287_889, 16.728_253)>] // Antares
let ``risingAndSetting some value returned`` y m d v lat lon ra dec azR utR azS utS =
    let result = 
        risingAndSetting 
            (new DateTime(y, m, d)) 
            v 
            { latitude = lat; longitude = lon } 
            { rightAscension = ra; declination = dec }

    Assert.That((result.Value.rising.azimuth, result.Value.rising.time.TotalHours), Is.EqualTo((azR, utR)).Within(1E-5), "rising")
    Assert.That((result.Value.setting.azimuth, result.Value.setting.time.TotalHours), Is.EqualTo((azS, utS)).Within(1E-5), "setting")

[<TestCase(2010, 8, 24, 0.5667, 76.1, 72.3, 37.581_917,  89.261_806)>] // Polaris
[<TestCase(2033, 3,  3, 0.1423, 53.4, 14.5,  6.569_042, -42.306_416)>] // Ankaa star from Phoenix constellation
let ``risingAndSetting none value returned`` y m d v lat lon ra dec =
    let result = 
        risingAndSetting 
            (new DateTime(y, m, d)) 
            v 
            { latitude = lat; longitude = lon } 
            { rightAscension = ra; declination = dec }

    Assert.IsTrue(result.IsNone)
    
[<TestCase(1900, 1979, 6, 1, 137.679_167, 14.390_278, 138.772_133, 14.063_316)>]
[<TestCase(1950, 1979, 6, 1, 137.679_167, 14.390_278, 138.084_092, 14.269_202)>]
[<TestCase(2000, 1979, 6, 1, 137.679_167, 14.390_278, 137.395_683, 14.475_001)>]
[<TestCase(2050, 1979, 6, 1, 137.679_167, 14.390_278, 136.706_871, 14.680_723)>]
let precessionLowPrecision epoch y m d ra dec raP decP =
    let intEpochToEpoch e =
        match e with
        | 1900 -> J1900
        | 1950 -> J1950
        | 2000 -> J2000
        | 2050 -> J2050
        | _ -> failwith "Wrong epoch"

    let result = 
        precessionLowPrecision 
            (intEpochToEpoch epoch) 
            (new DateTime(y, m, d))
            { rightAscension = ra; declination = dec }
    Assert.That((result.x, result.y), Is.EqualTo(raP, decP).Within(1E-5))

[<TestCase(1988, 9,  1,  5.492_910,  9.241_562)>]
[<TestCase(2028, 5, 15, 14.165_246,  3.942_734)>]
[<TestCase(2012, 2, 29, 17.042_770, -2.807_452)>]
let nutation y m d lon obl =
    let result = nutation (new DateTime(y, m, d))
    Assert.That((result.nutationLongitude, result.nutationObliquity), Is.EqualTo((lon, obl)).Within(1E-5))

[<TestCase(165.563_304, 352.619_472,  -1.549_000, 352.625_126,  -1.548_981)>]
[<TestCase(307.574_473, 175.195_556, -10.017_222, 175.199_453, -10.016_491)>]
[<TestCase(164.047_901,  65.579_722,  35.200_278,  65.580_748,  35.197_031)>]
let aberration sunLon lon lat dLon dLat =
    let result = aberration sunLon { eclLongitude = lon; eclLatitude = lat }
    Assert.That((result.eclLongitude, result.eclLatitude), Is.EqualTo(dLon, dLat).Within(1E-5))