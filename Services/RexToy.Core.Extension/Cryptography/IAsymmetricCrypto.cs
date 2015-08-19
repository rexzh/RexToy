using System;

namespace RexToy.Cryptography
{
    public interface IAsymmetricCrypto : IDisposable
    {
        byte[] ComputeHash(byte[] raw);
        string ComputeHash(string raw);
    }
}