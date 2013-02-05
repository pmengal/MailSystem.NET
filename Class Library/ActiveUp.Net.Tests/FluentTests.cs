using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    public static class FluentTests
    {
        public static void ShouldEqual(this object obj, object other)
        {
            Assert.AreEqual(other, obj);
        }
    }
}
