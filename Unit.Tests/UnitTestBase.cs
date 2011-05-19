using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.Web.Mvc;
using NUnit.Framework;
using Lib.Common;

namespace Unit.Tests {
	public class UnitTestBase {

		#region Assertion helpers

		/// <summary>
		/// Asserts that a dictionary contains a specified key and value.
		/// </summary>
		protected void AssertContains(
			IDictionary<string, object> p_dict
			, string p_key
			, object p_value) {

			Assert.That(p_dict.ContainsKey(p_key), "The key '" 
				+ p_key + "' was not found.");

			Assert.That(p_dict[p_key], Is.EqualTo(p_value));
		}

		protected void AssertContains<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate) {
			AssertContains(p_items, p_predicate, "The expected item was not found.");
		}

		protected void AssertContains<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate
			, string p_message) {

			if (p_items == null)
				Assert.Fail(p_message + " [Sequence was null]");

			Assert.That(p_items.Count(p_predicate), Is.Not.EqualTo(0), p_message);
		}

		protected void AssertAll<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate) {
			AssertAll(p_items, p_predicate, "Some unexpected items were found in the collection.");
		}

		protected void AssertAll<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate
			, string p_message) {

			Assert.That(p_items, Is.Not.Null, p_message);

			Assert.That(p_items.Count(), Is.EqualTo(p_items.Count(p_predicate)), p_message);
		}

		protected void AssertNone<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate) {
			AssertNone(p_items, p_predicate, "Some unexpected items were found in the collection.");
		}

		protected void AssertNone<T>(IEnumerable<T> p_items, Func<T, bool> p_predicate
			, string p_message) {

			Assert.That(p_items.Count(p_predicate), Is.EqualTo(0), p_message);
		}

		protected void AssertIsEmpty(IEnumerable p_items, string p_message) {
			Assert.That(p_items.Count(), Is.EqualTo(0), p_message);
		}

		protected void AssertIsNullOrEmpty(IEnumerable p_items) {
			AssertIsNullOrEmpty(p_items, "Expected a null or empty sequence.");
		}

		protected void AssertIsNullOrEmpty(IEnumerable p_items, string p_message) {
			if (p_items.IsNotNullOrEmpty()) {
				int itemCount = p_items.Count();

				var firstFewItems = p_items
					.Cast<object>()
					.Select(i => i.ToStringNullSafe("<null>"))
					.Take(2)
					.Join(", ");

				var errorMessage = p_message + String.Format(
					" [Sequence has {0} items starting with: {1}]", itemCount, firstFewItems);

				Assert.Fail(errorMessage);
			}
		}

		protected void AssertPropertiesAreEqual(object p_actual, object p_expected) {
			PropertyInfo[] properties = p_expected
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			
			foreach (PropertyInfo property in properties) {
				try {
					object expectedValue = property.GetValue(p_expected, null);
					object actualValue = property.GetValue(p_actual, null);

					if (actualValue is ICollection) {
						Assert.That(
							(ICollection)actualValue,
							Is.EquivalentTo((ICollection)expectedValue),
							"Multi-valued property '" + property.Name + "' is incorrect.");
					}
					// DateTime loses a little precision when it round-trips through the DB
					else if (actualValue is DateTime) {
						var minAllowed = ((DateTime)expectedValue).AddMilliseconds(-250);
						var maxAllowed = ((DateTime)expectedValue).AddMilliseconds(250);

						Assert.That(actualValue, Is.GreaterThanOrEqualTo(minAllowed)
							, "Property '" + property.Name + "' is incorrect (less than allowed tolerance).");
						Assert.That(actualValue, Is.LessThanOrEqualTo(maxAllowed)
							, "Property '" + property.Name + "' is incorrect (greater than allowed tolerance).");
					}
					else {
						Assert.That(actualValue, Is.EqualTo(expectedValue)
							, "Property '" + property.Name + "' is incorrect.");
					}
				}
				catch (Exception ex) {
					throw new ApplicationException("Exception testing property '" 
						+ property.Name + "' of type '" + p_actual.GetType() + "': " 
						+ ex.Message, ex);
				}
			}
		}

		/// <summary>
		/// Compares two lists and ensures that the items in each list have equivalent
		/// values in the specified property. (For instance, we could compare two lists
		/// of business objects and assert that they contain equivalent ID values)
		/// </summary>
		protected void AssertAreEquivalent<TClass, TProp>(
			IEnumerable<TClass> p_actualList
			, IEnumerable<TClass> p_expectedList
			, Expression<Func<TClass, TProp>> p_propertyToCompare) {

			if (p_actualList == null && p_expectedList == null)
				return;

			Assert.That(p_actualList.Count(), Is.EqualTo(p_expectedList.Count())
				, "Lists being compared have different item counts");

			string propName = ReflectionHelper.GetPropertyName<TClass, TProp>(p_propertyToCompare);

			var actualValues = new List<TProp>();
			foreach (TClass item in p_actualList) {
				actualValues.Add(item.GetPropertyValue<TProp>(propName));
			}

			var expectedValues = new List<TProp>();
			foreach (TClass item in p_expectedList) {
				expectedValues.Add(item.GetPropertyValue<TProp>(propName));
			}

			Assert.That(actualValues, Is.EquivalentTo(expectedValues)
				, "Values in the '" + propName + "' field of type '" + typeof(TClass).Name
				+ "' were not equivalent.");
		}

		/// <summary>
		/// Assert that one date is +/- an allowed range of another date.
		/// </summary>
		/// <remarks>
		/// Used when round-tripping dates through the database, which can cause a slight
		/// loss of precison, or for asserting against DB timestamps.
		/// </remarks>
		protected void AssertDatesAreWithinAllowedThreshold(DateTime? p_dateToTest, DateTime p_targetDate) {
			const int DEFAULT_ALLOWED_DIFFERENCE_IN_MILLISECONDS = 5000;

			AssertDatesAreWithinAllowedThreshold(
				p_dateToTest
				, p_targetDate
				, DEFAULT_ALLOWED_DIFFERENCE_IN_MILLISECONDS);
		}

		/// <summary>
		/// Assert that one date is +/- the specified number of milliseconds from another date.
		/// </summary>
		/// <remarks>
		/// Used when round-tripping dates through the database, which can cause a slight
		/// loss of precison, or for asserting against DB timestamps.
		/// </remarks>
		protected void AssertDatesAreWithinAllowedThreshold(
			DateTime? p_dateToTest
			, DateTime p_targetDate
			, int p_allowedDifferenceInMilliseconds) {

			if (p_dateToTest.HasValue == false)
				Assert.Fail("Date to test is null!");

			DateTime testDate = p_dateToTest.Value;
			
			DateTime minDate = p_targetDate.AddMilliseconds(-1 * p_allowedDifferenceInMilliseconds);
			DateTime maxDate = p_targetDate.AddMilliseconds(p_allowedDifferenceInMilliseconds);

			if (testDate < minDate) {
				Assert.Fail("Tested date '" + testDate.ToString() + "' is less than the min"
					+ " allowed date of '" + minDate.ToString() + "'.");
			}
			else if (testDate > maxDate) {
				Assert.Fail("Tested date '" + testDate.ToString() + "' is greater than the max"
					+ " allowed date of '" + maxDate.ToString() + "'.");
			}
		}

		#endregion

        public static List<T> MakeList<T>(params T[] values)
        {
            return values.ToList();
        }
	}

	#region Test-specific extensions

	public static class TestSpecificExtensions {
		/// <summary>
		/// Returns the first item from the specified sequence, or fails the current
		/// test if the sequence is empty.
		/// </summary>
		public static T FirstOrFail<T>(this IQueryable<T> p_items, string p_failMsg) {
			try {
				return p_items.First();
			}
			catch (NullReferenceException ex) {
				throw new NullReferenceException(p_failMsg + " [Collection was null]", ex);
			}
			catch (InvalidOperationException ex) {
				throw new InvalidOperationException(p_failMsg + " [Collection was null]", ex);
			}
		}

		/// <summary>
		/// Returns the first item from the specified sequence, or fails the current
		/// test if the sequence is empty.
		/// </summary>
		public static T FirstOrFail<T>(this IEnumerable<T> p_items, string p_failMsg) {
			if (p_items == null)
				Assert.Fail(p_failMsg + " [Items collection is null]");

			// todo: differentiate between value and reference types of T. For ref
			// types we don't need to check the length, we can check for null.
			var itemList = p_items.ToList();

			if (itemList.Count == 0)
				Assert.Fail(p_failMsg + " [Sequence was empty]");

			return itemList.First();
		}

		/// <summary>
		/// A wrapper for Single(), with a custom failure message.
		/// </summary>
		public static T SingleOrFail<T>(this IEnumerable<T> p_items, string p_failMsg) {
			if (p_items == null)
				Assert.Fail(p_failMsg + " [Items collection is null]");

			try {
				return p_items.Single();
			}
			catch (InvalidOperationException emptySequenceEx) {
				Assert.Fail(p_failMsg + " [Ex: " + emptySequenceEx.Message + "]");
				throw;
			}
		}

		/// <summary>
		/// Returns a strongly typed item from a dictionary, or fails with the specified message.
		/// </summary>
		public static T GetOrFail<T>(
			this Dictionary<string, object> p_items
			, string p_key
			, string p_failMsg) {
			
			if (p_items == null)
				Assert.Fail(p_failMsg + " [Dictionary is null]");

			if (p_items.ContainsKey(p_key) == false)
				Assert.Fail(p_failMsg + " [Key '" + p_key + "' not found]");

			try {
				return (T)p_items[p_key];
			}
			catch (InvalidCastException) {
				Assert.That(p_items[p_key], Is.TypeOf(typeof(T)), p_failMsg + " [Wrong type in dictionary]");
				throw;
			}
		}

		public static T GetOrFail<T>(this ViewDataDictionary p_items, string p_key, string p_failMsg)
		{
			if (p_items == null)
				Assert.Fail(p_failMsg + " [ViewDataDictionary is null]");

			if (p_items.ContainsKey(p_key) == false)
				Assert.Fail(p_failMsg + " [Key not found]");

			try {
				return (T)p_items[p_key];
			}
			catch (InvalidCastException) {
				Assert.That(p_items[p_key], Is.TypeOf(typeof(T)), p_failMsg + " [Wrong type in dictionary]");
				throw;
			}
		}

		public static List<T> TakeOrFail<T>(this IEnumerable<T> p_items, int p_count, string p_failMsg) {
			var selectedItems = p_items.Take(p_count).ToList();

			int selectedCount = selectedItems.Count;

			if (selectedCount != p_count)
				Assert.Fail(String.Format("{0} [Expected {1} items, but found {2}]", p_failMsg, p_count, selectedCount));

			return selectedItems;
		}

		public static List<T> TakeOrFail<T>(this IQueryable<T> p_items, int p_count, string p_failMsg) {
			var selectedItems = p_items.Take(p_count).ToList();

			int selectedCount = selectedItems.Count;

			if (selectedCount != p_count)
				Assert.Fail(String.Format("{0} [Expected {1} items, but found {2}]", p_failMsg, p_count, selectedCount));

			return selectedItems;
		}

		public static List<T> FailIfEmptyOrNull<T>(this IEnumerable<T> p_items, string p_failMsg) {
			if (p_items == null)
				Assert.Fail(p_failMsg);

			var list = p_items.ToList();

			if (list.Count == 0)
				Assert.Fail(p_failMsg);

			return list;
		}
	}

	#endregion

}
