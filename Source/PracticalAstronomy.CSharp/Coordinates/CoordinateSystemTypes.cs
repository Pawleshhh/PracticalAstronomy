namespace PracticalAstronomy.CSharp;

using FConversion = CoordinateSystemTypes.CoordConversion;

public record Coord2D(double X, double Y)
{
    public static Coord2D FromTuple(Tuple<double, double> tuple)
        => new Coord2D(tuple.Item1, tuple.Item2);
}

public record Coord3D(double X, double Y, double Z);

public abstract record CoordConversion{
    internal abstract FConversion ToFSharp();
}
public record HaToEq(TimeSpan SiderealTime) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHaToEq(SiderealTime);
}

public record HaToHor(double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHaToHor(Latitude);
}
public record HaToEcl(TimeSpan SiderealTime, double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHaToEcl(SiderealTime, MeanObliquity);
}
public record HaToGal(TimeSpan SiderealTime) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHaToGal(SiderealTime);
}
public record EqToHa(TimeSpan SiderealTime) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEqToHa(SiderealTime);
}
public record EqToHor(TimeSpan SiderealTime, double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEqToHor(SiderealTime, Latitude);
}
public record EqToEcl(double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEqToEcl(MeanObliquity);
}
public record EqToGal : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.EqToGal;
}
public record HorToHa(double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHorToHa(Latitude);
}
public record HorToEq(TimeSpan SiderealTime, double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHorToEq(SiderealTime, Latitude);
}
public record HorToEcl(TimeSpan SiderealTime, double Latitude, double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHorToEcl(SiderealTime, Latitude, MeanObliquity);
}
public record HorToGal(TimeSpan SiderealTime, double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewHorToGal(SiderealTime, Latitude);
}
public record EclToEq(double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEclToEq(MeanObliquity);
}
public record EclToHa(double MeanObliquity, TimeSpan SiderealTime) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEclToHa(MeanObliquity, SiderealTime);
}
public record EclToHor(double MeanObliquity, TimeSpan SiderealTime, double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEclToHor(MeanObliquity, SiderealTime, Latitude);
}
public record EclToGal(double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewEclToGal(MeanObliquity);
}
public record GalToEq : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.GalToEq;
}
public record GalToHa(TimeSpan SiderealTime) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewGalToHa(SiderealTime);
}
public record GalToHor(TimeSpan SiderealTime, double Latitude) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewGalToHor(SiderealTime, Latitude);
}
public record GalToEcl(double MeanObliquity) : CoordConversion{
    internal override FConversion ToFSharp()
        => FConversion.NewGalToEcl(MeanObliquity);
}
