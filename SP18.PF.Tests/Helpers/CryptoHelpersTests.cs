using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SP18.PF.Web.Helpers;

namespace SP18.PF.Tests.Helpers
{
    [TestClass]
    public class CryptoHelpersTests
    {
        [TestClass]
        public class HashPassword : CryptoHelpersTests
        {
            [TestMethod]
            public void SamePasswordTwice_DifferentHash()
            {
                //arrange
                var plaintext = "password123";

                //act
                var hashed1 = CryptoHelpers.HashPassword(plaintext);
                var hashed2 = CryptoHelpers.HashPassword(plaintext);

                //assert
                Assert.IsFalse(hashed1.SequenceEqual(hashed2));
            }

            [TestMethod]
            public void EmptyPasswords_DifferentHash()
            {
                //act
                var hashed1 = CryptoHelpers.HashPassword(string.Empty);
                var hashed2 = CryptoHelpers.HashPassword(string.Empty);

                //assert
                Assert.IsFalse(hashed1.SequenceEqual(hashed2));
            }
        }

        [TestClass]
        public class VerifyPassword : CryptoHelpersTests
        {
            [TestMethod]
            public void CorrectPassword_ReturnsTrue()
            {
                //arrange
                var plaintext = "password123";
                var hashed = CryptoHelpers.HashPassword(plaintext);

                //act
                var result = CryptoHelpers.VerifyPassword(plaintext, hashed);

                //assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void WrongPassword_ReturnsFalse()
            {
                //arrange
                var hashed = CryptoHelpers.HashPassword("password123");

                //act
                var result = CryptoHelpers.VerifyPassword("wrongpassword", hashed);

                //assert
                Assert.IsFalse(result);
            }
        }
    }
}
