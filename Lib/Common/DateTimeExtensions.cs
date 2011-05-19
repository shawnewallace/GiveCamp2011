using System;
using System.Data.SqlTypes;

namespace Lib.Common {
	public static class DateTimeExtensions {

		/// <summary>
		/// Squeezes a date/time value into a range that is compatible with SQL Server.
		/// </summary>
		public static DateTime ToSqlSafeDate(this DateTime p_date) {
			return (p_date < SqlDateTime.MinValue.Value)
					   ? SqlDateTime.MinValue.Value
					   : p_date;
		}

		/// <summary>
		/// Null-safe version of ToShortDateString() for nullable DateTime objects.
		/// </summary>
		public static string ToShortDateString(this DateTime? p_date) {
			return (p_date.HasValue)
				? p_date.Value.ToString("MM/dd/yyyy")
				: "";
		}

	    /// <summary>
		/// Returns true if a date is between two other dates, specified as strings. (inclusive)
		/// </summary>
		public static bool IsBetween(this DateTime p_date, string p_start, string p_end) {
			return p_date.IsBetween(DateTime.Parse(p_start), DateTime.Parse(p_end));
		}

		/// <summary>
		/// If the value isn't null, formats it using the specified format string. Returns an
		/// empty string if the value is null.
		/// </summary>
		public static string FormatIfNotNull(this DateTime? p_date, string p_format) {
			if (p_date == null) return String.Empty;
			else return p_date.Value.ToString(p_format);
		}
	}
}
