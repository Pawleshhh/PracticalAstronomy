namespace PracticalAstronomy.CSharp;

public class Time
{
    private Time() { }

    public TimeSpan DateTimeToGst(DateTime dateTime)
        => PracticalAstronomy.Time.dateTimeToGst(dateTime);

    public TimeSpan GstToUniversalTime(DateTime dateTime)
        => PracticalAstronomy.Time.gstToUt(dateTime);

}
