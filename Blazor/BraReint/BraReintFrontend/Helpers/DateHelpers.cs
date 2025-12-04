namespace BraReintFrontend.Helpers;

public class DateHelpers
{
    public static string FormatDate(string dateTimeString)
    {
        return DateTime.TryParse(dateTimeString, out var dateTime)
            ? dateTime.ToString("MMMM d, yyyy")
            : dateTimeString; // "April 2, 2025"
    }
    
    public static string GetWeekday(string dateTimeString)
    {
        return DateTime.TryParse(dateTimeString, out var dateTime)
            ? dateTime.ToString("dddd")
            : string.Empty; // "Wednesday"
    }
}