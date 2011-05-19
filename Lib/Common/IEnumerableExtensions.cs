using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Lib.Common {
    
    public static class IEnumerableExtensions {
    
        public static int Count(this IEnumerable p_items) {
			if (p_items == null) return 0;

			int count = 0;
			foreach (object o in p_items) count++;
			return count;
        }

		/// <summary>
		/// Returns TRUE if the collection is either null or empty.
		/// </summary>
		public static bool IsNullOrEmpty(this IEnumerable p_items) {
			if (p_items == null) return true;
			
			//Is empty if we can't move forward
			return p_items.GetEnumerator().MoveNext() == false;
		}

		/// <summary>
		/// Returns TRUE if the collection is not null and contains at least one element.
		/// </summary>
		public static bool IsNotNullOrEmpty(this IEnumerable p_items) {
			if (p_items == null) return false;

			//Is empty if we can't move forward
			return p_items.GetEnumerator().MoveNext() == true;
		}

		/// <summary>
		/// Returns a list of items from the source list that are not found in the target list.
		/// </summary>
		public static List<T> NotFoundIn<T>(this IEnumerable<T> p_base, IEnumerable<T> p_listToCheck) {

			// Perf optimizaiton: if target list is null or empty then return the base list
			if (IsNullOrEmpty(p_listToCheck))
				return new List<T>(p_base.ToEmptyIfNull());

			var list = new List<T>();

			foreach (T item in p_base.ToEmptyIfNull()) {
				if (!p_listToCheck.Contains(item)) {
					list.Add(item);
				}
			}

			return list;
		}

		public static List<T> ToEmptyIfNull<T>(this IEnumerable<T> p_list) {
			if (p_list == null) return new List<T>();
			else return p_list.ToList();
		}

		public static string Join(this IEnumerable p_list, string p_delimiter) {
			if (p_list == null) return String.Empty;

			var temp = new StringBuilder();

			foreach (object o in p_list) {
				if (o != null)
					temp.Append(p_delimiter).Append(o.ToString());
			}

			// trim the leading delim and return
			string joinedString = temp.ToString();

			return (joinedString.Length == 0)
				? ""
				: joinedString.Substring(p_delimiter.Length);
		}

		/// <summary>
		/// Returns true if the sequence contains one or more items matching the predicate.
		/// </summary>
		public static bool Contains<T>(this IEnumerable<T> p_items, Func<T, bool> p_predicate) {
			foreach (var item in p_items.Where(p_predicate))
				return true;

			return false;
		}
	}
}
