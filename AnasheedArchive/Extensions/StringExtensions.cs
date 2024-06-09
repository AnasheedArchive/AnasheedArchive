using System.Globalization;

namespace AnasheedArchive.Extensions;

public static class StringExtensions
{
    public static string ToTitle(this string s)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        return textInfo.ToTitleCase(s);
    }
}
