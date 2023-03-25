namespace PracticalAstronomy.CSharp;

public readonly record struct JulianDate(double Date)
{

    public DateTime ToDateTime()
        => Time.julianDateToDateTime(new(Date));

    public static JulianDate FromDateTime(DateTime dateTime)
        => new(Time.dateTimeToJulianDate(dateTime).julianDate);

}
