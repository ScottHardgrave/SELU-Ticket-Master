using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SP18.PF.Web.Helpers
{

    // see: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing
    public static class CryptoHelpers
    {
        public const int SaltSize = 128 / 8;
        public const int DerivedKeySize = 256 / 8;

        public static string HashPassword(string cleartextPassword)
        {
            var salt = GetSalt();
            return Base64String(cleartextPassword, salt);
        }

        public static bool VerifyPassword(string cleartextPassword, string hashedPassword)
        {
            var expectedBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(expectedBytes, 0, salt, 0, SaltSize);
            var actualBytes = TokenizedPasswordBytes(cleartextPassword, salt);
            return expectedBytes.Length == actualBytes.Length && expectedBytes.SequenceEqual(actualBytes);
        }

        private static byte[] GetSalt()
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static string Base64String(string cleartextPassword, byte[] salt)
        {
            var resultBytes = TokenizedPasswordBytes(cleartextPassword, salt);
            return Convert.ToBase64String(resultBytes);
        }

        private static byte[] TokenizedPasswordBytes(string cleartextPassword, byte[] salt)
        {
            cleartextPassword = cleartextPassword ?? string.Empty;
            var derivedBytes = KeyDerivation.Pbkdf2(
                cleartextPassword,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                DerivedKeySize);
            var totalArray = new byte[derivedBytes.Length + salt.Length];
            salt.CopyTo(totalArray, 0);
            derivedBytes.CopyTo(totalArray, salt.Length);
            return totalArray;
        }
    }
}