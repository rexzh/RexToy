using System;
using System.Text;
using System.Security.Cryptography;

namespace RexToy.Cryptography
{
    public abstract class AsymmetricCrypto : IAsymmetricCrypto
    {
        protected HashAlgorithm _algorithm;
        protected AsymmetricCrypto()
        {
        }

        #region IAsymmetricCrypto Members

        public virtual byte[] ComputeHash(byte[] raw)
        {
            return this._algorithm.ComputeHash(raw);
        }

        public string ComputeHash(string raw)
        {
            byte[] rawBytes = Encoding.UTF8.GetBytes(raw);
            byte[] crypted = this.ComputeHash(rawBytes);

            string hashHexText = string.Empty;
            for (int i = 0; i < crypted.Length; i++)
                hashHexText += crypted[i].ToString("X2");

            return hashHexText;
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