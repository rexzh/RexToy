using System;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public class MD5Provider : AsymmetricCrypto
    {
        public MD5Provider()
        {
            this._algorithm = new MD5CryptoServiceProvider();
        }
    }
}