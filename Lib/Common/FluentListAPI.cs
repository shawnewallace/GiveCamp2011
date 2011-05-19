using System.Collections.Generic;

namespace Lib.Common {

	/// <summary>
	/// A collection of static methods that provide a fluent API to IList.
	/// </summary>
	public static class FuentListAPI {

		public static IList<T> InsertItem<T>(
			this IList<T> p_list
			, int p_index
			, T p_value) {

			p_list.Insert(p_index, p_value);
			return p_list;
		}

		public static IList<T> InsertUnlessNullOrEmpty<T>(
			this IList<T> p_list
			, int p_index
			, T p_value) {

			if (p_value.IsNotNullOrEmpty())
				p_list.Insert(p_index, p_value);
			
			return p_list;
		}
	}
}
