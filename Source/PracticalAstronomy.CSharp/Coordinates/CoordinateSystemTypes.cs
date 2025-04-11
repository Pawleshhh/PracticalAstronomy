namespace PracticalAstronomy.CSharp;

internal record BaseCoordinateSystem(double X, double Y) : ICoordinateSystem;

public interface ICoordinateSystem
{
    public double X { get; }
    public double Y { get; }

    public static ICoordinateSystem Create(double X, double Y)
        => new BaseCoordinateSystem(X, Y);
}

internal interface IFSharpData<T>
{
    public T ToFSharp();
}

public record EquatorialHourAngle(double HourAngle, double Declination) : IFSharpData<FS.CoordinateDataTypes.EquatorialHourAngle>, ICoordinateSystem
{
    double ICoordinateSystem.X => HourAngle;
    double ICoordinateSystem.Y => Declination;

    public CoordinateDataTypes.EquatorialHourAngle ToFSharp()
    {
        return new CoordinateDataTypes.EquatorialHourAngle(HourAngle, Declination);
    }
}

public record EquatorialRightAscension(double RightAscension, double Declination) : IFSharpData<FS.CoordinateDataTypes.EquatorialRightAscension>, ICoordinateSystem
{
    double ICoordinateSystem.X => RightAscension;
    double ICoordinateSystem.Y => Declination;

    public CoordinateDataTypes.EquatorialRightAscension ToFSharp()
    {
        return new CoordinateDataTypes.EquatorialRightAscension(RightAscension, Declination);
    }
}

public record Horizon(double Azimuth, double Altitude) : IFSharpData<FS.CoordinateDataTypes.Horizon>, ICoordinateSystem
{
    double ICoordinateSystem.X => Azimuth;
    double ICoordinateSystem.Y => Altitude;

    public CoordinateDataTypes.Horizon ToFSharp()
    {
        return new CoordinateDataTypes.Horizon(Azimuth, Altitude);
    }
}

public record Ecliptic(double Longitude, double Latitude) : IFSharpData<FS.CoordinateDataTypes.Ecliptic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Longitude;
    double ICoordinateSystem.Y => Latitude;

    public CoordinateDataTypes.Ecliptic ToFSharp()
    {
        return new CoordinateDataTypes.Ecliptic(Longitude, Latitude);
    }
}

public record Galactic(double Longitude, double Latitude) : IFSharpData<FS.CoordinateDataTypes.Galactic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Longitude;
    double ICoordinateSystem.Y => Latitude;

    public CoordinateDataTypes.Galactic ToFSharp()
    {
        return new CoordinateDataTypes.Galactic(Longitude, Latitude);
    }
}

public record Geographic(double Latitude, double Longitude) : IFSharpData<FS.CoordinateDataTypes.Geographic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Latitude;
    double ICoordinateSystem.Y => Longitude;

    public CoordinateDataTypes.Geographic ToFSharp()
    {
        return new CoordinateDataTypes.Geographic(Latitude, Longitude);
    }
}

public record RisingAndSettingData(double Azimuth, TimeSpan Time) : IFSharpData<FS.CoordinateDataTypes.RisingAndSettingData>
{
    public CoordinateDataTypes.RisingAndSettingData ToFSharp()
    {
        return new CoordinateDataTypes.RisingAndSettingData(Azimuth, Time);
    }
}
public record RisingAndSetting(RisingAndSettingData Rising, RisingAndSettingData Setting): IFSharpData<FS.CoordinateDataTypes.RisingAndSetting>
{
    public  CoordinateDataTypes.RisingAndSetting ToFSharp()
    {
        return new CoordinateDataTypes.RisingAndSetting(Rising.ToFSharp(), Setting.ToFSharp());
    }
}

public record Nutation(double NutationLongitude, double NutationObliquity) : IFSharpData<FS.CoordinateDataTypes.Nutation>
{
    public CoordinateDataTypes.Nutation ToFSharp()
    {
        return new CoordinateDataTypes.Nutation(NutationLongitude, NutationObliquity);
    }
}