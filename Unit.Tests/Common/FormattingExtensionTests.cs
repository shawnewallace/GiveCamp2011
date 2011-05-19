using System;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class FormattingExtensionTests {
		
		[Test]
		public void ToCurrencyFormat_Decimal() {
			const decimal AMOUNT = 12.04m;
			Assert.That(AMOUNT.ToCurrencyFormat(), Is.EqualTo("$12.04"));
		}

		[Test]
		public void ToCurrencyFormat_RoundsUp() {
			const decimal AMOUNT = 12.038m;
			Assert.That(AMOUNT.ToCurrencyFormat(), Is.EqualTo("$12.04"));
		}

		[Test]
		public void ToCurrencyFormat_ReturnsEmtpyStringIfNull() {
			decimal? nullDecimal = null;
			Assert.That(nullDecimal.ToCurrencyFormat(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToDecimalFormat_RoundsUp() {
			const decimal AMOUNT = 12.038m;
			Assert.That(AMOUNT.ToDecimalFormat(), Is.EqualTo("12.04"));
		}

		[Test]
		public void ToDecimalFormat_WhenNegative() {
			const decimal AMOUNT = -12.038m;
			Assert.That(AMOUNT.ToDecimalFormat(), Is.EqualTo("-12.04"));
		}

		[Test]
		public void ToYesNoUnknown() {
			bool? yes = true;
			bool? no = false;
			bool? unknown = null;

			Assert.That(yes.ToYesNoUnknown(), Is.EqualTo("Yes"));
			Assert.That(no.ToYesNoUnknown(), Is.EqualTo("No"));
			Assert.That(unknown.ToYesNoUnknown(), Is.EqualTo("Unknown"));
		}

		[Test]
		public void ToJavascriptString_UsesAsciiEscapeCodesForBothTypesOfQuotes() {
			string unescaped = "This isn't \"escaped\"";
			string expected = @"This isn\&#39t \&#34escaped\&#34";

			Assert.That(unescaped.ToJavascriptString(), Is.EqualTo(expected));
		}

		[Test]
		public void FormatUnlessEmpty_WhenNotEmpty_FormatsAccordingToSpecifiedPattern() {
			var fooString = "Foo";

			Assert.That(fooString.FormatUnlessEmpty("Bar {0} Bat", "ignored")
				, Is.EqualTo("Bar Foo Bat"));
		}

		[Test]
		public void FormatUnlessEmpty_WhenEmpty_ReturnsSpecifiedAlternateValue() {
			string nullString = null;
			string emptyString = String.Empty;

			Assert.That(nullString.FormatUnlessEmpty("Ignored", "Empty")
				, Is.EqualTo("Empty"));

			Assert.That(emptyString.FormatUnlessEmpty("Ignored", "Empty")
				, Is.EqualTo("Empty"));
		}

		[Test]
		public void FormatUnlessEmpty_WithInnerFormatString() {
			var value = 1.2322222d;

			var formattedValueWithOutterPattern = value.FormatUnlessEmpty("Test: {0}", "#.00", "ignored");
			Assert.That(formattedValueWithOutterPattern, Is.EqualTo("Test: 1.23"));

			var formattedValueWithoutOutterPattern = value.FormatUnlessEmpty("", "#.00", "ignored");
			Assert.That(formattedValueWithoutOutterPattern, Is.EqualTo("1.23"));
		}
	}
}
