using System;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common
{
	[TestFixture]
	public class TimeExtensionsTester
	{
		[Test]
		public void Should_figure_out_midnight()
		{
			DateTime midnight = new DateTime(2000, 12, 3, 5, 3, 2).Midnight();
			Assert.That(midnight == new DateTime(2000, 12, 3, 0, 0, 0, 0));
		}

		[Test]
		public void Should_figure_out_noon()
		{
			DateTime noon = new DateTime(2000, 12, 3, 5, 3, 2).Noon();
			Assert.That(noon == new DateTime(2000, 12, 3, 12, 0, 0, 0));
		}

		[Test]
		public void Should_set_hour_minute_of_current_date()
		{
			DateTime testDate = new DateTime(2000, 12, 3, 5, 3, 2).SetTime(14, 2);
			Assert.That(testDate == new DateTime(2000, 12, 3, 14, 2, 0, 0));
		}

		[Test]
		public void Should_set_hour_minute_seconds_of_current_date()
		{
			DateTime testDate = new DateTime(2000, 12, 3, 5, 3, 2).SetTime(14, 2, 45);
			Assert.That(testDate == new DateTime(2000, 12, 3, 14, 2, 45, 0));
		}
	}
}