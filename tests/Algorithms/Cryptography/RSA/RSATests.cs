using Cnsl.Algorithms.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Cryptography
{
    [TestClass]
    public class RSATests
    {
        [TestMethod]
        public void DecryptTest()
        {
            var alica = new RSA();
            var bob = new RSA();

            const long originalMsg = 1234567890;
            var encryptedMsg = bob.Encrypt(originalMsg, alica.PublicKey);
            var decryptedMsg = alica.Decrypt(encryptedMsg);

            Assert.IsTrue(originalMsg == decryptedMsg, "The decrypted message does not match the original");
        }
    }
}