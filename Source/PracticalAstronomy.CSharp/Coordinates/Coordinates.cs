namespace PracticalAstronomy.CSharp;

public class Coordinates
{

    private Coordinates() { }

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => PracticalAstronomy.Coordinates.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => PracticalAstronomy.Coordinates.haToRa(dateTime, longitude, hourAngle);

    public Coord2D EquatorialToHorizontal(Coord2D equatorial, double latitude)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.equatorialToHorizontal(equatorial.X, equatorial.Y, latitude));

    public Coord2D HorizontalToEquatorial(Coord2D horizontal, double latitude)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.horizontalToEquatorial(horizontal.X, horizontal.Y, latitude));

}
