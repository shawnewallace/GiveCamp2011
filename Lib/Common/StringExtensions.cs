using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Lib.Common {

	public static class StringExtensions {

		public static string IfNullOrEmpty(this string p_stringValue, string p_valueIfNullOrEmpty) {
			return String.IsNullOrEmpty(p_stringValue)
				? p_valueIfNullOrEmpty
				: p_stringValue;
		}

		/// <summary>
		/// Syntactic sugar for String.Split(). Accepts a string rather than a character array.
		/// </summary>
		/// <param name="p_delimiterCharacter">The single character delimiter.</param>
		public static string[] Split(this string p_value, string p_delimiterCharacter) {
			if (p_delimiterCharacter.Length != 1)
				throw new ArgumentException("The delimiter must be a single character!.");

			if (String.IsNullOrEmpty(p_value))
				return new string[0];

			return p_value.Split(p_delimiterCharacter.ToCharArray());
		}

		public static string ReplaceRegex(this string p_value, string p_regex, string p_replaceWith) {
			return Regex.Replace(p_value ?? "", p_regex, p_replaceWith);
		}

		public static bool MatchesRegex(this string p_value, string p_regex) {
			return Regex.IsMatch(p_value, p_regex);
		}

		public static int CountMatches(this string p_value, string p_regex) {
			return Regex.Matches(p_value, p_regex).Count;
		}

		/// <summary>
		/// Accepts a regex and returns the portion of the target string matching the group.
		/// If a subgroup is specified using () then only the subgroup is returned.
		/// </summary>
		public static string Extract(this string p_value, string p_regex) {
			if (!p_value.MatchesRegex(p_regex))
				throw new FormatException("The specified pattern /" + p_regex + "/ was not found.");

			return p_value.Extract(p_regex, "");
		}

		/// <summary>
		/// Accepts a regex and returns the portion of the target string matching the group.
		/// If a subgroup is specified using () then only the subgroup is returned.
		/// </summary>
		public static string Extract(this string p_value, string p_regex, string p_default) {
			if (!p_value.MatchesRegex(p_regex)) return p_default;

			var matches = Regex.Match(p_value, p_regex);
			if (matches.Groups.Count == 1)
				return matches.Groups[0].Value;
			else
				return matches.Groups[1].Value;
		}

		/// <summary>
		/// Syntactic sugar to replace "if (!String.IsNullOrEmpty(value))"
		/// </summary>
		public static bool IsNotNullOrEmpty(this string p_value) {
			return !String.IsNullOrEmpty(p_value);
		}

		/// <summary>
		/// Syntactic sugar to replace "if (String.IsNullOrEmpty(value))"
		/// </summary>
		public static bool IsNullOrEmpty(this string p_value) {
			return String.IsNullOrEmpty(p_value);
		}

		public static string Repeat(this string p_toRepeat, int p_count) {
			if (p_count < 0) throw new ArgumentException("Count must be > 0");
			else if (p_count == 0) return String.Empty;
			else if (p_count == 1) return p_toRepeat;
			else {
				var sb = new StringBuilder();
				for (int i = 0; i < p_count; i++)
					sb.Append(p_toRepeat);
				return sb.ToString();
			}
		}

		/// <summary>
		/// Makes a string safe for direct usage in T-SQL by escaping single quotes.
		/// </summary>
		public static string ToSqlSafe(this string p_value) {
			if (p_value == null) return null;
			else return p_value.Replace("'", "''");
		}

		/// <summary>
		/// This is a null-safe replacement for string.ToUpper().
		/// </summary>
		public static string ToUpperCase(this string p_value) {
			return (p_value) != null
				? p_value.ToUpper()
				: "";
		}

		/// <summary>
		/// This is a null-safe replacement for string.Trim().
		/// </summary>
		public static string TrimWhitespace(this string p_value) {
			return (p_value) != null
				? p_value.Trim()
				: "";
		}

		/// <summary>
		/// Null-safe truncate function.
		/// </summary>
		public static string Truncate(this string p_value, int p_maxCharacters) {
			if (p_value.IsNullOrEmpty())
				return String.Empty;

			if (p_maxCharacters >= p_value.Length)
				return p_value;

			return p_value.Substring(0, p_maxCharacters);
		}

		/// <summary>
		/// Null-safe truncate function. If the result was truncated, appends the specified
		/// text after truncating.
		/// </summary>
		public static string Truncate(this string p_value, int p_maxCharacters, string p_appendIfWasTruncated) {
			if (p_value.IsNullOrEmpty())
				return String.Empty;

			if (p_maxCharacters >= p_value.Length)
				return p_value;

			return p_value.Substring(0, p_maxCharacters) + (p_appendIfWasTruncated ?? "");
		}

		/// <summary>
		/// Copies Left function from VB, null-safe
		/// </summary>
		public static string Left(this string value, int length) {
			if (value.IsNullOrEmpty())
				return String.Empty;

            if (length <= 0) return value;

            if (length >= value.Length) return value;

			return value.Substring(0, length);
		}

		/// <summary>
		/// Copies Right function from VB, null-safe
		/// </summary>
		public static string Right(this string p_value, int p_length) {
			if (p_value.IsNullOrEmpty())
				return String.Empty;

			return p_value.Substring(p_value.Length - p_length, p_length);
		}

		/// <summary>
		/// Skips the specified number of characters and then returns the rest of the string. This
		/// is essentially syntactic sugar over a null-safe version of Right().
		/// </summary>
		public static string SkipChars(this string p_value, int p_charsToSkip) {
			if (p_value.IsNullOrEmpty())
				return String.Empty;

			if (p_charsToSkip >= p_value.Length)
				return String.Empty;

			return p_value.Right(p_value.Length - p_charsToSkip);
		}

		/// <summary>
		/// Same as Substring, but returns an empty string (rather than error) if the starting
		/// position is greater than the length of the string.
		/// </summary>
		public static string SubstringSafe(this string p_value, int p_startIndex) {
			if (p_value.IsNullOrEmpty())
				return String.Empty;

			if (p_startIndex >= p_value.Length)
				return String.Empty;

			return p_value.Substring(p_startIndex);
		}

		/// <summary>
		/// Null-safe replacement for StartsWith() that performs a current culture, 
		/// case-insensitive comparison.
		/// </summary>
		public static bool StartsWithIgnoringCase(this string p_value, string p_target) {
			return (p_value ?? "").StartsWith(p_target, StringComparison.CurrentCultureIgnoreCase);
		}

		/// <summary>
		/// Removes Html Tags from a string
		/// </summary>
		public static string StripHtml(this string p_value) {
			const string pattern = @"<(.|\n)*?>";
			return Regex.Replace(p_value, pattern, string.Empty);
		}

		/// <summary>
		/// Performs a case-insensitive comparison and returns true if the strings are equal.
		/// </summary>
		public static bool EqualsIgnoringCase(this string p_value, string p_compareTo) {
			if (p_value == null) {
				return (p_compareTo == null);
			}

			return p_value.Equals(p_compareTo, StringComparison.CurrentCultureIgnoreCase);
		}

		/// <summary>
		/// Performs a case-insensitive comparison and returns true if the strings are not equal.
		/// </summary>
		public static bool IsNotEqualIgnoringCase(this string p_value, string p_compareTo) {
			return p_value.EqualsIgnoringCase(p_compareTo) == false;
		}

		/// <summary>
		/// Performs a case-insensitive Cpntains() operation.
		/// </summary>
		public static bool ContainsIgnoringCase(this string p_value, string p_target) {
			if (p_value == null)
				return false;

			return p_value.IndexOf(p_target, StringComparison.CurrentCultureIgnoreCase) > 0;
		}
	}
}
