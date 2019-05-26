using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void Test_Divide()
        {
            //Arrande
            int expected = 5;
            int numerator = 20;
            int denominator = 4;

            //Act
            int actual = Calculator.Library.Calculator.Divide(numerator, denominator);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Divide01()
        {
            //Arrande
            int expected = 5;
            int numerator = 25;
            int denominator = 5;

            //Act
            int actual = Calculator.Library.Calculator.Divide(numerator, denominator);

            // Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
