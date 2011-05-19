using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

	[TestFixture]
	public class MathExtensionTests {
		
		[Test]
		public void GetPercentageOf_ReturnsPercentageBetween0And100_Integers() {
			int piece = 5;

			Assert.That(piece.GetPercentageOf(0), Is.EqualTo(0.0d));
			Assert.That(piece.GetPercentageOf(5), Is.EqualTo(100.0d));
			Assert.That(piece.GetPercentageOf(10), Is.EqualTo(50.0d));
			Assert.That(piece.GetPercentageOf(15), Is.EqualTo(5.0d / 15.0d * 100.0d));
		}

		[Test]
		public void GetPercentageOf_ReturnsPercentageBetween0And100_Doubles() {
			double piece = 5.0d;

			Assert.That(piece.GetPercentageOf(0.0d), Is.EqualTo(0.0d));
			Assert.That(piece.GetPercentageOf(5.0d), Is.EqualTo(100.0d));
			Assert.That(piece.GetPercentageOf(10.0d), Is.EqualTo(50.0d));
			Assert.That(piece.GetPercentageOf(15.0d), Is.EqualTo(5.0d / 15.0d * 100.0d));
		}

		[Test]
		public void GetPercentageOf_ReturnsPercentageBetween0And100_NullableDoubles() {
			double? nullDouble = null;
			double? piece = 5.0d;

			Assert.That(nullDouble.GetPercentageOf(5d), Is.EqualTo(0.0d));
			Assert.That(piece.GetPercentageOf(nullDouble), Is.EqualTo(0.0d));
		}

		[Test]
		public void GetPercentageOf_ReturnsPercentageBetween0And100_NullableInts() {
			int? nullInt = null;
			int? piece = 5;

			Assert.That(nullInt.GetPercentageOf(5), Is.EqualTo(0.0d));
			Assert.That(piece.GetPercentageOf(nullInt), Is.EqualTo(0.0d));
		}
	}
}
