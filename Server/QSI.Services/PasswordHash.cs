using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using QSI.Services.Spec;

namespace QSI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PasswordHash : IPasswordHash
    {
        private PasswordHash() { }


        private static IPasswordHash _encryptPassword;
        private static IPasswordHash EncryptPassword
        {
            get
            {
                return _encryptPassword ??
                    (_encryptPassword = new PasswordHash());
            }
            set
            {
                _encryptPassword = value;
            }
        }


        /// <summary>
        /// Hashes a password
        /// </summary>
        /// <param name="password">The password to hash</param>

        /// <returns>The hashed password as a 128 character hex string</returns>
        public static string HashPassword(string password)
        {
            //string salt = GetRandomSalt();
            //string hash = Sha256Hex(salt + password);
            //return salt + hash;

            return EncryptPassword.HashPassword(password);
        }


        string IPasswordHash.HashPassword(string password)
        {
            string salt = GetRandomSalt();
            string hash = Sha256Hex(salt + password);
            return salt + hash;
        }

        private static IPasswordHash _passwordValidation;
        private static IPasswordHash PasswordValidation
        {
            get
            {
                return _passwordValidation ??
                    (_passwordValidation = new PasswordHash());
            }
            set
            {
                _passwordValidation = value;
            }
        }


        /// <summary>
        /// Validates a password
        /// </summary>
        /// <param name="password">The password to test</param>

        /// <param name="correctHash">The hash of the correct password</param>
        /// <returns>True if password is the correct password, false otherwise</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            return PasswordValidation.ValidatePassword(password, correctHash);
        }

        bool IPasswordHash.ValidatePassword(string password, string correctHash)
        {
            if (correctHash.Length < 128)
                throw new ArgumentException("correctHash must be 128 hex characters!");
            string salt = correctHash.Substring(0, 64);
            string validHash = correctHash.Substring(64, 64);
            string passHash = Sha256Hex(salt + password);
            return string.Compare(validHash, passHash) == 0;
        }


        //returns the SHA256 hash of a string, formatted in hex
        private static string Sha256Hex(string toHash)
        {
            SHA256Managed hash = new SHA256Managed();
            byte[] utf8 = UTF8Encoding.UTF8.GetBytes(toHash);
            return BytesToHex(hash.ComputeHash(utf8));
        }

        //Returns a random 64 character hex string (256 bits)
        private static string GetRandomSalt()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32]; //256 bits
            random.GetBytes(salt);
            return BytesToHex(salt);
        }

        //Converts a byte array to a hex string
        private static string BytesToHex(byte[] toConvert)
        {
            StringBuilder s = new StringBuilder(toConvert.Length * 2);
            foreach (byte b in toConvert)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }
    }
}
