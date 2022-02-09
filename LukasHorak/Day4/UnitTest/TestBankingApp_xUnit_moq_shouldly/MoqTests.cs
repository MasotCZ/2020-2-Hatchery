using Moq;
using Shouldly;
using System;
using Xunit;

namespace TestBankingApp_xUnit_moq_shouldly
{
    public interface ICalculator
    {
        int Add(int a, int b);
        void Clear();
        string Mode { get; set; }
        event EventHandler PoweringUp;
    }

    public class MoqTests
    {
        [Fact]
        public void TestSimpleMock()
        {
            ICalculator calculator = Mock.Of<ICalculator>(calculator => calculator.Add(1, 2) == 3);
            calculator.Add(1, 2).ShouldBe(3);
        }

        [Fact]
        public void TestPropertiesSubstitution()
        {
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            calculatorMock.SetupProperty(calculator => calculator.Mode, "DEC");

            ICalculator calculator = calculatorMock.Object;

            calculator.Mode.ShouldBe("DEC");
            calculator.Mode = "HEX";
            calculator.Mode.ShouldBe("HEX");
        }

        [Fact]
        public void TestArgumentMatchers()
        {
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            ICalculator calculator = calculatorMock.Object;

            // We return 7 when adding any number to 5. We use Arg.Any<int>() to tell NSubstitute to ignore the first argument.
            calculatorMock.Setup(c => c.Add(It.IsAny<int>(), 5)).Returns(7);
            calculator.Add(1000, 5).ShouldBe(7);

            // We return 1 when adding two numbers greater than 1000
            calculatorMock.Setup(c => c.Add(It.Is<int>(a => a > 1000), It.Is<int>(a => a > 1000))).Returns(1);
            calculator.Add(2500, 1001).ShouldBe(1);
        }

        [Fact]
        public void TestCallback()
        {
            var counter = 0;
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(c => c.Add(0, 0)).Callback(() => counter++);

            calculatorMock.Object.Add(0, 0);
            counter.ShouldBe(1);
        }

        [Fact]
        public void TestVoidCall()
        {
            var counter = 0;
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(c => c.Clear()).Callback(() => counter++);

            calculatorMock.Object.Clear();
            counter.ShouldBe(1);
        }

        [Fact]
        public void TestExceptions()
        {
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            ICalculator calculator = calculatorMock.Object;

            // Void call
            calculatorMock.Setup(c => c.Clear()).Throws<Exception>();
            Should.Throw<Exception>(() => calculator.Clear());

            // Non-void call
            calculatorMock.Setup(c => c.Add(1, 1)).Throws<ArgumentOutOfRangeException>();
            Should.Throw<Exception>(() => calculator.Add(1, 1)); // ArgumentOutOfRangeException inherits from Exception
        }

        [Fact]
        public void TestEvents()
        {
            var counter = 0;
            Mock<ICalculator> calculatorMock = new Mock<ICalculator>();
            ICalculator calculator = calculatorMock.Object;

            calculator.PoweringUp += (sender, args) => counter++;
            
            calculatorMock.Raise(c => c.PoweringUp += null, EventArgs.Empty);

            counter.ShouldBe(1);
        }
    }
}
