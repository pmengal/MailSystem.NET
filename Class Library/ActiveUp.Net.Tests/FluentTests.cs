using System.Text.RegularExpressions;
using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    public static class FluentTests
    {
        public static void ShouldEqual(this object obj, object other)
        {
            Assert.AreEqual(other, obj);
        }

        public static void ShouldNotEqual(this object obj, object other)
        {
            Assert.AreNotEqual(other, obj);
        }

        public static void ShouldBeNull(this object obj)
        {
            Assert.IsNull(obj);
        }

        public static void ShouldNotBeNull(this object obj)
        {
            Assert.IsNotNull(obj);
        }

        public static void ShouldMatch(this string str, string pattern)
        {
            Assert.IsTrue(Regex.IsMatch(str, pattern), "Input:\n" + str + "\nDidn't match pattern: \n" + pattern);
        }
    }
}
