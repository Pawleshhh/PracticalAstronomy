namespace PracticalAstronomy.CSharp;

public readonly record struct JulianDate(double Date)
{
    public static JulianDate FromDateTime(DateTime dateTime)
        => new(Time.dateTimeToJulianDate(dateTime).julianDate);

}
