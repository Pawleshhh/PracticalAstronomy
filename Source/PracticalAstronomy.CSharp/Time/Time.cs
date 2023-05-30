namespace PracticalAstronomy.CSharp;

public class Time
{
    internal Time() { }

    public TimeSpan DateTimeToGst(DateTime dateTime)
        => PracticalAstronomy.Time.dateTimeToGst(dateTime);

    public TimeSpan GstToUniversalTime(DateTime dateTime)
        => PracticalAstronomy.Time.gstToUt(dateTime);

    public TimeSpan GstToLst(double longitude, TimeSpan gst)
        => PracticalAstronomy.Time.gstToLst(longitude, gst);

    public TimeSpan LstToGst(double longitude, TimeSpan lst)
        => PracticalAstronomy.Time.lstToGst(longitude, lst);

}
