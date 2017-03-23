using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.View.Services;

namespace CS499.TCMS.ViewUnitTest
{
    [TestClass]
    public class PasswordServiceTest
    {
        [TestMethod]
        public void PasswordHashTesting()
        {
            string passphrase;
            string password = "Password1";

            string hashString = PasswordService.HashPassword(password, out passphrase);

            // validate that both the hash and the passphrase are not null
            Assert.IsFalse(string.IsNullOrEmpty(hashString));
            Assert.IsFalse(string.IsNullOrEmpty(passphrase));

            // validate password hash
            Assert.IsTrue(PasswordService.ValidatePassword(password, passphrase, hashString));

        }
    }
}
