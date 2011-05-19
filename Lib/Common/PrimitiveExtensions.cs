using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lib.Common
{
	public static class PrimitiveExtensions
	{
		public static bool ToBool(this bool? value)
		{
			return value != null && value.Value;
		}

		public static string ToXhtmlLink(this string url)
		{
			return !string.IsNullOrEmpty(url) ? url.Replace("&", "&amp;") : string.Empty;
		}

		public static string ToStandardDate(this DateTime? value, string valueIfNull)
		{
			return value.HasValue ? value.Value.ToString("MM/dd/yyyy") : valueIfNull;
		}

		public static string ToFormattedString(this TimeSpan value)
		{
			var list = new List<string>(3);

			var days = value.Days;
			var hours = value.Hours;
			var minutes = value.Minutes;

			if (days > 1)
				list.Add(String.Format("{0} days", days));
			else if (days == 1)
				list.Add(String.Format("{0} day", days));

			if (hours > 1)
				list.Add(String.Format("{0} hours", hours));
			else if (hours == 1)
				list.Add(String.Format("{0} hour", hours));

			if (minutes > 1)
				list.Add(String.Format("{0} minutes", minutes));
			else if (minutes == 1)
				list.Add(String.Format("{0} minute", minutes));

			return String.Join(", ", list.ToArray());
		}

		public static string ToNullSafeString(this object value)
		{
			return value == null ? String.Empty : value.ToString();
		}

		public static string ToLowerCamelCase(this string value)
		{
			return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
		}

		public static string ToSeparatedWords(this string value)
		{
			return Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim();
		}

		public static string WrapEachWith(this IEnumerable values, string before, string after, string separator)
		{
			var list = new List<string>();

			foreach (object value in values)
			{
				list.Add(string.Format("{0}{1}{2}", before, value, after));
			}
			return string.Join(separator, list.ToArray());
		}
	}
}