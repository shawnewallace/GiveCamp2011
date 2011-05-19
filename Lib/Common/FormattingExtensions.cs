using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;

namespace Lib.Common {
    public static class FormattingExtensions {

        /// <summary>
        /// Makes a string "Javascript safe" by escaping quotes and embedded newlines.
        /// </summary>
        public static string ToJavascriptString(this string p_value) {
            if (p_value.IsNullOrEmpty()) return String.Empty;

            // We don't know what type of quotes are surrounding the string, nor do we know
            // if the string is being used in an inline JS expression. We escape single quotes
            // using the escape code &#39 and double quotes with &#34, which ensures we can use
            // the string in an inline event handler (for instance)
            string safe = p_value.Replace("\"", @"\&#34");
            safe = safe.Replace("'", @"\&#39");
            safe = safe.Replace(Environment.NewLine, @"\n");

            return safe;
        }

        /// <summary>
        /// Returns a Javascript expression that initializes a string array from the
        /// given list of items (e.g. ["item1",...,"itemn"] )
        /// </summary>
        public static string ToJavascriptStringArray(this IEnumerable p_list) {
            var escapedStringList = new List<string>();
            foreach (object o in p_list) {
                if (o == null) continue;
                escapedStringList.Add("\"" + o.ToString() + "\"");
            }

            return "[" + escapedStringList.Join(",") + "]";
        }

        public static string ToHtmlEncoded(this string p_value) {
            return HttpUtility.HtmlEncode(p_value);
        }

        public static string ToCurrencyFormat(this decimal p_value) {
            // todo: We should format the value according to the current culture, but on my 
            // test system the proper format string isn't being applied. Hardcoding to US culture 
            // for now.
            var value = Math.Round(p_value, 2);
            if (value < 0) return String.Format("<span class='red'>{0:c}</span>", Math.Round(p_value, 2));
            else return String.Format("{0:c}", Math.Round(p_value, 2));
        }

		public static string ToCurrencyFormat(this decimal? p_value) {
			// todo: We should format the value according to the current culture, but on my 
			// test system the proper format string isn't being applied. Hardcoding to US culture 
			// for now.
			if (p_value.HasValue) {
				var value = Math.Round(p_value.Value, 2);
				if (value < 0) return String.Format("<span class='red'>{0:c}</span>", Math.Round(p_value.Value, 2));

				else return String.Format("{0:c}", Math.Round(p_value.Value, 2));
			}
			else
				return String.Empty;
		}

		public static string ToCurrencyFormat(this decimal? p_value, string p_ifNullOrZero) {
			if ((p_value ?? 0m) == 0m)
				return p_ifNullOrZero;

			return (p_value ?? 0m).ToCurrencyFormat();
		}

		public static string ToCurrencyFormat(this decimal p_value, string p_ifZero) {
			if (p_value == 0m)
				return p_ifZero;

			return p_value.ToCurrencyFormat();
		}

        public static string ToCurrencyFormat(this double p_value) {
            var value = Math.Round(p_value, 2);
            if (value < 0) return String.Format("<span class='red'>{0:c}</span>", Math.Round(p_value, 2));
            else return String.Format("{0:c}", Math.Round(p_value, 2));
        }

        public static string ToCurrencyFormat(this double? p_value) {
            if (p_value.HasValue) {
                var value = Math.Round(p_value.Value, 2);
                if (value < 0) return String.Format("<span class='red'>{0:c}</span>", Math.Round(p_value.Value, 2));
                else return String.Format("{0:c}", Math.Round(p_value.Value, 2));
            }
            else
                return String.Empty;
        }

		public static string ToCurrencyFormat(this double p_value, string p_ifZero) {
			if (p_value == 0d)
				return p_ifZero;

			return p_value.ToCurrencyFormat();
		}

		public static string ToCurrencyFormat(this double? p_value, string p_ifNullOrZero) {
			if ((p_value ?? 0d) == 0d)
				return p_ifNullOrZero;

			return p_value.Value.ToCurrencyFormat();
		}

		public static string ToDecimalFormat(this decimal? p_value) {
			return p_value.HasValue
				? p_value.Value.ToDecimalFormat()
				: String.Empty;
		}

		public static string ToDecimalFormat(this decimal p_value) {
			var value = Math.Round(p_value, 2);
			return value.ToString();
		}

		public static string ToPercentFormat(this double p_value) {
			return p_value.ToString("0.00") + "%";
		}

		public static string ToPercentFormat(this double? p_value) {
			return p_value.HasValue
				? p_value.Value.ToPercentFormat()
				: String.Empty;
		}

        public static string ToYesNo(this Boolean p_value) {
            return (p_value) ? "Yes" : "No";
        }

        public static string ToYesNoUnknown(this bool? p_value) {
            return (p_value.HasValue)
                ? p_value.Value ? "Yes" : "No"
                : "Unknown";
        }

        public static string ToYesNoBlank(this bool? p_value) {
            return (p_value.HasValue)
                ? p_value.Value ? "Yes" : "No"
                : "";
        }

        public static string ToYesNo(this string p_value) {
            return p_value.ToBoolean().ToYesNo();
        }

        /// <summary>
        /// Returns "1" if the boolean value is true, "0" if it is false.
        /// </summary>
		public static string ToBitFlag(this Boolean p_value) {
            return (p_value) ? "1" : "0";
        }

		/// <summary>
		///		Formats a string using one format pattern if the string is null or empty, and 
		///		another if it has a non-empty value. Useful in view logic when non-empty values 
		///		should be appended with some string, like a break tag, but empty values should 
		///		just be ignored.
		/// </summary>
		/// <param name="p_patternIfNotEmpty">
		///		The pattern to apply to non-empty values. Use {0} as a placeholder for the string being formatted.
		/// </param>
		/// <param name="p_resultIfEmpty">
		///		The string to return if the string being formatted is null or empty.
		/// </param>
		public static string FormatUnlessEmpty(this string p_stringToFormat, string p_patternIfNotEmpty, string p_resultIfEmpty) {
			return p_stringToFormat.IsNullOrEmpty()
				? p_resultIfEmpty
				: String.Format(p_patternIfNotEmpty, p_stringToFormat);
		}

		/// <summary>
		///		Formats a number using one format pattern if the string is null or empty, and 
		///		another if it has a non-empty value. Useful in view logic when non-empty values 
		///		should be appended with some string, like a break tag, but empty values should 
		///		just be ignored.
		/// </summary>
		/// <param name="p_resultPattern">
		///		The pattern to apply to non-empty values. Use {0} as a placeholder for the string being formatted.
		/// </param>
		/// <param name="p_valueFormat">
		///		This format string is applied to the object being formatted (using ToString()), and
		///		then the result is passed into the format in p_resultPattern.
		/// </param>
		/// <param name="p_resultIfEmpty">
		///		The string to return if the string being formatted is null or empty.
		/// </param>
		public static string FormatUnlessEmpty(this double? p_objectToFormat
			, string p_resultPattern
			, string p_valueFormat
			, string p_resultIfEmpty) {

			if (p_objectToFormat == null)
				return p_resultIfEmpty;

			var innerResult = p_objectToFormat.Value.ToString(p_valueFormat);

			return p_resultPattern.IsNullOrEmpty()
				? innerResult
				: String.Format(p_resultPattern, innerResult);
		}

		public static string FormatUnlessEmpty(this double p_objectToFormat
			, string p_resultPattern
			, string p_valueFormat
			, string p_resultIfEmpty) {

			return ((double?)p_objectToFormat).FormatUnlessEmpty(p_resultPattern, p_valueFormat, p_resultIfEmpty);
		}

		/// <summary>
		///		Formats a number using one format pattern if the string is null or empty, and 
		///		another if it has a non-empty value. Useful in view logic when non-empty values 
		///		should be appended with some string, like a break tag, but empty values should 
		///		just be ignored.
		/// </summary>
		/// <param name="p_resultPattern">
		///		The pattern to apply to non-empty values. Use {0} as a placeholder for the string being formatted.
		/// </param>
		/// <param name="p_valueFormat">
		///		This format string is applied to the object being formatted (using ToString()), and
		///		then the result is passed into the format in p_resultPattern.
		/// </param>
		/// <param name="p_resultIfEmpty">
		///		The string to return if the string being formatted is null or empty.
		/// </param>
		public static string FormatUnlessEmpty(this decimal? p_objectToFormat
			, string p_resultPattern
			, string p_valueFormat
			, string p_resultIfEmpty) {

			if (p_objectToFormat == null)
				return p_resultIfEmpty;

			var innerResult = p_objectToFormat.Value.ToString(p_valueFormat);

			return p_resultPattern.IsNullOrEmpty()
				? innerResult
				: String.Format(p_resultPattern, innerResult);
		}

		public static string FormatUnlessEmpty(this decimal p_objectToFormat
			, string p_resultPattern
			, string p_valueFormat
			, string p_resultIfEmpty) {

			return ((decimal?)p_objectToFormat).FormatUnlessEmpty(p_resultPattern, p_valueFormat, p_resultIfEmpty);
		}

		/// <summary>
		///		Formats a number using the specified format pattern [using ToString(format)], 
		///		unless the value is null in which case the specified alternate string is used. 
		/// </summary>
		/// <param name="p_formatString">
		///		The ToString() format to apply to non-null values.
		/// </param>
		/// <param name="p_resultIfNull">
		///		The string to return if the number being formatted is null or empty.
		/// </param>
		public static string FormatUnlessNull(this int? p_valueToFormat
			, string p_formatString
			, string p_resultIfNull) {

			return (p_valueToFormat == null)
				? p_resultIfNull
				: p_valueToFormat.Value.ToString(p_formatString);
		}

		/// <summary>
		///		Formats a number using the specified format pattern [using ToString(format)], 
		///		unless the value is null in which case the specified alternate string is used. 
		/// </summary>
		/// <param name="p_formatString">
		///		The ToString() format to apply to non-null values.
		/// </param>
		/// <param name="p_resultIfNull">
		///		The string to return if the number being formatted is null or empty.
		/// </param>
		public static string FormatUnlessNull(this decimal? p_valueToFormat
			, string p_formatString
			, string p_resultIfNull) {

			return (p_valueToFormat == null)
				? p_resultIfNull
				: p_valueToFormat.Value.ToString(p_formatString);
		}

		/// <summary>
		///		Formats a number using the specified format pattern [using ToString(format)], 
		///		unless the value is null in which case the specified alternate string is used. 
		/// </summary>
		/// <param name="p_formatString">
		///		The ToString() format to apply to non-null values.
		/// </param>
		/// <param name="p_resultIfNull">
		///		The string to return if the number being formatted is null or empty.
		/// </param>
		public static string FormatUnlessNull(this double? p_valueToFormat
			, string p_formatString
			, string p_resultIfNull) {

			return (p_valueToFormat == null)
				? p_resultIfNull
				: p_valueToFormat.Value.ToString(p_formatString);
		}

		/// <summary>
		///		Formats a number using the specified format pattern [using ToString(format)], 
		///		unless the value is null in which case the specified alternate string is used. 
		/// </summary>
		/// <param name="p_formatString">
		///		The ToString() format to apply to non-null values.
		/// </param>
		/// <param name="p_resultIfNull">
		///		The string to return if the number being formatted is null or empty.
		/// </param>
		public static string FormatUnlessNull(this float? p_valueToFormat
			, string p_formatString
			, string p_resultIfNull) {

			return (p_valueToFormat == null)
				? p_resultIfNull
				: p_valueToFormat.Value.ToString(p_formatString);
		}
	}
}
