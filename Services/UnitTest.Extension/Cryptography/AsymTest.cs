using System;
using NUnit.Framework;

using RexToy.Cryptography;

namespace UnitTest.Utility.Cryptography
{
	[TestFixture]
	public class AsymTest
	{
		[Test]
		public void MD5Test()
		{
			IAsymmetricCrypto md5=new MD5Provider();

			string s=md5.ComputeHash("Yes");
			string t=md5.ComputeHash("Yes");
			Assert.IsTrue(s==t);

			md5.Dispose();
		}

		[Test]
		public void MD5NormalTest()
		{
			IAsymmetricCrypto md5=new MD5Provider();

			string t="Yes";
			string s=md5.ComputeHash("Yes");
			Assert.IsTrue(s!=t);

			md5.Dispose();
		}

		[Test]
		public void SHA1Test()
		{
			IAsymmetricCrypto sha1=new SHA1Provider();

			string s=sha1.ComputeHash("Yes");
			string t=sha1.ComputeHash("Yes");
			Assert.IsTrue(s==t);

			sha1.Dispose();
		}

		[Test]
		public void SHA1NormalTest()
		{
			IAsymmetricCrypto sha1=new SHA1Provider();

			string t="Yes";
			string s=sha1.ComputeHash("Yes");
			Assert.IsTrue(s!=t);

			sha1.Dispose();
		}
	}
}