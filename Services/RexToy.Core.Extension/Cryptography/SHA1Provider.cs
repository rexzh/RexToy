using System;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public class SHA1Provider : AsymmetricCrypto
    {
        public SHA1Provider()
        {
            this._algorithm = new SHA1CryptoServiceProvider();
        }
    }
}