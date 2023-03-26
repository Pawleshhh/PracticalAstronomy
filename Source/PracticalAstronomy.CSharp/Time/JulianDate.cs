namespace PracticalAstronomy.CSharp;

public readonly record struct JulianDate(double Date)
{

    public DateTime ToDateTime()
        => PracticalAstronomy.Time.julianDateToDateTime(new(Date));

    public static JulianDate FromDateTime(DateTime dateTime)
        => new(PracticalAstronomy.Time.dateTimeToJulianDate(dateTime).jd);

}
