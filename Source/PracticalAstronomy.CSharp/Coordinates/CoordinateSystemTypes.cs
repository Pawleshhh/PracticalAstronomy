namespace PracticalAstronomy.CSharp;

public record Coord2D(double X, double Y)
{
    public static Coord2D FromTuple(Tuple<double, double> tuple)
        => new Coord2D(tuple.Item1, tuple.Item2);
}

public record Coord3D(double X, double Y, double Z);