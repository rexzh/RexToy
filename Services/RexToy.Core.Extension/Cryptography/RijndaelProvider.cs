using System;
using System.Text;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public class RijndaelProvider : SymmetricCrypto
    {
        public RijndaelProvider(string key)
            : base(key)
        {
            this._algorithm = new RijndaelManaged();
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                _key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                md5.Clear();
                this._algorithm.Key = _key;
            }
        }

        public RijndaelProvider(byte[] key)
            : base(key)
        {
            this._algorithm = new RijndaelManaged();
            this._algorithm.Key = _key;
        }
    }
}
