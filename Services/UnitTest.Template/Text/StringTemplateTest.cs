using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Template.Text;

namespace UnitTest.Text
{
    [TestFixture]
    public class StringTemplateTest
    {
        [Test]
        public void NormalBuild()
        {
            StringTemplate st = "Let name = #{name};";
            st.Assign("name", "R");
            string result = st.Render();
            Assert.AreEqual("Let name = R;", result);
        }

        [Test]
        public void NameResetBuild()
        {
            StringTemplate st = "Let name = #{name};";
            st.Assign("name", "R");
            string result1 = st.Render();
            Assert.AreEqual("Let name = R;", result1);

            st.Assign("name", "X");
            string result2 = st.Render();
            Assert.AreEqual("Let name = X;", result2);
        }

        [Test, ExpectedException(typeof(StringTemplateException))]
        public void TestMissingNotAllow()
        {
            StringTemplate st = "[#{item1},#{item2}]";
            st.Assign("item1", "v1");
            string r = st.Render();
        }

        [Test]
        public void TestMissingAllow()
        {
            StringTemplate st = "[#{item1},#{item2}]";
            st.Assign("item1", "v1");
            string r = st.Render(true);
            Assert.AreEqual("[v1,#{item2}]", r);
        }

        [Test]
        public void TestClear()
        {
            StringTemplate st = "[#{item}]";
            st.Assign("item", "v");
            Assert.AreEqual("[v]", st.Render());

            st.Clear();
            Assert.AreEqual("[#{item}]", st.Render(true));
        }

        [Test]
        public void TestEscape()
        {
            StringTemplate st = "##{test}#{case}";
            st.Assign("case", "1");
            Assert.AreEqual("#{test}1", st.Render());
        }

        [Test, ExpectedException(typeof(StringTemplateException))]
        public void TestAssignError()
        {
            StringTemplate st = "#{item}";
            st.Assign("v", "string");
        }

        [Test, ExpectedException(typeof(StringTemplateException))]
        public void TestErrorName()
        {
            StringTemplate st = "#{item 1}";
        }
    }
}
