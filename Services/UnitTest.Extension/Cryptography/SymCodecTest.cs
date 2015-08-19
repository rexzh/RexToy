using System;
using NUnit.Framework;

using RexToy.Cryptography;

namespace UnitTest.Utility.Cryptography
{
	[TestFixture]
	public class SymCodecTest
	{
		[Test]
		public void RijndaelTest()
		{
			ISymmetricCrypto codec=new RijndaelProvider("test");

			string org="hello";

            string res=codec.Encrypt(org);

			Assert.IsTrue(res!=org);

			string org1=codec.Decrypt(res);

			Assert.IsTrue(org==org1);

            codec.Dispose();
		}

		[Test]
		public void DESTest()
		{
			ISymmetricCrypto codec=new DESProvider("test");

			string org="hello";

			string res=codec.Encrypt(org);

			Assert.IsTrue(res!=org);

			string org1=codec.Decrypt(res);

			Assert.IsTrue(org==org1);

            codec.Dispose();
		}
	}
}
