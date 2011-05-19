using System.Collections.Generic;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

	[TestFixture]
	public class ListExtensionTests : UnitTestBase {
		[Test]
		public void Replace_ItemNotFound() {
			var list = MakeList(1, 2, 3).Replace(4, 0);

			Assert.That(list[0], Is.EqualTo(1));
			Assert.That(list[1], Is.EqualTo(2));
			Assert.That(list[2], Is.EqualTo(3));
		}

		[Test]
		public void Replace_ItemIsFound() {
			var list = MakeList(1, 2, 3).Replace(2, 0);

			Assert.That(list[0], Is.EqualTo(1));
			Assert.That(list[1], Is.EqualTo(0));
			Assert.That(list[2], Is.EqualTo(3));
		}

		[Test]
		public void Replace_ReplacesMultipleOccurrencesOfTarget() {
			var list = MakeList(1, 2, 3, 2).Replace(2, 0);

			Assert.That(list[0], Is.EqualTo(1));
			Assert.That(list[1], Is.EqualTo(0));
			Assert.That(list[2], Is.EqualTo(3));
			Assert.That(list[3], Is.EqualTo(0));
		}

		[Test]
		public void ContainsAll_EmptyList_ReturnsTrue() {
			var items = new List<int> { 1, 3, 5 };
			var itemsToFind = new List<int>();

			Assert.That(items.ContainsAll(itemsToFind), Is.True);
		}

		[Test]
		public void ContainsAll_AllItemsFound() {
			var items = new List<int> { 1, 3, 5 };
			var itemsToFind = new List<int> { 3, 5, 1, };

			Assert.That(items.ContainsAll(itemsToFind), Is.True);
		}

		[Test]
		public void ContainsAll_ItemsNotFound() {
			var items = new List<int> { 1, 3, 5 };
			var itemsToFind = new List<int> { 3, 5, 1, 7 };

			Assert.That(items.ContainsAll(itemsToFind), Is.False);
		}

		[Test]
		public void IsIn_EmptyList_ReturnsFalse() {
			var emptyList = new List<string>();
			Assert.That("foo".IsIn(emptyList), Is.False);
		}

		[Test]
		public void IsIn_ReturnsTrueIfItemIsInList() {
			var list = new List<string> { "foo", "bar", "bat" };
			Assert.That("bar".IsIn(list), Is.True);
			Assert.That("baz".IsIn(list), Is.False);
		}

		[Test]
		public void IsIn_UsingParamsObject() {
			Assert.That("foo".IsIn("foo", "bar"), Is.True);
			Assert.That("foo".IsIn("bar", "bat"), Is.False);
		}

		[Test]
		public void IsIn_CaseInsensitive() {
			var lowercaseList = new List<string> { "foo", "bar" };
			var uppercaseList = new List<string> { "FOO", "BAR" };

			Assert.That("foo".IsIn(uppercaseList, false), Is.True, "Did not ignore case");
			Assert.That("FOO".IsIn(lowercaseList, false), Is.True, "Did not ignore case");
		}
	}
}
