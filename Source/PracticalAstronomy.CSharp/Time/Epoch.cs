namespace PracticalAstronomy.CSharp;

public enum Epoch 
{
    J1900,
    J1950,
    J2000,
    J2050
}

internal static class EpochExtension
{
    public static FS.TimeDataTypes.Epoch ToFSharpEpoch(this Epoch epoch)
    {
        return epoch switch
        {
            Epoch.J1900 => FS.TimeDataTypes.Epoch.J1900,
            Epoch.J1950 => FS.TimeDataTypes.Epoch.J1950,
            Epoch.J2000 => FS.TimeDataTypes.Epoch.J2000,
            Epoch.J2050 => FS.TimeDataTypes.Epoch.J2050,
            _ => throw new ArgumentException(null, nameof(epoch))
        };
    }
}