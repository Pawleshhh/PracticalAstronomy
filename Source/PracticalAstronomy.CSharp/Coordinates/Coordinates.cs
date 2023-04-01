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
            PracticalAstronomy.Coordinates.equatorialToHorizontal(latitude, equatorial.X, equatorial.Y));

    public Coord2D HorizontalToEquatorial(Coord2D horizontal, double latitude)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.horizontalToEquatorial(latitude, horizontal.X, horizontal.Y));

    public double MeanObliquity(DateTime dateTime)
        => PracticalAstronomy.Coordinates.meanObliquity(dateTime);

    public Coord2D EclipticToEquatorial(Coord2D ecliptic, DateTime dateTime)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.eclitpicToEquatorial(dateTime, ecliptic.X, ecliptic.Y));

    public Coord2D EquatorialToEcliptic(Coord2D equatorial, DateTime dateTime)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.equatorialToEcliptic(dateTime, equatorial.X, equatorial.Y));

    public Coord2D EquatorialToGalactic(Coord2D equatorial)
        => Coord2D.FromTuple(
            PracticalAstronomy.Coordinates.equatorialToGalactic(equatorial.X, equatorial.Y));

}
