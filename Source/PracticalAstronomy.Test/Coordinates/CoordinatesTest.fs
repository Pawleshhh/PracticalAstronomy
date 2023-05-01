﻿module PracticalAstronomy.Test.CoordinatesTest

open System
open NUnit.Framework
open PracticalAstronomy
open PracticalAstronomy.CoordinateSystemTypes
open PracticalAstronomy.Coordinates

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

[<TestCase(2010, 8, 24, 30.0, 64.0, 23.655_558, 21.7, 0.5667, 14.271_6699, 64.362_348, 4.166_990_15, 295.637_652)>]
let risingAndSetting (y: int) (m: int) (d: int) (lat: float) (lon: float) (ra: float) (dec: float) (v: float) (r: float) (ar: float) (s: float) (as': float) =
    let result = risingAndSetting (new DateTime(y, m, d)) v (lat, lon) (ra, dec)

    match result with
    | (Some(r', ar'), Some(s', as'')) ->
        Assert.That((r'.TotalHours, s'.TotalHours), Is.EqualTo(r, s).Within(1E-3))
        Assert.That((ar', as''), Is.EqualTo(ar, as').Within(1E-3))
    | _ -> Assert.Fail()

let private indexToEpoch e =
    match e with
        | 1 -> Epochs.J1900 | 2 -> Epochs.J1950 
        | 3 -> Epochs.J2000 | 4 -> Epochs.J2050
        | _ -> failwith "Wrong index of an epoch"

[<TestCase(2, 1979.5, 137.679_167, 14.390_278, 138.085_29, 14.268_842)>]
let precessionLowPrecision (e: int) (y: float) (r: float) (d: float) (ra: float) (dec: float) =
    let epoch = indexToEpoch e
    let result = precessionLowPrecision epoch y (r, d)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-5))

[<TestCase(2, 1979.5, 137.679_167, 14.390_278, 138.083_991, 14.268_792)>]
let precessionRigorousMethod (e: int) (y: float) (r: float) (d: float) (ra: float) (dec: float) =
    let epoch = indexToEpoch e
    let result = precessionRigorousMethod epoch y (r, d)
    Assert.That(result, Is.EqualTo((ra, dec)).Within(1E-2)) // The low threshold here is due to the fact that the function calculates epoch2 date time
                                                            // more precise so the result differs from the book where date time is rounded to the month only

[<TestCase(1988, 09, 01, 5.492910079, 9.24156161)>]                                                            
let nutation (y: int) (m: int) (d: int) (l: float) (o: float) =
    let result = nutation (new DateTime(y, m, d))
    Assert.That(result, Is.EqualTo((l, o)).Within(1E-5))

[<TestCase(165.562_250, 352.619_472, -1.549, 352.625_126, -1.548_981)>]
let abberration (sunLon: float) (lon: float) (lat: float) (elon: float) (elat: float) =
    let result = abberration sunLon (lon, lat)
    Assert.That(result, Is.EqualTo((elon, elat)).Within(1E-5))

[<TestCase(13.0, 1008.0, 283.271_027, 19.334_345, 19.379_748)>]
let refraction (t: float) (p: float) (az: float) (alt: float) (ealt: float) =
    let (raz, ralt) = refraction t p (az, alt)
    Assert.That(ralt, Is.EqualTo(ealt).Within(1E-05))
    Assert.That(raz, Is.EqualTo(az))

[<TestCase(60.0, 50.0, 0.762_422, 0.644_060)>]
let geocentricParallax (h: float) (lat: float) (pSin: float) (pCos: float) =
    let result = geocentricParallax h lat
    Assert.That(result, Is.EqualTo((pSin, pCos)).Within(1E-5))

[<TestCase(1979, 2, 26, 16, 45, 60.0, 50.0, -100.0, 1.019_167, 338.829_165, -7.686_944, 339.180_075, -8.538_165)>]
let parallaxCorrectionOfMoon (y: int) (m: int) (d: int) (hr: int) (mm: int) (height: float) (lat: float) (lon: float) (moonP: float) (ra: float) (dec: float) (era: float) (edec: float) =
    let result = parallaxCorrectionOfMoon (new DateTime(y, m, d, hr, mm, 0)) height (lat, lon) moonP (ra, dec)
    Assert.That(result, Is.EqualTo((era, edec)).Within(1E-5))

[<TestCase(1979, 2, 26, 16, 45, 60.0, 50.0, -100.0, 0.9901, 339.183_33, -8.74, 339.184_185, -8.742_064)>]
let parallaxCorrection (y: int) (m: int) (d: int) (hr: int) (mm: int) (height: float) (lat: float) (lon: float) (distance: float) (ra: float) (dec: float) (era: float) (edec: float) =
    let result = parallaxCorrection (new DateTime(y, m, d, hr, mm, 0)) height (lat, lon) distance (ra, dec)
    Assert.That(result, Is.EqualTo((era, edec)).Within(1E-5))

[<TestCase(1988, 5, 1, 40.843_611, 103.47, -4.13)>]
let centreOfSolarDisc (y: int) (m: int) (d: int) (l: float) (l0: float) (b0: float) =
    let result = centreOfSolarDisc (new DateTime(y, m, d)) l
    Assert.That(result, Is.EqualTo((l0, b0)).Within(1E-2))
    
[<TestCase(1988, 5, 1, 23.442, 40.843_611, -24.127_321)>]
let positionAngleOfSunRotationAxis (y: int) (m: int) (d: int) (o: float) (l: float) (p: float) =
    let result = positionAngleOfSunRotationAxis (new DateTime(y, m, d)) o l
    Assert.That(result, Is.EqualTo(p).Within(1E-5))
    
[<TestCase(1988, 5, 1, 23.442, 40.843_611, 15.867, 220.0, 10.5, -19.945, 142.611)>]
let sunspotPositionToHeliographic (y: int) (m: int) (d: int) (o: float) (g: float) (a: float) (t: float) (r: float) (b: float) (l: float) =
    let result = sunspotPositionToHeliographic (new DateTime(y, m, d)) o g a (t, r)
    Assert.That(result, Is.EqualTo((b, l)).Within(1E-2))

[<TestCase(1975, 1, 27, 1624)>]
let carringtionRotationNumber (y: int) (m: int) (d: int) (n: int) =
    let result = carringtionRotationNumber (new DateTime(y, m, d))
    Assert.That(result, Is.EqualTo(n))

[<TestCase(1988, 5, 1, 209.12, -3.08, -4.88, 4.04)>]
let centreOfMoon (y: int) (m: int) (d: int) (lam: float) (bet: float) (le: float) (be: float) =
    let result = centreOfMoon (new DateTime(y, m, d)) (lam, bet)
    Assert.That(result, Is.EqualTo((le, be)).Within(1E-2))

[<TestCase(1988, 5, 1, 23.4433, 209.12, -3.08, 19.78)>]
let positionAngleOfMoonRotationAxis (y: int) (m: int) (d: int) (o: float) (lam: float) (bet: float) (c: float) =
    let result = positionAngleOfMoonRotationAxis (new DateTime(y, m, d)) o (lam, bet)
    Assert.That(result, Is.EqualTo(c).Within(1E-2))