using System;
using System.Collections.Generic;

namespace Lib.Common {
    public static class DictionaryExtensions {

        /// <summary>
        /// Replaces a dictionary element, if it exists.
        /// </summary>
		public static void ReplaceKeyValue<TKey, TValue>(
			this Dictionary<TKey, TValue> p_dictionary
			, TKey p_key
			, TValue p_newValue) {

			if (p_dictionary == null) return;

			if (p_dictionary.ContainsKey(p_key))
				p_dictionary[p_key] = p_newValue;
		}

		/// <summary>
		/// Merges values from one dictionary into another, replacing any values that
		/// already exist.
		/// </summary>
		public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> p_baseDictionary
			, IDictionary<TKey, TValue> p_dictionaryToMerge) {

			p_baseDictionary.Merge(p_dictionaryToMerge, true);
		}

		/// <summary>
		/// Merges values from one dictionary into another.
		/// </summary>
		public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> p_baseDictionary
			, IDictionary<TKey, TValue> p_dictionaryToMerge
			, bool p_replaceExisting) {

			if (p_dictionaryToMerge == null) 
				return;

			foreach (KeyValuePair<TKey, TValue> pair in p_dictionaryToMerge) {
				TKey key = pair.Key;
				TValue value = pair.Value;
				
				if (key.IsNullOrEmpty())
					throw new ArgumentException("Dictionary key cannot be null or empty");

				if (p_replaceExisting || p_baseDictionary.ContainsKey(key))
					p_baseDictionary[key] = value;
			}
		}

	}
}
