using NUnit.Framework;
using PlaytikaTestTask.BI.Implementation;
using PlaytikaTestTask.Constants;
using System;

namespace UnitTests
{
    [TestFixture]
    public class CppFileNameTransformerTests
    {
        private CppFileNameTransformer cppFileNameTransformer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            cppFileNameTransformer = new CppFileNameTransformer();
        }

        [Test]
        public void Exception_FileName_Is_Null()
        {
            //arrange
            string filename = null;
            // act and assert
            Assert.Throws<ArgumentNullException>(() => cppFileNameTransformer.Transform(filename));
        }

        [Test]
        public void Equals_Filename_Transformed()
        {
            //arrange
            string sourceFileName = "name";
            string expected = sourceFileName + Constant.cppAddString;
            //act
            string actual = cppFileNameTransformer.Transform(sourceFileName);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
