namespace PracticalAstronomy.CSharp;

internal static class TupleExtensions
{

    public static Coord2D ToCoord2D(this Tuple<double, double> tuple)
        => Coord2D.FromTuple(tuple);

}
