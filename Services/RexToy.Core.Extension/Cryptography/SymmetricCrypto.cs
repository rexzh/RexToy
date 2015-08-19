using System;
using System.Text;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public abstract class SymmetricCrypto : ISymmetricCrypto
    {
        protected SymmetricAlgorithm _algorithm;
        protected byte[] _key;

        protected SymmetricCrypto(byte[] key)
        {
            _key = key;
        }

        protected SymmetricCrypto(string key)
        {
            _key = Encoding.UTF8.GetBytes(key);
        }

        #region ISymmetricCrypto Members

        public virtual byte[] Encrypt(byte[] raw)
        {
            return this._algorithm.CreateEncryptor(_key, _key).TransformFinalBlock(raw, 0, raw.Length);
        }

        public virtual string Encrypt(string raw)
        {
            byte[] rawBytes = Encoding.UTF8.GetBytes(raw);
            byte[] crypted = this.Encrypt(rawBytes);
            return Convert.ToBase64String(crypted);
        }

        public virtual byte[] Decrypt(byte[] crypt)
        {
            return this._algorithm.CreateDecryptor(_key, _key).TransformFinalBlock(crypt, 0, crypt.Length);
        }

        public virtual string Decrypt(string crypt)
        {
            byte[] cryptByte = Convert.FromBase64String(crypt);
            byte[] rawBytes = this.Decrypt(cryptByte);
            return Encoding.UTF8.GetString(rawBytes);
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._algorithm != null)
                    this._algorithm.Clear();
            }
        }
    }
}