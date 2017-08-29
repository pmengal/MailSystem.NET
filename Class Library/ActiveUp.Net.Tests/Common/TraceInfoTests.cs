using ActiveUp.Net.Mail;
using NUnit.Framework;
using System;

namespace ActiveUp.Net.Tests.Common
{
    [TestFixture]
    public class TraceInfoTests
    {
        [Test]
        public void should_process_null_values()
        {
            var test = new TraceInfo(null, DateTime.MinValue, null, null, null, null, null);
            Assert.AreEqual("", test.ToString());
        }
    }
}
