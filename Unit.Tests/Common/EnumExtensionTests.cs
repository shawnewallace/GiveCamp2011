using System;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

	[TestFixture]
	public class EnumExtensionTests : UnitTestBase {

		[Test][NUnit.Framework.Category("ShortRun")]
		public void GetAttributesByType() {
			var attributes = TestEnum.Item1.GetAttributes<TestAttribute1>();

			Assert.That(attributes.Length, Is.EqualTo(2));
			AssertContains(attributes, a => a.Value == "Item1:Attr1-1", "Attribute 1 not found");
			AssertContains(attributes, a => a.Value == "Item1:Attr1-2", "Attribute 2 not found");
		}
		
		[Test][NUnit.Framework.Category("ShortRun")]
		public void GetAttributesByType_ReturnsEmptyListIfEnumHasNoMatchingAttributes() {
			var attributes = TestEnum.Item2.GetAttributes<TestAttribute1>();

			Assert.That(attributes.Length, Is.EqualTo(0));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ParseAsNullable_IfNoMatches_ReturnsNull() {
			var parsed = EnumHelper.ParseAsNullable<TestEnum>("Non-matching value");
			Assert.That(parsed, Is.Null);
		}

		[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
		private class TestAttribute1 : Attribute {
			public string Value { get; set; }
			public TestAttribute1(string p_value) { this.Value = p_value; }
		}

		[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
		private class TestAttribute2 : Attribute {
			public string Value { get; set; }
			public TestAttribute2(string p_value) { this.Value = p_value; }
		}

		private enum TestEnum {
			[TestAttribute1("Item1:Attr1-1")]
			[TestAttribute1("Item1:Attr1-2")]
			[TestAttribute2("Item1:Attr2-1")]
			Item1,

			[TestAttribute2("Item2:Attr2-1")]
			[TestAttribute2("Item2:Attr2-1")]
			Item2
		}
	}
}
