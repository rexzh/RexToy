using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Validation;

namespace UnitTest.Extension.Validation
{
    [TestFixture]
    public class ValidateTest
    {
        private void WriteResultToConsole(IValidateResult vr)
        {
            foreach (var key in vr.ErrorKeys)
            {
                //Console.WriteLine("{0}-->{1}", key, vr[key]);
            }
        }

        [Test]
        public void Test1()
        {
            ValidateSample1 s = new ValidateSample1();
            var v = ValidatorFactory.CreateInstance().CreateValidator<ValidateSample1>();
            var result = v.Check(s);

            Assert.IsTrue(result.IsError("Salary"), "Salary");
            Assert.IsTrue(result.IsError("Code"), "Code");
            Assert.IsTrue(result.IsError("Name"), "Name");

            this.WriteResultToConsole(result);
        }

        [Test]
        public void Test2()
        {
            ValidateSample1 s = new ValidateSample1();
            s.Name = "ABC";
            s.Code = "123-45678";
            s.Salary = 100;
            var v = ValidatorFactory.CreateInstance().CreateValidator<ValidateSample1>();
            var result = v.Check(s);

            Assert.IsFalse(result.IsError("Name"), "Name");
            Assert.IsFalse(result.IsError("Salary"), "Salary");
            Assert.IsTrue(result.IsError("Code"), "Code");

            this.WriteResultToConsole(result);
        }

        [Test]
        public void Test3()
        {
            ValidateSample1 s = new ValidateSample1();
            s.Name = "ABCDEFG HIJKLMN O";
            s.Code = "123-45678";
            s.Salary = 100;
            var v = ValidatorFactory.CreateInstance().CreateValidator<ValidateSample1>();
            var result = v.Check(s);

            Assert.IsTrue(result.IsError("Name"), "Name");
            Assert.IsFalse(result.IsError("Salary"), "Salary");
            Assert.IsTrue(result.IsError("Code"), "Code");

            this.WriteResultToConsole(result);
        }

        [Test]
        public void Test4()
        {
            ValidateSample2 s = new ValidateSample2();
            s.LowerBound = -1;
            s.HighBound = 4;

            var v = ValidatorFactory.CreateInstance().CreateValidator<ValidateSample2>();
            var result = v.Check(s);

            Assert.IsTrue(result.IsError("LowerBound"), "LowerBound");
            Assert.IsFalse(result.IsError("HighBound"), "HighBound");

            this.WriteResultToConsole(result);
        }

        [Test]
        public void Test5()
        {
            ValidateSample2 s = new ValidateSample2();
            s.LowerBound = 6;
            s.HighBound = 4;

            var v = ValidatorFactory.CreateInstance().CreateValidator<ValidateSample2>();
            var result = v.Check(s);

            Assert.IsFalse(result.IsError("LowerBound"), "LowerBound");
            Assert.IsTrue(result.IsError("HighBound"), "HighBound");

            this.WriteResultToConsole(result);
        }
    }
}
