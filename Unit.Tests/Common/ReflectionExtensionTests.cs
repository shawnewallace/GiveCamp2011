using System.Linq;
using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class ReflectionExtensionTests : UnitTestBase {
		private class TestClass {
			public const string TEST_STRING_CONSTANT = "foo";
			public const int TEST_INT_CONSTANT = 42;

			public string StringProperty { get; set; }
			public int IntProperty { get; set; }
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void GetPropertyValue() {
			var obj = new TestClass() { StringProperty = "String Test" };
			Assert.That(
				obj.GetPropertyValue("StringProperty"),
				Is.EqualTo(obj.StringProperty));
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void GetConstantsStartingWith() {
			var names = typeof(TestClass).GetConstantsStartingWith("TEST_").Select(f => f.Name);
			var expected = MakeList("TEST_STRING_CONSTANT", "TEST_INT_CONSTANT");
			Assert.That(names, Is.EquivalentTo(expected));
		}
	}
}
