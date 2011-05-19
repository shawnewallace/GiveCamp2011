using System;
using NUnit.Framework;
using Lib.Common;

namespace Unit.Tests.Common.StringExtensionTests {

    [TestFixture]
    public class StringExtensionTests {
		
		[Test]
		public void MatchesRegex_IsMatch() {
			Assert.That(
				" 123 abc ".MatchesRegex(@"^\s+[0-9]+ [a-z]{3}\s$"), 
				Is.True);
		}

		[Test]
		public void MatchesRegex_IsNotMatch() {
			Assert.That(
				" 123 nomatch".MatchesRegex(@"^\s+[0-9]+ [a-z]{3}\s$"),
				Is.False);
		}

		[Test]
		public void IsNullOrEmpty() {
			string nullString = null;
			string emptyString = String.Empty;
			string nonEmptyString = "foo";

			Assert.That(nullString.IsNullOrEmpty(), Is.True);
			Assert.That(emptyString.IsNullOrEmpty(), Is.True);
			Assert.That(nonEmptyString.IsNullOrEmpty(), Is.False);
		}

		[Test]
		public void IsNotNullOrEmpty() {
			string nullString = null;
			string emptyString = String.Empty;
			string nonEmptyString = "foo";

			Assert.That(nullString.IsNotNullOrEmpty(), Is.False);
			Assert.That(emptyString.IsNotNullOrEmpty(), Is.False);
			Assert.That(nonEmptyString.IsNotNullOrEmpty(), Is.True);
		}

		[Test]
		public void ToUpperCase() {
			string testString = "tHiS iS a tESt";
			Assert.That(testString.ToUpperCase(), Is.EqualTo(testString.ToUpper()));
		}

		[Test]
		public void ToUpperCase_ReturnsEmptyStringIfValueIsNull() {
			string nullString = null;
			Assert.That(nullString.ToUpperCase(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void Extract_UsingRegex_WithNoSubgroup_ReturnsEntireMatch() {
			string target = "This is a 123-456 test";
			string extracted = target.Extract("[0-9]*-[0-9]*", "");
			Assert.That(extracted, Is.EqualTo("123-456"));
		}

		[Test]
		public void Extract_UsingRegex_WithSubgroup_ReturnsSubgroupMatch() {
			string target = "This is a 123-456 test";
			string extracted = target.Extract("([0-9]*)-[0-9]*", "");
			Assert.That(extracted, Is.EqualTo("123"));
		}

		[Test]
		public void CountMatches() {
			string target = "This is a 123-456 test";
			
			Assert.That(target.CountMatches("[0-9]+"), Is.EqualTo(2));
			Assert.That(target.CountMatches("[-_ ]"), Is.EqualTo(5));
			Assert.That(target.CountMatches("NOTFOUND"), Is.EqualTo(0));
		}

		[Test]
		public void Truncate() {
			Assert.That("12345".Truncate(3), Is.EqualTo("123"));
		}

		[Test]
		public void Truncate_ReturnsEmptyStringIfTargetIsNull() {
			string nullString = null;
			Assert.That(nullString.Truncate(5), Is.EqualTo(""));
		}

		[Test]
		public void Truncate_ReturnsEntireStringIfMaxCharactersGreaterThanStringLength() {
			Assert.That("12345".Truncate(10), Is.EqualTo("12345"));
		}

		[Test]
		public void SkipChars() {
			Assert.That("foobar".SkipChars(0), Is.EqualTo("foobar"));
			Assert.That("foobar".SkipChars(1), Is.EqualTo("oobar"));
			Assert.That("foobar".SkipChars(6), Is.EqualTo(""));

			string nullstring = null;
			Assert.That(nullstring.SkipChars(4), Is.EqualTo(""));
		}

		[Test]
		public void StartsWithIgnoringCase() {
			Assert.That("foobar".StartsWithIgnoringCase("foo"), Is.True);
			Assert.That("foobar".StartsWithIgnoringCase("FOO"), Is.True);
			Assert.That("foobar".StartsWithIgnoringCase("bar"), Is.False);

			string nullstring = null;
			Assert.That(nullstring.StartsWithIgnoringCase("foo"), Is.False);
		}

		[Test]
		public void EqualsIgnoringCase() {
			Assert.That("foobar".EqualsIgnoringCase("foobar"), Is.True);
			Assert.That("foobar".EqualsIgnoringCase("FoOBaR"), Is.True);
			Assert.That("foobar".EqualsIgnoringCase("foobar2"), Is.False);

			string nullstring = null;
			Assert.That(nullstring.EqualsIgnoringCase("foo"), Is.False);
		}

		[Test]
		public void IsNotEqualIgnoringCase() {
			Assert.That("foobar".IsNotEqualIgnoringCase("foobar"), Is.False);
			Assert.That("foobar".IsNotEqualIgnoringCase("FoOBaR"), Is.False);
			Assert.That("foobar".IsNotEqualIgnoringCase("foobar2"), Is.True);

			string nullstring = null;
			Assert.That(nullstring.IsNotEqualIgnoringCase("foo"), Is.True);
		}
	}
}
