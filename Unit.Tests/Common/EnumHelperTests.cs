using System;
using Lib.Common;
using Lib.Infrastructure;
using NUnit.Framework;

namespace Unit.Tests.Common {

	[TestFixture]
	public class EnumHelperTests {
		private enum TestEnum {
			WithoutAttribute,

            [Lib.Infrastructure.Description("WithAttribute_SameAsName")]
			WithAttribute_SameAsName,

            [Lib.Infrastructure.Description("Attribute value not the same as ToString()")]
			WithAttribute_DifferentThanName,

			[StringConstant("WITH_CONSTANT")]
			[Lib.Infrastructure.Description("WithStringConstant")]
			WithStringConstant
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToDescriptionExtensionMethod_NoDescription_ReturnsToString() {
			var testEnum = TestEnum.WithoutAttribute;
			Assert.That(testEnum.ToDescription(), Is.EqualTo(TestEnum.WithoutAttribute.ToString()));
		}
		
		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToDescriptionExtensionMethod_HasDescription() {
			var testEnum = TestEnum.WithAttribute_DifferentThanName;
			Assert.That(testEnum.ToDescription(), Is.EqualTo("Attribute value not the same as ToString()"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_ReturnsEnumWithMatchingStringConstant() {
			var parsed = EnumHelper.Parse<TestEnum>("WITH_CONSTANT");
			Assert.That(parsed, Is.EqualTo(TestEnum.WithStringConstant));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_ReturnsEnumWithMatchingDescription_DescriptionSameAsToString() {
			var parsed = EnumHelper.Parse<TestEnum>("WithAttribute_SameAsName");
			Assert.That(parsed, Is.EqualTo(TestEnum.WithAttribute_SameAsName));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_ReturnsEnumWithMatchingDescription_DescriptionDifferentThanToString() {
			var parsed = EnumHelper.Parse<TestEnum>("Attribute value not the same as ToString()");
			Assert.That(parsed, Is.EqualTo(TestEnum.WithAttribute_DifferentThanName));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_IfNoDescriptionMatches_ReturnsEnumWithMatchingToStringValue() {
			var parsed = EnumHelper.Parse<TestEnum>("WithoutAttribute");
			Assert.That(parsed, Is.EqualTo(TestEnum.WithoutAttribute));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_IfNoMatches_ReturnsDefaultValue() {
			var parsed = EnumHelper.Parse<TestEnum>("Non-matching value", TestEnum.WithAttribute_SameAsName);
			Assert.That(parsed, Is.EqualTo(TestEnum.WithAttribute_SameAsName));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void Parse_IfNoMatches_ThrowsException() {
			try {
				var parsed = EnumHelper.Parse<TestEnum>("Non-matching value");
				Assert.Fail("No exception parsing invalid enum value.");
			}
			catch (ArgumentException) { /* this passes test */ }
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ParseAsNullable_IfNoMatches_ReturnsNull() {
			var parsed = EnumHelper.ParseAsNullable<TestEnum>("Non-matching value");
			Assert.That(parsed, Is.Null);
		}
	}
}
