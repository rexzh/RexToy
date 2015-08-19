using System;
using System.Text;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public class DESProvider : SymmetricCrypto
    {
        public DESProvider(string key)
            : base(key)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] hashed_key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                md5.Clear();
                _key = new byte[8];
                Array.Copy(hashed_key, 0, _key, 0, 8);

                this._algorithm = new DESCryptoServiceProvider();
                this._algorithm.Key = _key;
            }
        }

        public DESProvider(byte[] key)
            : base(key)
        {
            this._algorithm = new DESCryptoServiceProvider();
            this._algorithm.Key = _key;
        }
    }
}
