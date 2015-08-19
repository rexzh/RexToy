using System;

namespace RexToy.Cryptography
{
    public interface ISymmetricCrypto : IDisposable
    {
        byte[] Encrypt(byte[] raw);
        string Encrypt(string raw);

        byte[] Decrypt(byte[] crypt);
        string Decrypt(string crypt);
    }
}