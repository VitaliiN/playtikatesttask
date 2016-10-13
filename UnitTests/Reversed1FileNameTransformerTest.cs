using NUnit.Framework;
using PlaytikaTestTask.BI.Implementation;
using PlaytikaTestTask.Constants;
using System;

namespace UnitTests
{
    [TestFixture]
    public class Reversed1FileNameTransformerTest
    {
        private Reversed1FileNameTransformer reversed1FileNameTransformer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            reversed1FileNameTransformer = new Reversed1FileNameTransformer();
        }

        [Test]
        public void Exception_FileName_Is_Null()
        {
            //arrange
            string filename = null;
            // act and assert
            Assert.Throws<ArgumentNullException>(() => reversed1FileNameTransformer.Transform(filename));
        }

        [Test]
        public void Equals_FileName_Is_Empty()
        {
            //arrange
            string filename = String.Empty;
            string expected = String.Empty;
            // act
            string actual = reversed1FileNameTransformer.Transform(filename);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Equals_FileName_IsNot_Empty()
        {
            //arrange
            string filename = "f\\bla\\ra\\t.dat";
            string expected = "t.dat\\ra\\bla\\f";
            // act
            string actual = reversed1FileNameTransformer.Transform(filename);
            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}