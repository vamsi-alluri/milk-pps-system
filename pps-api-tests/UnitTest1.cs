using BCrypt.Net;

namespace pps_api_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("hello");

            Assert.IsTrue(BCrypt.Net.BCrypt.EnhancedVerify("hello", PasswordHash));
        }
    }
}