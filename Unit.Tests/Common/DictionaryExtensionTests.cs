using System.Collections.Generic;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class DictionaryExtensionTests : UnitTestBase {
		
		[Test][NUnit.Framework.Category("ShortRun")]
		public void ReplaceKeyValue_KeyDoesExist() {
			var dictionary = new Dictionary<int, string>();
			dictionary.Add(1, "One");
			dictionary.Add(2, "Two");

			dictionary.ReplaceKeyValue(2, "NewTwo");

			Assert.That(dictionary.Count, Is.EqualTo(2), "Count changed");
			Assert.That(dictionary[2], Is.EqualTo("NewTwo"));
		}

		public void ReplaceKeyValue_KeyDoesNotExist_NoChangesAreMade() {
			var dictionary = new Dictionary<int, string>();
			dictionary.Add(1, "One");
			dictionary.Add(2, "Two");

			dictionary.ReplaceKeyValue(3, "NewThree");

			Assert.That(dictionary.Count, Is.EqualTo(2), "Count changed");
			Assert.That(dictionary.ContainsKey(3), Is.False);
		}

		public void Merge_DoesNothingIfObjectToMergeIsNull() {
			var dict1 = new Dictionary<int, string>();
			dict1.Add(1, "One");
			dict1.Add(2, "Two");

			Dictionary<int, string> nullDict = null;

			dict1.Merge(nullDict);

			Assert.That(dict1.Count, Is.EqualTo(2));
		}

		public void Merge_ReplacingExistingValues() {
			var dict1 = new Dictionary<int, string>();
			dict1.Add(1, "One");
			dict1.Add(2, "Two");

			var dict2 = new Dictionary<int, string>();
			dict1.Add(2, "Two New");
			dict1.Add(3, "Three");

			dict1.Merge(dict2, true);

			Assert.That(dict1.Count, Is.EqualTo(3));
			Assert.That(dict1[1], Is.EqualTo("One"));
			Assert.That(dict1[2], Is.EqualTo("Two New"));
			Assert.That(dict1[3], Is.EqualTo("Three"));
		}

		public void Merge_NotReplacingExistingValues() {
			var dict1 = new Dictionary<int, string>();
			dict1.Add(1, "One");
			dict1.Add(2, "Two");

			var dict2 = new Dictionary<int, string>();
			dict1.Add(2, "Two New");
			dict1.Add(3, "Three");

			dict1.Merge(dict2, false);

			Assert.That(dict1.Count, Is.EqualTo(3));
			Assert.That(dict1[1], Is.EqualTo("One"));
			Assert.That(dict1[2], Is.EqualTo("Two"));
			Assert.That(dict1[3], Is.EqualTo("Three"));
		}
	}
}
