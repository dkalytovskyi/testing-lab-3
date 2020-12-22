using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;

namespace Lab3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void getHashNullShouldThrow()
        {
            Assert.ThrowsException<ArgumentNullException>(() => PasswordHasher.GetHash(null));
        }

        [TestMethod]
        public void getHash()
        {
            string password1 = "password";
            string password2 = "";
            string password3 = "   ";
            int length = 64;

            string hash1 = PasswordHasher.GetHash(password1);
            string hash2 = PasswordHasher.GetHash(password2);
            string hash3 = PasswordHasher.GetHash(password3);

            Assert.IsNotNull(hash1);
            Assert.AreNotEqual(hash1, password1);
            Assert.IsNotNull(hash2);
            Assert.AreNotEqual(hash2, password2);
            Assert.IsNotNull(hash3);
            Assert.AreNotEqual(hash3, password3);

            Assert.AreEqual(hash1.Length, length);
            Assert.AreEqual(hash2.Length, length);
            Assert.AreEqual(hash3.Length, length);
        }

        [TestMethod]
        public void getHashEquality()
        {
            Assert.AreEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("password"));
            Assert.AreNotEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("pass"));
        }

        [TestMethod]
        public void getHashSpecialCharacters()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("👽👽👽"));
            Assert.IsNotNull(PasswordHasher.GetHash("進撃の巨人"));
            Assert.IsNotNull(PasswordHasher.GetHash("ґєїёэ"));
            Assert.IsNotNull(PasswordHasher.GetHash("?????."));
        }

        [TestMethod]
        public void getHashAdler()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("password", null, 15));
            Assert.AreEqual(PasswordHasher.GetHash("password", null, 15), PasswordHasher.GetHash("password", null, 15));
            Assert.AreNotEqual(PasswordHasher.GetHash("password", null, 15), PasswordHasher.GetHash("password", null, 1));
        }

        [TestMethod]
        public void getHashSalt()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("password", "salty", null));
            Assert.AreEqual(PasswordHasher.GetHash("password", "salty", null), PasswordHasher.GetHash("password", "salty", null));
            Assert.AreNotEqual(PasswordHasher.GetHash("password", "salty", null), PasswordHasher.GetHash("password", "sweet", null));
        }

        [TestMethod]
        public void getHashSaltAndAdler()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("password", "salty", 15));
            Assert.AreEqual(PasswordHasher.GetHash("password", "salty", 15), PasswordHasher.GetHash("password", "salty", 15));
            Assert.AreNotEqual(PasswordHasher.GetHash("password", "salty", 15), PasswordHasher.GetHash("password", "sweet", 1));
        }

        [TestMethod]
        public void hashInit()
        {
            PasswordHasher.Init("salty", 15);
            Assert.IsNotNull(PasswordHasher.GetHash("password", "salty", 15));
            Assert.AreEqual(PasswordHasher.GetHash("password"), PasswordHasher.GetHash("password", "salty", 15));
        }
    }
}
