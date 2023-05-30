namespace PracticalAstronomy.CSharp;

internal record BaseCoordinateSystem(double X, double Y) : ICoordinateSystem;

public interface ICoordinateSystem
{
    public double X { get; }
    public double Y { get; }

    public static ICoordinateSystem Create(double X, double Y)
        => new BaseCoordinateSystem(X, Y);
}

public abstract record FSharpData<T>
{
    internal abstract T ToFSharp();
}

public record EquatorialHourAngle(double HourAngle, double Declination) : FSharpData<FS.CoordinateDataTypes.EquatorialHourAngle>, ICoordinateSystem
{
    double ICoordinateSystem.X => HourAngle;
    double ICoordinateSystem.Y => Declination;

    internal override CoordinateDataTypes.EquatorialHourAngle ToFSharp()
    {
        return new CoordinateDataTypes.EquatorialHourAngle(HourAngle, Declination);
    }
}

public record EquatorialRightAscension(double RightAscension, double Declination) : FSharpData<FS.CoordinateDataTypes.EquatorialRightAscension>, ICoordinateSystem
{
    double ICoordinateSystem.X => RightAscension;
    double ICoordinateSystem.Y => Declination;

    internal override CoordinateDataTypes.EquatorialRightAscension ToFSharp()
    {
        return new CoordinateDataTypes.EquatorialRightAscension(RightAscension, Declination);
    }
}

public record Horizon(double Azimuth, double Altitude) : FSharpData<FS.CoordinateDataTypes.Horizon>, ICoordinateSystem
{
    double ICoordinateSystem.X => Azimuth;
    double ICoordinateSystem.Y => Altitude;

    internal override CoordinateDataTypes.Horizon ToFSharp()
    {
        return new CoordinateDataTypes.Horizon(Azimuth, Altitude);
    }
}

public record Ecliptic(double Longitude, double Latitude) : FSharpData<FS.CoordinateDataTypes.Ecliptic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Longitude;
    double ICoordinateSystem.Y => Latitude;

    internal override CoordinateDataTypes.Ecliptic ToFSharp()
    {
        return new CoordinateDataTypes.Ecliptic(Longitude, Latitude);
    }
}

public record Galactic(double Longitude, double Latitude) : FSharpData<FS.CoordinateDataTypes.Galactic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Longitude;
    double ICoordinateSystem.Y => Latitude;

    internal override CoordinateDataTypes.Galactic ToFSharp()
    {
        return new CoordinateDataTypes.Galactic(Longitude, Latitude);
    }
}

public record Geographic(double Latitude, double Longitude) : FSharpData<FS.CoordinateDataTypes.Geographic>, ICoordinateSystem
{
    double ICoordinateSystem.X => Latitude;
    double ICoordinateSystem.Y => Longitude;

    internal override CoordinateDataTypes.Geographic ToFSharp()
    {
        return new CoordinateDataTypes.Geographic(Latitude, Longitude);
    }
}

public record RisingAndSettingData(double Azimuth, TimeSpan Time) : FSharpData<FS.CoordinateDataTypes.RisingAndSettingData>
{
    internal override CoordinateDataTypes.RisingAndSettingData ToFSharp()
    {
        return new CoordinateDataTypes.RisingAndSettingData(Azimuth, Time);
    }
}
public record RisingAndSetting(RisingAndSettingData Rising, RisingAndSettingData Setting): FSharpData<FS.CoordinateDataTypes.RisingAndSetting>
{
    internal override CoordinateDataTypes.RisingAndSetting ToFSharp()
    {
        return new CoordinateDataTypes.RisingAndSetting(Rising.ToFSharp(), Setting.ToFSharp());
    }
}

public record Nutation(double NutationLongitude, double NutationObliquity) : FSharpData<FS.CoordinateDataTypes.Nutation>
{
    internal override CoordinateDataTypes.Nutation ToFSharp()
    {
        return new CoordinateDataTypes.Nutation(NutationLongitude, NutationObliquity);
    }
}