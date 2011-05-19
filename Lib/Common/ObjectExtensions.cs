using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.ComponentModel;

namespace Lib.Common {
    
    public static class ObjectExtensions {

		public static bool IsNull(this object p_object) {
			return p_object == null;
		}

		/// <summary>
		/// If the value is Enumerable, treats it like Enumerable.IsNullOrEmpty().
		/// Otherwise, treats it like String.IsNullOrEmpty().
		/// </summary>
		public static bool IsNullOrEmpty(this object p_value) {
			if (p_value == null) 
				return StringExtensions.IsNullOrEmpty(null);
			
			else if (p_value is IEnumerable) 
				return ((IEnumerable)p_value).IsNullOrEmpty();
			
			else 
				return StringExtensions.IsNullOrEmpty(p_value.ToString());
		}

		public static bool IsNotNull(this object p_value) {
			return (p_value != null);
		}

		/// <summary>
		/// If the value is Enumerable, treats it like Enumerable.IsNotNullOrEmpty().
		/// Otherwise, treats it like String.IsNotNullOrEmpty().
		/// </summary>
		public static bool IsNotNullOrEmpty(this object p_value) {
			if (p_value == null)
				return StringExtensions.IsNotNullOrEmpty(null);

			else if (p_value is IEnumerable)
				return ((IEnumerable)p_value).IsNotNullOrEmpty();

			else
				return StringExtensions.IsNotNullOrEmpty(p_value.ToString());
		}

		/// <summary>
		/// If the value isn't null or empty, prints it using the specified 
		/// format string. {0} refers to the value itself.
		/// </summary>
		public static string FormatIfNotEmpty(this object p_value, string p_format) {
			if (p_value == null) return String.Empty;

			var s = p_value.ToString();

			if (s == String.Empty) return String.Empty;
			else return String.Format(p_format, s);
		}

		/// <summary>
		/// Returns true if the target is between the two specified values, inclusive.
		/// </summary>
		public static bool IsBetween<T>(this T p_thisObj, T p_min, T p_max) where T : IComparable {
			bool isGreaterThanOrEqualToMin = (p_thisObj.CompareTo(p_min) >= 0);
			bool isLessThanOrEqualToMax = (p_thisObj.CompareTo(p_max) <= 0);
			return isGreaterThanOrEqualToMin && isLessThanOrEqualToMax;
		}

		/// <summary>
		/// Throws an exception if the object is null, otherwise returns the object.
		/// </summary>
		public static T ThrowIfNull<T>(this T p_thisObj, string p_messageIfNull) {
			if (p_thisObj == null)
				throw new ApplicationException(p_messageIfNull);

			return p_thisObj;
		}

		/// <summary>
		/// Throws an exception if the sequence is empty, otherwise returns the object.
		/// </summary>
		public static T ThrowIfNullOrEmpty<T>(this T p_sequence, string p_messageIfEmpty) {
			if (p_sequence.IsNullOrEmpty())
				throw new ApplicationException(p_messageIfEmpty);

			return p_sequence;
		}

		/// <summary>
		/// Returns a dictionary of all public properties mapped to their values.
		/// </summary>
		public static Dictionary<string, object> ToPropertyDictionary(this object p_object) {
			var dict = new Dictionary<string, object>();

			if (p_object == null) return dict;

			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(p_object))
			{
				object obj2 = descriptor.GetValue(p_object);
				dict.Add(descriptor.Name, obj2);
			}

			return dict;
		}

		/// <summary>
		/// If the instance is non-null, returns its ToString() result. If the instance is
		/// null, returns p_valueIfNull.
		/// </summary>
		public static string ToStringNullSafe(this object p_object, string p_valueIfNull) {
			return (p_object == null)
				? p_valueIfNull
				: p_object.ToString();
		}
	}
}
