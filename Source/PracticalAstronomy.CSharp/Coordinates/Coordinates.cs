namespace PracticalAstronomy.CSharp;

public class Coordinates
{

    private Coordinates() { }

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => PracticalAstronomy.Coordinates.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => PracticalAstronomy.Coordinates.haToRa(dateTime, longitude, hourAngle);

    public Coord2D EquatorialToHorizon(Coord2D equatorial, double latitude)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.equatorialToHorizon(equatorial.X, equatorial.Y, latitude));

}
