using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace TestBankingApp_MSTest_NSubstitute
{
    public interface ICalculator
    {
        int Add(int a, int b);
        void Clear();
        string Mode { get; set; }
        event EventHandler PoweringUp;
    }

    [TestClass]
    public class NSubstituteTest
    {
        [TestMethod]
        public void TestSimpleSubstitution()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.AreEqual(3, calculator.Add(1, 2));
        }

        [TestMethod]
        public void TestPropertiesSubstitution()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Mode.Returns("DEC");
            Assert.AreEqual("DEC", calculator.Mode);
            calculator.Mode = "HEX";
            Assert.AreEqual("HEX", calculator.Mode);
        }

        [TestMethod]
        public void TestArgumentMatchers()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            // We return 7 when adding any number to 5. We use Arg.Any<int>() to tell NSubstitute to ignore the first argument.
            calculator.Add(Arg.Any<int>(), 5).Returns(7);
            Assert.AreEqual(7,calculator.Add(1000, 5));

            // We return 1 when adding two numbers greater than 1000
            calculator.Add(Arg.Is<int>(a => a > 1000), Arg.Is<int>(a => a > 1000)).Returns(1);
            Assert.AreEqual(1, calculator.Add(2500, 1001));
        }

        [TestMethod]
        public void TestCallback()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(0, 0).Returns(x => counter++);
            calculator.Add(0, 0);
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void TestVoidCall()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.When(f => f.Clear())
                .Do(callInfo => counter++);
            calculator.Clear();
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void TestExceptions()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            // Void call
            calculator.When(f => f.Clear())
                .Do(callInfo => throw new Exception());
            Assert.ThrowsException<Exception>(() => calculator.Clear());
            // Non-void call
            calculator.Add(1, 1).Returns(x => throw new Exception());
            Assert.ThrowsException<Exception>(() => calculator.Add(1,1));
        }

        [TestMethod]
        public void TestEvents()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.PoweringUp += (sender, args) => counter++;
            calculator.PoweringUp += Raise.EventWith(new object(), new EventArgs());
            Assert.AreEqual(1, counter);
        }
    }
}
