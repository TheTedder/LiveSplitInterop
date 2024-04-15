using System;
using System.Text;
using System.Globalization;

namespace LiveSplitInterop
{
    /// <summary>
    /// Utility class for formatting and parsing data
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Parses a string returned by LiveSplit as a <see cref="TimeSpan"/>.
        /// </summary>
        public static TimeSpan ParseLsTimeSpan(string s) => TimeSpan.ParseExact(s, "c", CultureInfo.InvariantCulture);

        /// <summary>
        /// Parses a string returned by LiveSplit as a <see cref="TimeSpan"/>.
        /// Considers <c>-</c> to be a null TimeSpan.
        /// </summary>
        public static TimeSpan? ParseLsTimeSpanNullable(string s)
        {
            if (s == "-")
            {
                return null;
            }

            return ParseLsTimeSpan(s);
        }

        public static string ToLsString(this TimeSpan timeSpan) => timeSpan.ToString("c", CultureInfo.InvariantCulture);
    }
}
