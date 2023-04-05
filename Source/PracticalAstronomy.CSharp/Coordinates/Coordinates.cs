namespace PracticalAstronomy.CSharp;

public class Coordinates
{

    private Coordinates() { }

    public Coord2D GeneralisedTransformation(CoordConversion conversion, Coord2D coord)
        => PracticalAstronomy.Coordinates.generalisedTransformation(conversion.ToFSharp(), coord.X, coord.Y);

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => PracticalAstronomy.Coordinates.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => PracticalAstronomy.Coordinates.haToRa(dateTime, longitude, hourAngle);

    public Coord2D EquatorialToHorizontal(Coord2D equatorial, double latitude)
        => PracticalAstronomy.Coordinates.equatorialToHorizontal(latitude, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D HorizontalToEquatorial(Coord2D horizontal, double latitude)
        => PracticalAstronomy.Coordinates.horizontalToEquatorial(latitude, horizontal.X, horizontal.Y).ToCoord2D();

    public double MeanObliquity(DateTime dateTime)
        => PracticalAstronomy.Coordinates.meanObliquity(dateTime);

    public Coord2D EclipticToEquatorial(Coord2D ecliptic, DateTime dateTime)
        => PracticalAstronomy.Coordinates.eclitpicToEquatorial(dateTime, ecliptic.X, ecliptic.Y).ToCoord2D();

    public Coord2D EquatorialToEcliptic(Coord2D equatorial, DateTime dateTime)
        => PracticalAstronomy.Coordinates.equatorialToEcliptic(dateTime, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D EquatorialToGalactic(Coord2D equatorial)
        => PracticalAstronomy.Coordinates.equatorialToGalactic(equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D GalacticToEquatorial(Coord2D galactic)
        => PracticalAstronomy.Coordinates.galacticToEquatorial(galactic.X, galactic.Y).ToCoord2D();

}
