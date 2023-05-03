module PracticalAstronomy.Test.CoordinatesTest

open System
open NUnit.Framework
open PracticalAstronomy
open PracticalAstronomy.CoordinateSystemTypes
open PracticalAstronomy.Coordinates

let private assertThat<'a, 'b> (result: 'a) (actual: 'b) delta =
    Assert.That(result, Is.EqualTo(actual).Within(delta))

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 278.627_481)>]
let ``generalisedTransformation HaToEq`` ha dec st ra =
    let result = generalisedTransformation (HaToEq(TimeSpan.FromHours(st))) (ha, dec)
    assertThat result (ra, dec) 1E-6
    
[<TestCase(147.558_5835, 42.124_235, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation HaToHor`` ha dec lat az alt =
    let result = generalisedTransformation (HaToHor(lat)) (ha, dec)
    assertThat result (az, alt) 1E-6

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation HaToEcl`` ha dec st mo lon lat =
    let result = generalisedTransformation (HaToEcl(TimeSpan.FromHours(st), mo)) (ha, dec)
    assertThat result (lon, lat) 1E-6

[<TestCase(147.558_5835, 42.124_235, 4.412_4043, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation HaToGal`` ha dec st lon lat =
    let result = generalisedTransformation (HaToGal(TimeSpan.FromHours(st))) (ha, dec)
    assertThat result (lon, lat) 1E-5

[<TestCase(278.627_481, 42.124_235, 4.412_4043, 147.558_5835)>]
let ``generalisedTransformation EqToHa`` ra dec st ha =
    let result = generalisedTransformation (EqToHa(TimeSpan.FromHours(st))) (ra, dec)
    assertThat result (ha, dec) 1E-6

[<TestCase(278.627_481, 42.124_235, 4.412_4043, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation EqToHor`` ra dec st lat az alt =
    let result = generalisedTransformation (EqToHor(TimeSpan.FromHours(st), lat)) (ra, dec)
    assertThat result (az, alt) 1E-6

[<TestCase(278.627_481, 42.124_235, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation EqToEcl`` ra dec mo lon lat =
    let result = generalisedTransformation (EqToEcl(mo)) (ra, dec)
    assertThat result (lon, lat) 1E-6

[<TestCase(278.627_481, 42.124_235, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation EqToGal`` ra dec lon lat =
    let result = generalisedTransformation (EqToGal) (ra, dec)
    assertThat result (lon, lat) 1E-5
    
[<TestCase(235.9807995, -61.3127810, -64.0, 147.558_5835, 42.124_235)>]
let ``generalisedTransformation HorToHa`` az alt lat ha dec =
    let result = generalisedTransformation (HorToHa(lat)) (az, alt)
    assertThat result (ha, dec) 1E-6

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 278.627_481, 42.124_235)>]
let ``generalisedTransformation HorToEq`` az alt st lat ra dec =
    let result = generalisedTransformation (HorToEq(TimeSpan.FromHours(st), lat)) (az, alt)
    assertThat result (ra, dec) 1E-6

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation HorToEcl`` az alt st geoLat mo lon lat =
    let result = generalisedTransformation (HorToEcl(TimeSpan.FromHours(st), geoLat, mo)) (az, alt)
    assertThat result (lon, lat) 1E-6

[<TestCase(235.9807995, -61.3127810, 4.412_4043, -64.0, 70.788_4839, 20.504_4140)>]
let ``generalisedTransformation HorToGal`` az alt st geoLat lon lat =
    let result = generalisedTransformation (HorToGal(TimeSpan.FromHours(st), geoLat)) (az, alt)
    assertThat result (lon, lat) 1E-5

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 278.627_481, 42.124_235)>] //Date: 2009/07/06
let ``generalisedTransformation EclToEq`` lon lat mo ra dec =
    let result = generalisedTransformation (EclToEq(mo)) (lon, lat)
    assertThat result (ra, dec) 1E-6

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 4.412_4043, 147.558_5835, 42.124_235)>] //Date: 2009/07/06
let ``generalisedTransformation EclToHa`` lon lat mo st ha dec =
    let result = generalisedTransformation (EclToHa(mo, TimeSpan.FromHours(st))) (lon, lat)
    assertThat result (ha, dec) 1E-6

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 4.412_4043, -64.0, 235.9807995, -61.3127810)>] //Date: 2009/07/06
let ``generalisedTransformation EclToHor`` lon lat mo st geoLat az alt =
    let result = generalisedTransformation (EclToHor(mo, TimeSpan.FromHours(st), geoLat)) (lon, lat)
    assertThat result (az, alt) 1E-6

[<TestCase(285.325_7326, 65.105_1288, 23.439_2318, 70.788_4839, 20.504_4140)>] //Date: 2009/07/06
let ``generalisedTransformation EclToGal`` lon lat mo galLon galLat =
    let result = generalisedTransformation (EclToGal(mo)) (lon, lat)
    assertThat result (galLon, galLat) 1E-5

[<TestCase(70.788_4839, 20.504_4140, 278.627_481, 42.124_235)>]
let ``generalisedTransformation GalToEq`` lon lat ra dec =
    let result = generalisedTransformation (GalToEq) (lon, lat)
    assertThat result (ra, dec) 1E-5

[<TestCase(70.788_4839, 20.504_4140, 4.412_4043, 147.558_5835, 42.124_235)>]
let ``generalisedTransformation GalToHa`` lon lat st ha dec =
    let result = generalisedTransformation (GalToHa(TimeSpan.FromHours(st))) (lon, lat)
    assertThat result (ha, dec) 1E-5

[<TestCase(70.788_4839, 20.504_4140, 4.412_4043, -64.0, 235.9807995, -61.3127810)>]
let ``generalisedTransformation GalToHor`` lon lat st geoLat az alt =
    let result = generalisedTransformation (GalToHor(TimeSpan.FromHours(st), geoLat)) (lon, lat)
    assertThat result (az, alt) 1E-5

[<TestCase(70.788_4839, 20.504_4140, 23.439_2318, 285.325_7326, 65.105_1288)>] //Date: 2009/07/06
let ``generalisedTransformation GalToEcl`` galLon galLat mo lon lat =
    let result = generalisedTransformation (GalToEcl(mo)) (galLon, galLat)
    assertThat result (lon, lat) 1E-5

[<TestCase(278.087_505, -64.0, 1980, 4, 22, 18.614_353 , 148.098_555)>]
[<TestCase(188.087_501,  15.0, 1980, 4, 22, 21.614_353 ,   2.221_770)>]
[<TestCase( 13.508_334,  15.0, 2030, 9,  7, 21.614_3528, 312.712_491)>]
let raToHa ra longitude year month day time ha =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = raToHa dateTime longitude ra
    assertThat result ha 1E-5

[<TestCase(148.098_585 , -64.0, 1980, 4, 22, 18.614_353 , 278.087_475 )>]
[<TestCase(137.848_584 ,  27.0, 1999, 4, 22,  4.614_3528, 168.169_5765)>]
[<TestCase(358.848_5835,  27.0, 2066, 1,  2,  3.997_6861, 190.235_5485)>]
let haToRa ha longitude year month day time ra =
    let dateTime = (new DateTime(year, month, day)).AddHours(time)
    let result = haToRa dateTime longitude ha
    assertThat result ra 1E-5

[<TestCase(87.93333, 23.219_444, 52.0, 283.271_027, 19.334_345)>]
let equatorialToHorizontal ha dec latitude az alt =
    let result = equatorialToHorizontal latitude (ha, dec)
    assertThat result (az, alt) 1E-5

[<TestCase(283.271_028, 19.334_344, 52.0, 87.93333, 23.219_444)>]
let horizontalToEquatorial az alt latitude ha dec =
    let result = horizontalToEquatorial latitude (az, alt)
    assertThat result (ha, dec) 1E-5

[<TestCase(2009, 7, 6, 23.438_055_31)>]
let meanObliquity year month day meanObl = 
    let result = meanObliquity (new DateTime(year, month, day))
    assertThat result meanObl 1E-8

[<TestCase(139.686_1111, 4.875_277_778, 2009, 7, 6, 143.7225092, 19.535_699_24)>]
let eclipticToEquatorial lon lat year month day ra dec =
    let result = eclitpicToEquatorial (new DateTime(year, month, day)) (lon, lat)
    assertThat result (ra, dec) 1E-3

[<TestCase(9.581_4778, 19.535_003, 2009, 7, 6, 139.686_1027, 4.875_275_723)>]
let equatorialToEcliptic ra dec year month day lon lat =
    let result = equatorialToEcliptic (new DateTime(year, month, day)) (ra, dec)
    assertThat result (lon, lat) 1E-5

[<TestCase(155.25, 10.053_055_56, 232.247_8835, 51.122_2678)>]
let equatorialToGalactic ra dec lon lat =
    let result = equatorialToGalactic (ra, dec)
    assertThat result (lon, lat) 1E-5

[<TestCase(232.247_778, 51.122_222, 155.249925, 10.053_087)>]
let galacticToEquatorial lon lat ra dec =
    let result = galacticToEquatorial (lon, lat)
    assertThat result (ra, dec) 1E-5

[<TestCase(78.382_080, 101.055_840, -8.225_000, -16.686_389, 23.673_850)>]
let celestialAngle x1 x2 y1 y2 angle = 
    let result = celestialAngle (x1, y1) (x2, y2)
    assertThat result angle 1E-5

[<TestCase(2010, 8, 24, 30.0, 64.0, 23.655_558, 21.7, 0.5667, 14.271_6699, 64.362_348, 4.166_990_15, 295.637_652)>]
let risingAndSetting y m d lat lon ra dec v r ar s as' =
    let result = risingAndSetting (new DateTime(y, m, d)) v (lat, lon) (ra, dec)

    match result with
    | (Some(r', ar'), Some(s', as'')) ->
        assertThat (r'.TotalHours, s'.TotalHours) (r, s) 1E-3
        assertThat (ar', as'') (ar, as') 1E-3
    | _ -> Assert.Fail()

let private indexToEpoch e =
    match e with
        | 1 -> Epochs.J1900 | 2 -> Epochs.J1950 
        | 3 -> Epochs.J2000 | 4 -> Epochs.J2050
        | _ -> failwith "Wrong index of an epoch"

[<TestCase(2, 1979.5, 137.679_167, 14.390_278, 138.085_29, 14.268_842)>]
let precessionLowPrecision e y r d ra dec =
    let epoch = indexToEpoch e
    let result = precessionLowPrecision epoch y (r, d)
    assertThat result (ra, dec) 1E-5

[<TestCase(2, 1979.5, 137.679_167, 14.390_278, 138.083_991, 14.268_792)>]
let precessionRigorousMethod e y r d ra dec =
    let epoch = indexToEpoch e
    let result = precessionRigorousMethod epoch y (r, d)
    assertThat result (ra, dec) 1E-2 // The low threshold here is due to the fact that the function calculates epoch2 date time
                                     // more precise so the result differs from the book where date time is rounded to the month only

[<TestCase(1988, 09, 01, 5.492910079, 9.24156161)>]                                                            
let nutation y m d l o =
    let result = nutation (new DateTime(y, m, d))
    assertThat result (l, o) 1E-5

[<TestCase(165.562_250, 352.619_472, -1.549, 352.625_126, -1.548_981)>]
let abberration sunLon lon lat elon elat =
    let result = abberration sunLon (lon, lat)
    assertThat result (elon, elat) 1E-5

[<TestCase(13.0, 1008.0, 283.271_027, 19.334_345, 19.379_748)>]
let refraction t p az alt ealt =
    let (raz, ralt) = refraction t p (az, alt)
    assertThat ralt ealt 1E-05
    assertThat raz az 0

[<TestCase(60.0, 50.0, 0.762_422, 0.644_060)>]
let geocentricParallax h lat pSin pCos =
    let result = geocentricParallax h lat
    assertThat result (pSin, pCos) 1E-5

[<TestCase(1979, 2, 26, 16, 45, 60.0, 50.0, -100.0, 1.019_167, 338.829_165, -7.686_944, 339.180_075, -8.538_165)>]
let parallaxCorrectionOfMoon y m d hr mm height lat lon moonP ra dec era edec =
    let result = parallaxCorrectionOfMoon (new DateTime(y, m, d, hr, mm, 0)) height (lat, lon) moonP (ra, dec)
    assertThat result (era, edec) 1E-5

[<TestCase(1979, 2, 26, 16, 45, 60.0, 50.0, -100.0, 0.9901, 339.183_33, -8.74, 339.184_185, -8.742_064)>]
let parallaxCorrection y m d hr mm height lat lon distance ra dec era edec =
    let result = parallaxCorrection (new DateTime(y, m, d, hr, mm, 0)) height (lat, lon) distance (ra, dec)
    assertThat result (era, edec) 1E-5

[<TestCase(1988, 5, 1, 40.843_611, 103.47, -4.13)>]
let centreOfSolarDisc y m d l l0 b0 =
    let result = centreOfSolarDisc (new DateTime(y, m, d)) l
    assertThat result (l0, b0) 1E-2
    
[<TestCase(1988, 5, 1, 23.442, 40.843_611, -24.127_321)>]
let positionAngleOfSunRotationAxis y m d o l p =
    let result = positionAngleOfSunRotationAxis (new DateTime(y, m, d)) o l
    assertThat result p 1E-5
    
[<TestCase(1988, 5, 1, 23.442, 40.843_611, 15.867, 220.0, 10.5, -19.945, 142.611)>]
let sunspotPositionToHeliographic y m d o g a t r b l =
    let result = sunspotPositionToHeliographic (new DateTime(y, m, d)) o g a (t, r)
    assertThat result (b, l) 1E-2

[<TestCase(1975, 1, 27, 1624)>]
let carringtionRotationNumber y m d n =
    let result = carringtionRotationNumber (new DateTime(y, m, d))
    assertThat result n 0

[<TestCase(1988, 5, 1, 209.12, -3.08, -4.88, 4.04)>]
let centreOfMoon y m d lam bet le be =
    let result = centreOfMoon (new DateTime(y, m, d)) (lam, bet)
    assertThat result (le, be) 1E-2

[<TestCase(1988, 5, 1, 23.4433, 209.12, -3.08, 19.78)>]
let positionAngleOfMoonRotationAxis y m d o lam bet c =
    let result = positionAngleOfMoonRotationAxis (new DateTime(y, m, d)) o (lam, bet)
    assertThat result c 1E-2

[<TestCase(1988, 5, 1, 55.952, 1.0076, 40.8437, 209.12, -3.08, 6.811, 83.19, 1.19)>]
let selenographicCoordsOfSun y m d mp dist sl lm b ls cl bs =
    let result = selenographicCoordsOfSun (new DateTime(y, m, d)) mp dist sl (lm, b)
    assertThat result (ls, cl, bs) 1E-2

[<TestCase(75.0, 0.8)>]
let atmosphericExtinction z m =
    let result = atmosphericExtinction z
    assertThat result m 1E-1