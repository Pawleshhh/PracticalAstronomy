namespace PracticalAstronomy.CSharp;

using FCoordinates = PracticalAstronomy.Coordinates;

public class Coordinates
{

    private Coordinates() { }

    public Coord2D GeneralisedTransformation(CoordConversion conversion, Coord2D coord)
        => FCoordinates.generalisedTransformation(conversion.ToFSharp(), coord.X, coord.Y).ToCoord2D();

    public double RightAscensionToHourAngle(DateTime dateTime, double longitude, double rightAscension)
        => FCoordinates.raToHa(dateTime, longitude, rightAscension);

    public double HourAngleToRightAscension(DateTime dateTime, double longitude, double hourAngle)
        => FCoordinates.haToRa(dateTime, longitude, hourAngle);

    public Coord2D EquatorialToHorizontal(Coord2D equatorial, double latitude)
        => FCoordinates.equatorialToHorizontal(latitude, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D HorizontalToEquatorial(Coord2D horizontal, double latitude)
        => FCoordinates.horizontalToEquatorial(latitude, horizontal.X, horizontal.Y).ToCoord2D();

    public double MeanObliquity(DateTime dateTime)
        => FCoordinates.meanObliquity(dateTime);

    public Coord2D EclipticToEquatorial(Coord2D ecliptic, DateTime dateTime)
        => FCoordinates.eclitpicToEquatorial(dateTime, ecliptic.X, ecliptic.Y).ToCoord2D();

    public Coord2D EquatorialToEcliptic(Coord2D equatorial, DateTime dateTime)
        => FCoordinates.equatorialToEcliptic(dateTime, equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D EquatorialToGalactic(Coord2D equatorial)
        => FCoordinates.equatorialToGalactic(equatorial.X, equatorial.Y).ToCoord2D();

    public Coord2D GalacticToEquatorial(Coord2D galactic)
        => FCoordinates.galacticToEquatorial(galactic.X, galactic.Y).ToCoord2D();

    public double CelestialAngle(Coord2D celestialObj1, Coord2D celestialObj2)
        => FCoordinates.celestialAngle(celestialObj1.X, celestialObj1.Y, celestialObj2.X, celestialObj2.Y);

}
