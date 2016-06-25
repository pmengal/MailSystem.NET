using System.Text.RegularExpressions;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    public static class FluentTests
    {
        public static void ShouldEqual(this object obj, object other, string message = null)
        {
            Assert.AreEqual(other, obj, message);
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

        public static void ShouldEqual(this Header header, Header otherHeader)
        {
            header.HeaderFieldNames.Keys.ShouldEqual(otherHeader.HeaderFieldNames.Keys, "Headers don't have the same fields");
            foreach (string headerFieldName in header.HeaderFieldNames)
                header.HeaderFields[headerFieldName].ShouldEqual(otherHeader.HeaderFields[headerFieldName], "Field [" + header.HeaderFieldNames[headerFieldName] + "] was different between the compared headers");
        }
    }
}
