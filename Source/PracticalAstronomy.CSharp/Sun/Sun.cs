namespace PracticalAstronomy.CSharp;

public class Sun
{
    internal Sun() { }

    public static EquatorialRightAscension PositionOfSunEquatorial(Epoch epoch, DateTime dateTime)
    {
        var position = FS.Sun.positionOfSunEquatorial(Epoch.ToFSharpEpoch(epoch), dateTime);
        return new(position.rightAscension, position.declination);
    }
}
