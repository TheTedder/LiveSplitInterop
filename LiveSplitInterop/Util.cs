using System;
using System.Text;

namespace LiveSplitInterop
{
    /// <summary>
    /// Utility class for formatting data
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Parses a string returned by LiveSplit as a <see cref="TimeSpan"/>.
        /// </summary>
        /// <remarks>
        /// TimeSpans returned by LiveSplit are in the format <c>[-][[hhhhhhhhh:]m]m:ss.ff</c>.
        /// This can't be parsed by any of the built-in custom TimeSpan formatters, necessitating
        /// the use of this method instead.
        /// </remarks>
        public static TimeSpan ParseLiveSplitTimeSpan(string s)
        {
            string[] sections = s.Split(':');
            double seconds = double.Parse(sections[sections.Length - 1]);
            seconds += double.Parse(sections[sections.Length - 2]) * 60.0;

            if (sections.Length == 3)
            {
                seconds += double.Parse(sections[sections.Length - 3]) * 3600.0;
            }

            return TimeSpan.FromSeconds(seconds);
        }

        //TODO: Implement a custom format specifier instead.

        /// <summary>
        /// Formats a <see cref="TimeSpan"/> in a way that LiveSplit can parse.
        /// </summary>
        /// <remarks>
        /// LiveSplit cannot parse TimeSpans that begin with a number of days followed by a period,
        /// and there is no custom format specifier that can print a number of hours greater than 23;
        /// therefore, it is necessary to use this method to format TimeSpans of 24 hours or longer.
        /// </remarks>
        public static string ToLiveSplitString(this TimeSpan timeSpan)
        {
            return $@"setgametime {(int)timeSpan.TotalHours}:{timeSpan:mm\:ss\.fffffff}";
        }
    }
}
