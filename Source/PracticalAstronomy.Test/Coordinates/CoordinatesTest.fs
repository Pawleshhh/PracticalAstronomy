module PracticalAstronomy.Test.CoordinatesTest

open System
open NUnit.Framework
open PracticalAstronomy.Coordinates
open PracticalAstronomy.CoordinateSystemTypes

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 278.627_481)>]
let ``generalisedTransformation HaToEq`` (ha: float) (dec: float) (st: float) (ra: float) =
    let result = generalisedTransformation (HaToEq(TimeSpan.FromHours(st))) (ha, dec)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-6))
    
[<TestCase(147.558_5835, 42.124_235, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation HaToHor`` (ha: float) (dec: float) (lat: float) (az: float) (alt: float) =
    let result = generalisedTransformation (HaToHor(lat)) (ha, dec)
    Assert.That(result, Is.EqualTo((az, alt)).Within(1E-6))

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation HaToEcl`` (ha: float) (dec: float) (st: float) (mo: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (HaToEcl(TimeSpan.FromHours(st), mo)) (ha, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-6))

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation HaToGal`` (ha: float) (dec: float) (st: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (HaToGal(TimeSpan.FromHours(st))) (ha, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(278.627_481, 42.124_235, 4.412_4043, 147.558_5835)>]
let ``generalisedTransformation EqToHa`` (ra: float) (dec: float) (st: float) (ha: float) =
    let result = generalisedTransformation (EqToHa(TimeSpan.FromHours(st))) (ra, dec)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-6))

[<TestCase(278.627_481, 42.124_235, 4.412_4043, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation EqToHor`` (ra: float) (dec: float) (st: float) (lat: float) (az: float) (alt: float) =
    let result = generalisedTransformation (EqToHor(TimeSpan.FromHours(st), lat)) (ra, dec)
    Assert.That(result, Is.EqualTo((az, alt)).Within(1E-6))

[<TestCase(278.627_481, 42.124_235, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation EqToEcl`` (ra: float) (dec: float) (mo: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (EqToEcl(mo)) (ra, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-6))

[<TestCase(278.627_481, 42.124_235, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation EqToGal`` (ra: float) (dec: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (EqToGal) (ra, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))
    
[<TestCase(235.9807995, -61.3127810, -64.0, 147.558_5835, 42.124_235)>]
let ``generalisedTransformation HorToHa`` (az: float) (alt: float) (lat: float) (ha: float) (dec: float) =
    let result = generalisedTransformation (HorToHa(lat)) (az, alt)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-6))

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 278.627_481, 42.124_235)>]
let ``generalisedTransformation HorToEq`` (az: float) (alt: float) (st: float) (lat: float) (ra: float) (dec: float) =
    let result = generalisedTransformation (HorToEq(TimeSpan.FromHours(st), lat)) (az, alt)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-6))

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation HorToEcl`` (az: float) (alt: float) (st: float) (geoLat: float) (mo: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (HorToEcl(TimeSpan.FromHours(st), geoLat, mo)) (az, alt)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-6))

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation HorToGal`` (az: float) (alt: float) (st: float) (geoLat: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (HorToGal(TimeSpan.FromHours(st), geoLat)) (az, alt)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 278.627_481, 42.124_235)>] //Date: 2009/07/06
let ``generalisedTransformation EclToEq`` (lon: float) (lat: float) (mo: float) (ra: float) (dec: float) =
    let result = generalisedTransformation (EclToEq(mo)) (lon, lat)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-6))

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 4.412_4043, 147.558_5835, 42.124_235)>] //Date: 2009/07/06
let ``generalisedTransformation EclToHa`` (lon: float) (lat: float) (mo: float) (st: float) (ha: float) (dec: float) =
    let result = generalisedTransformation (EclToHa(mo, TimeSpan.FromHours(st))) (lon, lat)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-6))

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 4.412_4043, -64.0, 235.9807995, -61.3127810)>] //Date: 2009/07/06
let ``generalisedTransformation EclToHor`` (lon: float) (lat: float) (mo: float) (st: float) (geoLat: float) (az: float) (alt: float) =
    let result = generalisedTransformation (EclToHor(mo, TimeSpan.FromHours(st), geoLat)) (lon, lat)
    Assert.That(result, Is.EqualTo((az, alt)).Within(1E-6))

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 70.788_4839, 20.504_4140)>] //Date: 2009/07/06
let ``generalisedTransformation EclToGal`` (lon: float) (lat: float) (mo: float) (galLon: float) (galLat: float) =
    let result = generalisedTransformation (EclToGal(mo)) (lon, lat)
    Assert.That(result, Is.EqualTo((galLon, galLat)).Within(1E-5))

[<TestCase(70.788_4839, 20.504_4140, 278.627_481, 42.124_235)>]
let ``generalisedTransformation GalToEq`` (lon: float) (lat: float) (ra: float) (dec: float) =
    let result = generalisedTransformation (GalToEq) (lon, lat)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-5))

[<TestCase(70.788_4839, 20.504_4140, 4.412_4043, 147.558_5835, 42.124_235)>]
let ``generalisedTransformation GalToHa`` (lon: float) (lat: float)  (st: float) (ha: float) (dec: float) =
    let result = generalisedTransformation (GalToHa(TimeSpan.FromHours(st))) (lon, lat)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-5))

[<TestCase(70.788_4839, 20.504_4140, 4.412_4043, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation GalToHor`` (lon: float) (lat: float) (st: float) (geoLat: float) (az: float) (alt: float)  =
    let result = generalisedTransformation (GalToHor(TimeSpan.FromHours(st), geoLat)) (lon, lat)
    Assert.That(result, Is.EqualTo((az, alt)).Within(1E-5))

[<TestCase(70.788_4839, 20.504_4140, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation GalToEcl`` (galLon: float) (galLat: float) (mo: float) (lon: float) (lat: float) =
    let result = generalisedTransformation (GalToEcl(mo)) (galLon, galLat)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(278.087505, -64.0, 1980, 4, 22, 18.614_353, 148.098555)>]
let raToHa (ra : float) (longitude : float) (year : int) (month : int) (day : int) (time : float) (ha : float) =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = raToHa dateTime longitude ra
    Assert.That(result, Is.EqualTo(ha).Within(1E-5))

[<TestCase(148.098585, -64.0, 1980, 4, 22, 18.614_353, 278.087475)>]
let haToRa (ha : float) (longitude : float) (year : int) (month : int) (day : int) (time : float) (ra : float) =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = haToRa dateTime longitude ha
    Assert.That(result, Is.EqualTo(ra).Within(1E-5))

[<TestCase(87.93333, 23.219_444, 52.0, 283.271_027, 19.334_345)>]
let equatorialToHorizontal (ha : float) (dec : float) (latitude : float) (az : float) (alt : float) =
    let result = equatorialToHorizontal latitude (ha, dec)
    Assert.That(result, Is.EqualTo((az, alt)).Within(1E-5))

[<TestCase(283.271_028, 19.334_344, 52.0, 87.93333, 23.219_444)>]
let horizontalToEquatorial (az : float) (alt : float) (latitude : float) (ha : float) (dec : float) =
    let result = horizontalToEquatorial latitude (az, alt)
    Assert.That(result, Is.EqualTo((ha, dec)).Within(1E-5))

[<TestCase(2009, 7, 6, 23.438_055_31)>]
let meanObliquity (year : int) (month : int) (day : int) (meanObl : float) = 
    let result = meanObliquity (new DateTime(year, month, day))
    Assert.That(result, Is.EqualTo(meanObl).Within(1E-8))

[<TestCase(139.686_1111, 4.875_277_778, 2009, 7, 6, 143.7225092, 19.535_699_24)>]
let eclipticToEquatorial (lon : float) (lat : float) (year : int) (month : int) (day : int) (ra : float) (dec : float) =
    let result = eclitpicToEquatorial (new DateTime(year, month, day)) (lon, lat)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-3))

[<TestCase(9.581_4778, 19.535_003, 2009, 7, 6, 139.686_1027, 4.875_275_723)>]
let equatorialToEcliptic (ra : float) (dec : float) (year : int) (month : int) (day : int) (lon : float) (lat : float) =
    let result = equatorialToEcliptic (new DateTime(year, month, day)) (ra, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(155.25, 10.053_055_56, 232.247_8835, 51.122_2678)>]
let equatorialToGalactic (ra : float) (dec : float) (lon : float) (lat : float) =
    let result = equatorialToGalactic (ra, dec)
    Assert.That(result, Is.EqualTo((lon, lat)).Within(1E-5))

[<TestCase(232.247_778, 51.122_222, 155.249925, 10.053_087)>]
let galacticToEquatorial (lon : float) (lat : float) (ra : float) (dec : float) =
    let result = galacticToEquatorial (lon, lat)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-5))

[<TestCase(78.382_080, 101.055_840, -8.225_000, -16.686_389, 23.673_850)>]
let celestialAngle (x1 : float) (x2 : float) (y1 : float) (y2 : float) (angle : float) = 
    let result = celestialAngle (x1, y1) (x2, y2)
    Assert.That(result, Is.EqualTo(angle).Within(1E-5))