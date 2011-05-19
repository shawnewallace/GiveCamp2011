using System;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

	[TestFixture]
	public class DateTimeExtensionTests {
		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToShortDateStringExtension_NullValue_ReturnsEmptyString() {
			DateTime? date = null;

			Assert.That(date.ToShortDateString(), Is.EqualTo(String.Empty));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void ToShortDateStringExtension_NotNull_ReturnsZeroPaddedString() {
			DateTime? date = new DateTime(2008, 1, 1);

			Assert.That(date.ToShortDateString(), Is.EqualTo("01/01/2008"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_DateIsBetweenTargets() {
			var DATE1 = new DateTime(2008, 1, 1);
			var TEST_DATE = new DateTime(2008, 1, 2);
			var DATE2 = new DateTime(2008, 1, 3);

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_DateIsEqualToStartDate() {
			var DATE1 = new DateTime(2008, 1, 1);
			var DATE2 = new DateTime(2008, 1, 3);
			var TEST_DATE = DATE1;

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_DateIsEqualToEndDate() {
			var DATE1 = new DateTime(2008, 1, 1);
			var DATE2 = new DateTime(2008, 1, 3);
			var TEST_DATE = DATE2;

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_SameDayAsStartDateButTargetHasEarlierTimePortion() {
			var DATE1 = new DateTime(2008, 1, 1);
			var DATE2 = new DateTime(2008, 1, 3, 11, 00, 00);
			var TEST_DATE = DATE1.AddMinutes(-1);

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_SameDayAsEndDateButTargetHasLaterTimePortion() {
			var DATE1 = new DateTime(2008, 1, 1);
			var DATE2 = new DateTime(2008, 1, 3, 11, 00, 00);
			var TEST_DATE = DATE2.AddMinutes(1);

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_DateIsNotBetweenTargets() {
			var DATE1 = new DateTime(2008, 1, 1);
			var DATE2 = new DateTime(2008, 1, 3);
			var TEST_DATE = new DateTime(2008, 1, 4);

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2), Is.False);
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void IsBetween_CanSpecifyArgumentsAsStrings() {
			var DATE1 = "1/1/2008";
			var TEST_DATE = new DateTime(2008, 1, 2);
			var DATE2 = "1/3/2008";

			Assert.That(TEST_DATE.IsBetween(DATE1, DATE2));
		}
	}
}
