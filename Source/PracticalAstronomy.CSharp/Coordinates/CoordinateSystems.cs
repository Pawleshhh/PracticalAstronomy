namespace PracticalAstronomy.CSharp;

public class CoordinateSystems
{

    internal CoordinateSystems() { }

    public double MeanObliquity(DateTime dateTime)
        => FS.CoordinateSystems.meanObliquity(dateTime);

    public double MeanObliquityWithNutation(DateTime dateTime)
        => FS.CoordinateSystems.meanObliquityWithNutation(dateTime);

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => FS.CoordinateSystems.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => FS.CoordinateSystems.haToRa(dateTime, longitude, hourAngle);

    public Horizon EquatorialToHorizon(double latitude, EquatorialHourAngle eq)
    {
        var result = FS.CoordinateSystems
            .eqToHor(latitude, new CoordinateDataTypes.EquatorialHourAngle(eq.HourAngle, eq.Declination));

        return new Horizon(result.azimuth, result.altitude);
    }

    public EquatorialHourAngle HorizonToEquatorial(double latitude, Horizon hor)
    {
        var result = FS.CoordinateSystems
            .horToEq(latitude, new CoordinateDataTypes.Horizon(hor.Azimuth, hor.Altitude));

        return new EquatorialHourAngle(result.hourAngle, result.declination);
    }

    public EquatorialRightAscension EclipticToEquatorial(DateTime dateTime, Ecliptic ecl)
    {
        var result = FS.CoordinateSystems
            .eclToEq(dateTime, new CoordinateDataTypes.Ecliptic(ecl.Longitude, ecl.Latitude));

        return new EquatorialRightAscension(result.rightAscension, result.declination);
    }

    public Ecliptic EclipticToEquatorial(DateTime dateTime, EquatorialRightAscension eq)
    {
        var result = FS.CoordinateSystems
            .eqToEcl(dateTime, new CoordinateDataTypes.EquatorialRightAscension(eq.RightAscension, eq.Declination));

        return new Ecliptic(result.eclLongitude, result.eclLatitude);
    }

    public EquatorialRightAscension GalacticToEquatorial(Galactic gal)
    {
        var result = FS.CoordinateSystems
            .galToEq(new CoordinateDataTypes.Galactic(gal.Longitude, gal.Latitude));

        return new EquatorialRightAscension(result.rightAscension, result.declination);
    }

    public Galactic EquatorialToGalactic(EquatorialRightAscension eq)
    {
        var result = FS.CoordinateSystems
            .eqToGal(new CoordinateDataTypes.EquatorialRightAscension(eq.RightAscension, eq.Declination));

        return new Galactic(result.galLongitude, result.galLatitude);
    }

    public double CelestialAngle(EquatorialRightAscension eq1, EquatorialRightAscension eq2)
        => FS.CoordinateSystems.celestialAngleEq(
            eq1.ToFSharp(),
            eq2.ToFSharp());

    public double CelestialAngle(Ecliptic ecl1, Ecliptic ecl2)
        => FS.CoordinateSystems.celestialAngleEcl(
            ecl1.ToFSharp(),
            ecl2.ToFSharp());

    public RisingAndSetting? RisingAndSetting(DateTime dateTime, double verticalShift, Geographic geo, EquatorialRightAscension eq)
    {
        var result = FS.CoordinateSystems.risingAndSetting(
                        dateTime,
                        verticalShift,
                        geo.ToFSharp(),
                        eq.ToFSharp());

        if (result?.Value is null)
        {
            return null;
        }

        return new RisingAndSetting(
            new RisingAndSettingData(result.Value.rising.azimuth, result.Value.rising.time),
            new RisingAndSettingData(result.Value.setting.azimuth, result.Value.setting.time));
    }

    public ICoordinateSystem PrecessionLowPrecision(Epoch epoch, DateTime dateTime, EquatorialRightAscension eq)
    {
        var result = FS.CoordinateSystems.precessionLowPrecision(epoch.ToFSharpEpoch(), dateTime, eq.ToFSharp());

        return ICoordinateSystem.Create(result.x, result.y);
    }

    public Nutation Nutation(DateTime dateTime)
    {
        var result = FS.CoordinateSystems.nutation(dateTime);

        return new Nutation(result.nutationLongitude, result.nutationObliquity);
    }

    public Ecliptic Aberration(double sunLongitude, Ecliptic ecl)
    {
        var result = FS.CoordinateSystems.aberration(sunLongitude, ecl.ToFSharp());

        return new Ecliptic(result.eclLongitude, result.eclLatitude);
    }

    public EquatorialHourAngle Refraction(double temperature, double pressure, Geographic geo, EquatorialHourAngle eqHa)
    {
        var result = FS.CoordinateSystems.refraction(temperature, pressure, geo.ToFSharp(), eqHa.ToFSharp());

        return new EquatorialHourAngle(result.hourAngle, result.declination);
    }

}
