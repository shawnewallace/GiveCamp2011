using System;

namespace Lib.Common {
    public static class DateFormatExtensions {

        /// <summary>
        /// Null-safe version of ToDateFormat(). Formats dates as m/d/yyyy.
        /// </summary>
        public static string ToDateFormat(this DateTime? date) {
            return date.ToDateFormat("");
        }

        /// <summary>
        /// Null-safe version of ToDateFormat(). Formats dates as m/d/yyyy.
        /// </summary>
        public static string ToDateFormat(this DateTime? date, string valueIfNull) {
            return (date.HasValue)
                ? date.Value.ToString("M/d/yyyy")
                : valueIfNull;
        }

        /// <summary>
        /// Formats dates as mm/dd/yyyy.
        /// </summary>
        public static string ToDateFormat(this DateTime date) {
            return date.ToString("M/d/yyyy");
        }

        /// <summary>
        /// Formats dates and times as mm/dd/yyyy HH:mm:ss.
        /// </summary>
        public static string ToDateTimeFormat(this DateTime? date) {
            return date.HasValue
                ? date.Value.ToDateTimeFormat()
                : "";
        }

        /// <summary>
        /// Formats dates and times as mm/dd/yyyy HH:mm:ss.
        /// </summary>
        public static string ToDateTimeFormat(this DateTime date) {
            return date.ToString("M/d/yyyy HH:mm:ss");
        }

        public static string DateAndTime(DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy HH:mm");
        }

        public static string DateAndTime(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("MM/dd/yyyy HH:mm") : string.Empty;
        }

        public static string Date(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }

        public static string Date(DateTime? date)
        {
            return date == null
                    ? string.Empty
                    : Date(date.Value);
        }
    }
}
