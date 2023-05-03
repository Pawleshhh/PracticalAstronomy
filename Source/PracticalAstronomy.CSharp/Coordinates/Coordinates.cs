namespace PracticalAstronomy.CSharp;

using FCoordinates = PracticalAstronomy.Coordinates;

public class Coordinates
{

    private Coordinates() { }

    public Coord2D GeneralisedTransformation(CoordConversion conversion, Coord2D coord)
        => FCoordinates.generalisedTransformation(conversion.ToFSharp(), coord.X, coord.Y).ToCoord2D();

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => FCoordinates.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => FCoordinates.haToRa(dateTime, longitude, hourAngle);

    public Coord2D EquatorialToHorizontal(Coord2D equatorial, double latitude)
        => FCoordinates.equatorialToHorizontal(latitude, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D HorizontalToEquatorial(Coord2D horizontal, double latitude)
        => FCoordinates.horizontalToEquatorial(latitude, horizontal.X, horizontal.Y).ToCoord2D();

    public double MeanObliquity(DateTime dateTime)
        => FCoordinates.meanObliquity(dateTime);

    public Coord2D EclipticToEquatorial(Coord2D ecliptic, DateTime dateTime)
        => FCoordinates.eclitpicToEquatorial(dateTime, ecliptic.X, ecliptic.Y).ToCoord2D();

    public Coord2D EquatorialToEcliptic(Coord2D equatorial, DateTime dateTime)
        => FCoordinates.equatorialToEcliptic(dateTime, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D EquatorialToGalactic(Coord2D equatorial)
        => FCoordinates.equatorialToGalactic(equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D GalacticToEquatorial(Coord2D galactic)
        => FCoordinates.galacticToEquatorial(galactic.X, galactic.Y).ToCoord2D();

    public double CelestialAngle(Coord2D celestialObj1, Coord2D celestialObj2)
        => FCoordinates.celestialAngle(celestialObj1.X, celestialObj1.Y, celestialObj2.X, celestialObj2.Y);

    public bool NeverRises(double v, double latitude, double declination)
        => FCoordinates.neverRises(v, latitude, declination);

    public bool IsCircumpolar(double v, double latitude, double declination)
        => FCoordinates.isCircumpolar(v, latitude, declination);

    public RisingAndSetting? RisingAndSetting(DateTime dateTime, double v, Coord2D geographic, Coord2D equatorial)
    {
        var result = FCoordinates.risingAndSetting(dateTime, v, geographic.X, geographic.Y, equatorial.X, equatorial.Y);
        var rising = result.Item1.OptionToNullable();
        var setting = result.Item2.OptionToNullable();

        if (rising is null || setting is null)
        {
            return null;
        }

        return new((rising.Item1, rising.Item2), (setting.Item1, setting.Item2));
    }

    public Coord2D PrecessionLowPrecision(Epoch epoch, double year, Coord2D equatorial)
    {
        var (ra, dec) = equatorial;
        var fshEpoch = EpochToFSharp(epoch);

        return FCoordinates.precessionLowPrecision(fshEpoch, year, ra, dec).ToCoord2D();
    }

    public Coord2D PrecessionRigorousMethod(Epoch epoch, double year, Coord2D equatorial)
    {
        var (ra, dec) = equatorial;
        var fshEpoch = EpochToFSharp(epoch);

        return FCoordinates.precessionRigorousMethod(fshEpoch, year, ra, dec).ToCoord2D();
    }

    public Coord2D Nutation(DateTime dateTime)
    {
        return FCoordinates.nutation(dateTime).ToCoord2D();
    }

    public Coord2D Abberration(double sunLongitude, Coord2D ecliptic)
    {
        return FCoordinates.abberration(sunLongitude, ecliptic.X, ecliptic.Y).ToCoord2D();
    }

    public Coord2D Refraction(double temperature, double pressure, Coord2D horizon)
    {
        return FCoordinates.refraction(temperature, pressure, horizon.X, horizon.Y).ToCoord2D();
    }

    public (double PSin, double PCos) GeocentricParallax(double height, double geographicLatitude)
    {
        var result = FCoordinates.geocentricParallax(height, geographicLatitude);
        return (result.Item1,  result.Item2);
    }

    public Coord2D ParallaxCorrectionOfMoon(
        DateTime dateTime, 
        double height, 
        Coord2D geographic, 
        double moonParallax, 
        Coord2D equatorial)
    {
        return FCoordinates.parallaxCorrectionOfMoon(
            dateTime,
            height,
            geographic.X, geographic.Y,
            moonParallax,
            equatorial.X, equatorial.Y).ToCoord2D();
    }

    public Coord2D ParallaxCorrection(
        DateTime dateTime,
        double height,
        Coord2D geographic,
        double distance,
        Coord2D equatorial)
    {
        return FCoordinates.parallaxCorrection(
            dateTime,
            height,
            geographic.X, geographic.Y,
            distance,
            equatorial.X, equatorial.Y).ToCoord2D();
    }

    public Coord2D CentreOfSolarDisc(DateTime dateTime, double geocentricLongitude)
        => FCoordinates.centreOfSolarDisc(dateTime, geocentricLongitude).ToCoord2D();

    public double PositionAngleOfSunRotationAxis(
        DateTime dateTime, 
        double obliquityOfEcliptic, 
        double geocentricLongitude)
        => FCoordinates.positionAngleOfSunRotationAxis(dateTime, obliquityOfEcliptic, geocentricLongitude);

    public Coord2D SunspotPositionToHeliographic(
        DateTime dateTime, 
        double obliquityOfEcliptic,
        double geocentricLongitude,
        double angularRadius, 
        Coord2D position)
        => FCoordinates.sunspotPositionToHeliographic(dateTime, obliquityOfEcliptic, geocentricLongitude, angularRadius, position.X, position.Y).ToCoord2D();

    public int CarringtonRotationNumber(DateTime dateTime)
        => FCoordinates.carringtionRotationNumber(dateTime);

    public Coord2D CentreOfMoon(DateTime dateTime, Coord2D moonGeocentric)
        => FCoordinates.centreOfMoon(dateTime, moonGeocentric.X, moonGeocentric.Y).ToCoord2D();

    public double PositionAngleOfMoonRotationAxis(DateTime dateTime, double obliquity, Coord2D moonGeocentric)
        => FCoordinates.positionAngleOfMoonRotationAxis(dateTime, obliquity, moonGeocentric.X, moonGeocentric.Y);

    public Coord3D SelenographicCoordsOfSun(
        DateTime dateTime,
        double moonParallax,
        double sunEarthDistance,
        double geocentricSunLongitude,
        Coord2D moonHeliocentric)
        => FCoordinates.selenographicCoordsOfSun(
            dateTime,
            moonParallax,
            sunEarthDistance,
            geocentricSunLongitude,
            moonHeliocentric.X,
            moonHeliocentric.Y).ToCoord3D();

    public double AtmosphericExtinction(double zenithAngle)
        => FCoordinates.atmosphericExtinction(zenithAngle);

    private static Epochs EpochToFSharp(Epoch epoch)
        => epoch switch
        {
            Epoch.J1900 => Epochs.J1900,
            Epoch.J1950 => Epochs.J1950,
            Epoch.J2000 => Epochs.J2000,
            Epoch.J2050 => Epochs.J2050,
            _ => throw new InvalidOperationException($"Unrecognized epoch of {epoch}")
        };

}
