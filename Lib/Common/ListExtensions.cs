using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Common {
	
	public static class ListExtensions {
	
		/// <summary>
		/// Replaces all matching list items with another item, then returns a reference to the list.
		/// </summary>
		public static List<T> Replace<T>(this List<T> p_list, T p_target, T p_replaceWith) {
			if (p_list == null || p_list.Count == 0) return p_list;

			for (int i = 0; i < p_list.Count; i++) {
				if (p_list[i].Equals(p_target))
					p_list[i] = p_replaceWith;
			}

			return p_list;
		}

		public static bool ContainsAll<T>(this List<T> p_list, List<T> p_valuesToFind) {
			if (p_valuesToFind.IsNullOrEmpty()) return true;

			foreach (var item in p_valuesToFind) {
				if (p_list.Contains(item) == false)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns TRUE if the value is found in the specified list. Syntactic sugar
		/// for List.Contains()
		/// </summary>
		public static bool IsIn<T>(this T p_value, IList<T> p_targetList) {
			if (p_targetList == null) return false;

			return p_targetList.Contains(p_value);
		}

		/// <summary>
		/// Returns TRUE if the value is found in the specified list. Syntactic sugar
		/// for List.Contains()
		/// </summary>
		public static bool IsIn<T>(this T p_value, params T[] p_targetList) {
			if (p_targetList == null) return false;

			return p_targetList.Contains(p_value);
		}

		/// <summary>
		/// Returns TRUE if the value is found in the specified list. Syntactic sugar
		/// for List.Contains() with a case-insensitive comparison.
		/// </summary>
		public static bool IsInIgnoringCase(this string p_value, params string[] p_targetList) {
			if (p_targetList == null) return false;

			return p_targetList.Contains(p_value, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>
		/// Returns TRUE if the value is NOT found in the specified list. Syntactic sugar
		/// for the opposite of List.Contains()
		/// </summary>
		public static bool IsNotIn<T>(this T p_value, IList<T> p_targetList) {
			return p_value.IsIn(p_targetList) == false;
		}

		/// <summary>
		/// Returns TRUE if the value is NOT found in the specified list, ignoring case. 
		/// Syntactic sugar for the opposite of a case-insensitive List.Contains()
		/// </summary>
		public static bool IsNotInIgnoringCase(this string p_value, IList<String> p_targetList) {
			return p_value.IsIn(p_targetList, false) == false;
		}

		/// <summary>
		/// Returns TRUE if the value is NOT found in the specified list, ignoring case. 
		/// Syntactic sugar for the opposite of a case-insensitive List.Contains()
		/// </summary>
		public static bool IsNotInIgnoringCase(this string p_value, params string[] p_targetList) {
			return p_value.IsNotInIgnoringCase(p_targetList.ToList());
		}

		/// <summary>
		/// Returns TRUE if the value is NOT found in the specified list. Syntactic sugar
		/// for the opposite of List.Contains()
		/// </summary>
		public static bool IsNotIn<T>(this T p_value, params T[] p_targetList) {
			return p_value.IsIn(p_targetList) == false;
		}

		/// <summary>
		/// Returns TRUE if the value is found in the specified list. Syntactic sugar
		/// for List.Contains()
		/// </summary>
		/// <param name="isCaseSensitive">Defaults to true.</param>
		public static bool IsIn(this string p_value, IList<string> p_targetList, bool isCaseSensitive) {
			if (p_targetList == null) return false;

			return p_targetList.Contains(p_value, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>
		/// Returns the item at the specified position in the list, or default(T)
		/// if the index is out of range.
		/// </summary>
		public static T ByIndexOrDefault<T>(this IList<T> p_list, int p_index) {
			if (p_list == null)
				return default(T);
			else if (p_index >= p_list.Count)
				return default(T);
			else
				return p_list [p_index];
		}

		public static bool DoesNotContain<T>(this IList<T> p_list, T p_itemToFind) {
			if (p_list.IsNullOrEmpty())
				return true;

			return p_list.Contains(p_itemToFind) == false;
		}
	}
}
