
namespace PracticalAstronomy.CSharp;

public abstract class Epoch : IFSharpData<FS.TimeDataTypes.Epoch>
{

    public DateTime DateTime { get; }

    protected Epoch(int year)
    {
        DateTime = new DateTime(year, 1, 1);
    }

    public static Epoch J1900 { get; } = new J1900();
    public static Epoch J1950 { get; } = new J1950();
    public static Epoch J2000 { get; } = new J2000();
    public static Epoch J2050 { get; } = new J2050();

    public FS.TimeDataTypes.Epoch ToFSharp()
    {
        return ToFSharpEpoch(this);
    }

    public static FS.TimeDataTypes.Epoch ToFSharpEpoch(Epoch epoch)
    {
        return epoch switch
        {
            CSharp.J1900 => FS.TimeDataTypes.Epoch.J1900,
            CSharp.J1950 => FS.TimeDataTypes.Epoch.J1950,
            CSharp.J2000 => FS.TimeDataTypes.Epoch.J2000,
            CSharp.J2050 => FS.TimeDataTypes.Epoch.J2050,
            _ => FS.TimeDataTypes.Epoch.NewJEpoch(epoch.DateTime.Year)
        };
    }

}

public class J1900 : Epoch
{
    public J1900() : base(1900) { }
}

public class J1950 : Epoch
{
    public J1950() : base(1950) { }
}

public class J2000 : Epoch
{
    public J2000() : base(2000) { }
}

public class J2050 : Epoch
{
    public J2050() : base(2050) { }
}

public class JEpoch : Epoch
{
    public JEpoch(int year) : base(year) { }
}