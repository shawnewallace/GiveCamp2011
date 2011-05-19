using System;
using System.Collections.Generic;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class ObjectExtensionTests : UnitTestBase {

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsNullOrEmpty_TreatsValueAsACollectionIfIsEnumerable() {
			object nullList = null;
			object emptyList = new List<int>();
			object nonEmptyList = MakeList(1, 2, 3);

			Assert.That(nullList.IsNullOrEmpty(), Is.True);
			Assert.That(emptyList.IsNullOrEmpty(), Is.True);
			Assert.That(nonEmptyList.IsNullOrEmpty(), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsNotNullOrEmpty_TreatsValueAsACollectionIfIsEnumerable() {
			object nullList = null;
			object emptyList = new List<int>();
			object nonEmptyList = MakeList(1, 2, 3);

			Assert.That(nullList.IsNotNullOrEmpty(), Is.False);
			Assert.That(emptyList.IsNotNullOrEmpty(), Is.False);
			Assert.That(nonEmptyList.IsNotNullOrEmpty(), Is.True);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsNullOrEmpty_TreatsValueAsAStringIfNotEnumerable() {
			object nullString = null;
			object emptyString = String.Empty;
			object nonEmptyString = "foo";

			Assert.That(nullString.IsNullOrEmpty(), Is.True);
			Assert.That(emptyString.IsNullOrEmpty(), Is.True);
			Assert.That(nonEmptyString.IsNullOrEmpty(), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsNotNullOrEmpty_TreatsValueAsAStringIfNotEnumerable() {
			object nullString = null;
			object emptyString = String.Empty;
			object nonEmptyString = "foo";

			Assert.That(nullString.IsNotNullOrEmpty(), Is.False);
			Assert.That(emptyString.IsNotNullOrEmpty(), Is.False);
			Assert.That(nonEmptyString.IsNotNullOrEmpty(), Is.True);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_IntegerValues() {
			int five = 5;
			Assert.That(five.IsBetween(4, 5));
			Assert.That(five.IsBetween(5, 6));
			Assert.That(five.IsBetween(4, 6));

			Assert.That(five.IsBetween(6, 7), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsNull_NullableInt() {
			int? nullInt = null;
			int? nonNullInt = 5;

			Assert.That(nullInt.IsNull(), Is.True);
			Assert.That(nonNullInt.IsNull(), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void FormatIfNotEmpty_ReturnsEmptyStringIfNullOrEmpty() {
			string nullString = null;
			string emptyString = String.Empty;

			Assert.That(nullString.FormatIfNotEmpty("BLAH"), Is.EqualTo(String.Empty));
			Assert.That(emptyString.FormatIfNotEmpty("BLAH"), Is.EqualTo(String.Empty));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void FormatIfNotEmpty_WithNoPlaceholders_ReturnsTheFormatStringByItself() {
			string baseValue = "Foo";
			Assert.That(baseValue.FormatIfNotEmpty("BLAH"), Is.EqualTo("BLAH"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void FormatIfNotEmpty_WithOnePlaceholder_ReplacesItWithValue() {
			string baseValue = "Foo";
			Assert.That(baseValue.FormatIfNotEmpty("BLAH {0}"), Is.EqualTo("BLAH Foo"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void FormatIfNotEmpty_NullableInt() {
			int? nullInt = null;
			int? nonNullInt = 5;

			Assert.That(nullInt.FormatIfNotEmpty("BLAH {0}"), Is.EqualTo(String.Empty));
			Assert.That(nonNullInt.FormatIfNotEmpty("BLAH {0}"), Is.EqualTo("BLAH 5"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToPropertyDictionary() {
			object testObj = new { 
				Prop1 = "Val 1",
				Prop2 = 5
			};

			var dictionary = testObj.ToPropertyDictionary();

			Assert.That(dictionary["Prop1"], Is.EqualTo("Val 1"));
			Assert.That(dictionary["Prop2"], Is.EqualTo(5));
		}
	}
}
