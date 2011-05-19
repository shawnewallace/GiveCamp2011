using Lib.Common;
using NUnit.Framework;

namespace Unit.Tests.Common {

    [TestFixture]
    public class ReflectionHelperTests : UnitTestBase {
		private class TestClass {
			public string StringProperty { get; set; }
			public int IntProperty { get; set; }
		}

		[Test][NUnit.Framework.Category("ShortRun")]
		public void GetPropertyName() {
			string stringPropName = 
				ReflectionHelper.GetPropertyName<TestClass, string>(t => t.StringProperty);
			
			string intPropName = 
				ReflectionHelper.GetPropertyName<TestClass, int>(t => t.IntProperty);

			Assert.That(stringPropName, Is.EqualTo("StringProperty"));
			Assert.That(intPropName, Is.EqualTo("IntProperty"));
		}
	}
}
