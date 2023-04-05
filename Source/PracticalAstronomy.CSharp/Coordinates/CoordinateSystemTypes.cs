namespace PracticalAstronomy.CSharp;

public record Coord2D(double X, double Y)
{
    public static Coord2D FromTuple(Tuple<double, double> tuple)
        => new Coord2D(tuple.Item1, tuple.Item2);
}

public record Coord3D(double X, double Y, double Z);

public abstract record CoordConversion
{
    //internal abstract PracticalAstronomy.CoordinateSystemTypes.CoordConversion ToFSharp();
}
public record HaToEq(TimeSpan SiderealTime) : CoordConversion;
public record HaToHor(double Latitude) : CoordConversion;
public record HaToEcl(TimeSpan SiderealTime, double MeanObliquity) : CoordConversion;
public record HaToGal(TimeSpan SiderealTime) : CoordConversion;
public record EqToHa(TimeSpan SiderealTime) : CoordConversion;
public record EqToHor(TimeSpan SiderealTime, double Latitude) : CoordConversion;
public record EqToEcl(double MeanObliquity) : CoordConversion;
public record EqToGal : CoordConversion;
public record HorToHa(double Latitude) : CoordConversion;
public record HorToEq(TimeSpan SiderealTime, double Latitude) : CoordConversion;
public record HorToEcl(TimeSpan SiderealTime, double Latitude, double MeanObliquity) : CoordConversion;
public record HorToGal(TimeSpan SiderealTime, double Latitude) : CoordConversion;
public record EclToEq(double MeanObliquity) : CoordConversion;
public record EclToHa(double MeanObliquity, TimeSpan SiderealTime) : CoordConversion;
public record EclToHor(double MeanObliquity, TimeSpan SiderealTime, double Latitude) : CoordConversion;
public record EclToGal(double MeanObliquity) : CoordConversion;
public record GalToEq : CoordConversion;
public record GalToHa(TimeSpan SiderealTime) : CoordConversion;
public record GalToHor(TimeSpan SiderealTime, double Latitude) : CoordConversion;
public record GalToEcl(double MeanObliquity) : CoordConversion;
