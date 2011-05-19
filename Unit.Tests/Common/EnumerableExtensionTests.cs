using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lib.Common;

namespace Unit.Tests.Common {

    [TestFixture]
    public class EnumerableExtensionTests : UnitTestBase {
		
		[Test]
		public void IsNullOrEmpty() {
			IEnumerable<int> nullList = null;
			IEnumerable<int> emptyList = new List<int>();
			IEnumerable<int> nonEmptyList = MakeList(1, 2);

			Assert.That(nullList.IsNullOrEmpty(), Is.True);
			Assert.That(emptyList.IsNullOrEmpty(), Is.True);
			Assert.That(nonEmptyList.IsNullOrEmpty(), Is.False);
		}

		[Test]
		public void IsNotNullOrEmpty() {
			IEnumerable<int> nullList = null;
			IEnumerable<int> emptyList = new List<int>();
			IEnumerable<int> nonEmptyList = MakeList(1, 2);

			Assert.That(nullList.IsNotNullOrEmpty(), Is.False);
			Assert.That(emptyList.IsNotNullOrEmpty(), Is.False);
			Assert.That(nonEmptyList.IsNotNullOrEmpty(), Is.True);
		}

		[Test]
		public void NotFoundIn() {
			var list1 = MakeList(1, 2, 3, 4);
			var list2 = MakeList(2, 3);
			var list3 = list1.NotFoundIn(list2);

			Assert.That(list3, Is.EquivalentTo(new int[] { 1, 4 }));
		}

		[Test]
		public void NotFoundIn_TargetListIsNullOrEmpty_ReturnsOriginalList() {
			List<int> sourceList = MakeList(1, 2, 3, 4);
			List<int> nullList = null;
			List<int> emptyList = new List<int>();

			Assert.That(sourceList.NotFoundIn(nullList), Is.EquivalentTo(sourceList));
			Assert.That(sourceList.NotFoundIn(emptyList), Is.EquivalentTo(sourceList));
		}

		[Test]
		public void NotFoundIn_SourceListIsNullOrEmpty_ReturnsNewEmptyList() {
			List<int> nonEmptyList = MakeList(1, 2, 3, 4);
			List<int> nullList = null;
			List<int> emptyList = new List<int>();

			Assert.That(nullList.NotFoundIn(nonEmptyList), Is.EquivalentTo(emptyList));
			Assert.That(nullList.NotFoundIn(emptyList), Is.EquivalentTo(emptyList));
		}

		[Test]
		public void Join() {
			var emptyList = new List<string>();
			var oneItemList = new List<string> { "One" };
			var twoItemList = new List<string> { "One", "Two" };

			Assert.That(emptyList.Join(","), Is.EqualTo(String.Empty));
			Assert.That(oneItemList.Join(","), Is.EqualTo("One"));
			Assert.That(twoItemList.Join(","), Is.EqualTo("One,Two"));
		}

		[Test]
		public void Join_IgnoresNullElements() {
			var listWithNull = new List<string> { "One", null, "Two" };
			Assert.That(listWithNull.Join(","), Is.EqualTo("One,Two"));
		}

		[Test]
		public void Contains_WithPredicate() {
			var testList = new List<string> { "One", "Two", "Three" };

			Assert.That(testList.Contains(s => s.StartsWith("T")), Is.True);
			Assert.That(testList.Contains(s => s.StartsWith("Z")), Is.False);
		}
	}
}
