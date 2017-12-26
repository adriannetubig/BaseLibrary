using System;

namespace BaseExtension
{
    public static class DateFormat
    {
        public static string ToHtml5Date(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm");
        }
    }
}
