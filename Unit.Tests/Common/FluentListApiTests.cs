using System;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class FluentListApiTests : UnitTestBase {
		
		[Test][NUnit.Framework.Category("ShortRun")]
		public void InsertUnlessNullOrEmpty() {
			var list = MakeList("One", "Two")
				.InsertUnlessNullOrEmpty(1, "Three");

			Assert.That(list.Count, Is.EqualTo(3));
			Assert.That(list.Join(","), Is.EqualTo("One,Three,Two"));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void InsertUnlessNullOrEmpty_IgnoresNullOrEmptyValues() {
			var list = MakeList("One", "Two")
				.InsertUnlessNullOrEmpty(0, (string)null)
				.InsertUnlessNullOrEmpty(0, String.Empty);

			Assert.That(list.Count, Is.EqualTo(2));
			Assert.That(list.Join(","), Is.EqualTo("One,Two"));
		}
	}
}
