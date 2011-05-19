using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Lib.Common {
	public static class ParsingExtensions {

		public static DateTime ToDateTime(this string p_stringValue) {
			return DateTime.Parse(p_stringValue);
		}

		public static DateTime ToDateTime(this string p_stringValue, DateTime p_defaultValue) {
			DateTime parsed;

			if (DateTime.TryParse(p_stringValue, out parsed))
				return parsed;

			return p_defaultValue;
		}

		public static double ToDouble(this string p_stringValue)
		{
			if (p_stringValue == null) return 0d;
			return Convert.ToDouble(p_stringValue);
		}

		public static double ToDouble(this string p_stringValue, double p_defaultValue)
		{
			if (String.IsNullOrEmpty(p_stringValue))
				return p_defaultValue;

			double parsedValue;
			if (Double.TryParse(p_stringValue, out parsedValue))
				return parsedValue;
			else
				return p_defaultValue;
		}

		public static int ToInt32(this string p_stringValue) {
			if (p_stringValue == null) return 0;

			return Convert.ToInt32(p_stringValue);
		}

		public static int ToInt32(this string p_stringValue, int p_defaultValue) {
			if (String.IsNullOrEmpty(p_stringValue))
				return p_defaultValue;

			int parsedValue;
			if (Int32.TryParse(p_stringValue, out parsedValue))
				return parsedValue;
			else
				return p_defaultValue;
		}

		public static decimal ToDecimal(this string p_stringValue, decimal p_defaultValue) {
			if (String.IsNullOrEmpty(p_stringValue))
				return p_defaultValue;

			return p_stringValue.ToNullableDecimal() ?? p_defaultValue;
		}

		public static int? ToNullableInt(this string p_stringValue) {
			int parsed;

			return Int32.TryParse(p_stringValue, out parsed)
				? (int?)parsed
				: (int?)null;
		}

		public static DateTime? ToNullableDate(this string p_stringValue) {
			DateTime parsed;

			return DateTime.TryParse(p_stringValue, out parsed)
				? (DateTime?)parsed
				: (DateTime?)null;
		}

		public static decimal? ToNullableDecimal(this string p_stringValue) {
			decimal parsed;

			return Decimal.TryParse(p_stringValue, out parsed)
				? (decimal?)parsed
				: (decimal?)null;
		}

		public static bool ToBoolean(this string p_stringValue) {
			if (p_stringValue.IsNullOrEmpty())
				throw new FormatException("Can't parse null or empty string as boolean");

			bool result;

			if (Boolean.TryParse(p_stringValue, out result))
				return result;
			else {
				switch (p_stringValue.ToUpper()) {
					case "T":
					case "TRUE":
					case "Y":
					case "YES":
					case "1":
						return true;

					case "F":
					case "FALSE":
					case "N":
					case "NO":
					case "0":
						return false;
				}

				throw new FormatException("Can't parse '" + p_stringValue + "' as a boolean.");
			}
		}

		public static bool ToBoolean(this string p_value, bool p_defaultValue) {
			try {
				return p_value.ToBoolean();
			}
			catch (FormatException) {
				return p_defaultValue;
			}
		}

		public static bool? ToNullableBoolean(this string p_value) {
			try {
				return p_value.ToBoolean();
			}
			catch (FormatException) {
				return null;
			}
		}

		/// <summary>
		/// Parses a comma delimited string and returns a List<int> of the values.
		/// </summary>
		public static List<int> ToIntList(this string p_stringValue) {
			if (p_stringValue.IsNullOrEmpty())
				return new List<int>();
			else 
				return p_stringValue.ToIntList(",");
		}

		/// <summary>
		/// Parses a comma delimited string and returns a List<int> of the values.
		/// </summary>
		public static List<int> ToIntList(this string p_stringValue, string p_delimiter) {
			if (String.IsNullOrEmpty(p_stringValue)) return new List<int>();

			var list = new List<int>();

			foreach (string slice in p_stringValue.Split(p_delimiter)) {
				int? parsed = slice.ToNullableInt();

				if (parsed.HasValue)
					list.Add(parsed.Value);
			}

			return list;
		}

		/// <summary>
		/// Parses a string into an enum of the specified type. Will match against both the
		/// enum's ToString() value as well as its Description attribute, if applicable.
		/// </summary>
		public static T ToEnum<T>(this string p_stringValue) where T : struct {
			return EnumHelper.Parse<T>(p_stringValue);
		}

		/// <summary>
		/// Parses a string into an enum of the specified type. Will match against both the
		/// enum's ToString() value as well as its Description attribute, if applicable.
		/// </summary>
		public static T ToEnum<T>(this string p_stringValue, T p_default) where T : struct {
			return EnumHelper.Parse<T>(p_stringValue, p_default);
		}

		/// <summary>
		/// Parses a string into an enum of the specified type. Will match against both the
		/// enum's ToString() value as well as its Description attribute, if applicable.
		/// </summary>
		public static T? ToNullableEnum<T>(this string p_stringValue) where T : struct {
			return EnumHelper.ParseAsNullable<T>(p_stringValue);
		}

		/// <summary>
		/// Parses a list of strings into a list of enums of the specified type. Will match 
		/// the enum's ToString() value or its Description attribute, if applicable.
		/// </summary>
		public static List<T> ToEnumList<T>(this IEnumerable<string> p_strings) where T : struct {
			var list = new List<T>();

			if (p_strings == null)
				return list;

			foreach (string s in p_strings) {
				if (s.IsNullOrEmpty()) continue;
				list.Add(s.ToEnum<T>());
			}

			return list;
		}
	}
}
