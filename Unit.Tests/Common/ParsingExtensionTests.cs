using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lib.Common;

namespace Unit.Tests.Common {

	[TestFixture]
	public class ParsingExtensionTests : UnitTestBase {
		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToDecimal_GoodData() {
			Assert.That("5.5".ToDecimal(0m), Is.EqualTo(5.5m));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToDecimal_ReturnsDefaultIfParseFails() {
			Assert.That("5.5f".ToDecimal(0m), Is.EqualTo(0m));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableInt_GoodData() {
			Assert.That("5".ToNullableInt(), Is.EqualTo(5));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableInt_BadData() {
			Assert.That("".ToNullableInt(), Is.Null);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableDate_GoodData() {
			Assert.That(
				"01/01/2008".ToNullableDate()
				, Is.EqualTo(new DateTime(2008, 1, 1)));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableDate_BadData() {
			Assert.That(
				"01/32/2008".ToNullableDate()
				, Is.EqualTo(null));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToBoolean_ParsesMultipleRepresentationsOfTrue() {
			var stringsThatShouldParseAsTrue = new List<string> {
				"t", "true", "y", "yes", "1" };

			foreach (string s in stringsThatShouldParseAsTrue) {
				Assert.That(s.ToBoolean(), Is.True, "'" + s + "' should parse as true.");
				Assert.That(s.ToUpper().ToBoolean(), Is.True, "'" + s.ToUpper() + "' should parse as true.");
			}
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToBoolean_ParsesMultipleRepresentationsOfFalse() {
			var stringsThatShouldParseAsFalse = new List<string> {
				"f", "false", "n", "no", "0" };

			foreach (string s in stringsThatShouldParseAsFalse) {
				Assert.That(s.ToBoolean(), Is.False, "'" + s + "' should parse as false.");
				Assert.That(s.ToUpper().ToBoolean(), Is.False, "'" + s.ToUpper() + "' should parse as false.");
			}
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToBoolean_WithDefaultValue() {
			Assert.That("blah".ToBoolean(true), Is.True);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableBoolean() {
			Assert.That("true".ToNullableBoolean(), Is.True);
			Assert.That("blah".ToNullableBoolean(), Is.Null);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToIntList_BadData() {
			List<int> list;

			// Null
			list = ((string)null).ToIntList();
			Assert.That(list, Is.Not.Null);
			Assert.That(list, Is.Empty);

			// Empty string
			list = "".ToIntList();
			Assert.That(list, Is.Not.Null);
			Assert.That(list, Is.Empty);

			// Delimited, but no good values
			list = "a,A4,5B".ToIntList();
			Assert.That(list, Is.Not.Null);
			Assert.That(list, Is.Empty);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToIntList_GoodData() {
			List<int> list;

			// One value only
			list = "4".ToIntList(",");
			Assert.That(list, Is.Not.Null);
			Assert.That(list.Count, Is.EqualTo(1));
			Assert.That(list[0], Is.EqualTo(4));

			// Two values
			list = "4,5".ToIntList(",");
			Assert.That(list, Is.Not.Null);
			Assert.That(list.Count, Is.EqualTo(2));
			Assert.That(list[1], Is.EqualTo(5));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToIntList_MixedData() {
			List<int> list = "4,foo,5".ToIntList(",");

			Assert.That(list, Is.Not.Null);
			Assert.That(list.Count, Is.EqualTo(2));
			Assert.That(list[0], Is.EqualTo(4));
			Assert.That(list[1], Is.EqualTo(5));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnum_WithoutDescription() {
			Assert.That(
				"WithoutDescription".ToEnum<TestEnum>(), 
				Is.EqualTo(TestEnum.WithoutDescription));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnum_WithDescription() {
			Assert.That(
				"With Different Description".ToEnum<TestEnum>(),
				Is.EqualTo(TestEnum.WithDifferentDescription));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnum_WithDefaultValue() {
			Assert.That(
				"foo".ToEnum<TestEnum>(TestEnum.WithEqualDescription),
				Is.EqualTo(TestEnum.WithEqualDescription));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnumList() {
			var enums = MakeList(TestEnum.WithoutDescription, TestEnum.WithEqualDescription);

			var strings = new List<string>();
			enums.ForEach(e => strings.Add(e.ToString()));

			Assert.That(
				strings.ToEnumList<TestEnum>(), 
				Is.EquivalentTo(enums));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnumList_ThrowsExceptionIfGivenBadData() {
			var list = MakeList(
				TestEnum.WithEqualDescription.ToString()
				, "foo"
				, TestEnum.WithoutDescription.ToString());
			
			try {
				list.ToEnumList<TestEnum>();
				Assert.Fail("No exception when passed bad data");
			}
			catch (Exception) { /* this passes test */ }
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnumList_NullList_ReturnsEmptyListOfEnums() {
			List<string> nullList = null;
			
			Assert.That(nullList.ToEnumList<TestEnum>(), Is.Empty);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnumList_EmptyListOfStrings_ReturnsEmptyListOfEnums() {
			Assert.That(
				new string[0].ToEnumList<TestEnum>(),
				Is.Empty);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToEnumList_IgnoresNullOrEmptyStrings() {
			var strings = new List<string>();
			strings.Add(TestEnum.WithoutDescription.ToString());
			strings.Add(null);
			strings.Add("");

			var expectedEnums = MakeList(TestEnum.WithoutDescription);

			Assert.That(
				strings.ToEnumList<TestEnum>(),
				Is.EquivalentTo(expectedEnums));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToNullableEnum() {
			Assert.That(
				"foo".ToNullableEnum<TestEnum>(),
				Is.Null);
		}

		private enum TestEnum {
			WithoutDescription,

			[Lib.Infrastructure.Description("WithEqualDescription")]
			WithEqualDescription,

			[Lib.Infrastructure.Description("With Different Description")]
			WithDifferentDescription
		}
	}
}
