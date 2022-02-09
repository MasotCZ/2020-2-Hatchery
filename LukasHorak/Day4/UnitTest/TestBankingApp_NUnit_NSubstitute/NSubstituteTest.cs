using NSubstitute;
using NUnit.Framework;
using System;

namespace TestBankingApp_NUnit_NSubstitute
{
    public interface ICalculator
    {
        int Add(int a, int b);
        void Clear();
        string Mode { get; set; }
        event EventHandler PoweringUp;
    }

    [TestFixture]
    public class NSubstituteTest
    {
        [Test]
        public void TestSimpleSubstitution()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.That(() => { return calculator.Add(1, 2); }, Is.EqualTo(3));
        }

        [Test]
        public void TestPropertiesSubstitution()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Mode.Returns("DEC");
            Assert.That(calculator.Mode, Is.EqualTo("DEC"));
            calculator.Mode = "HEX";
            Assert.That(calculator.Mode, Is.EqualTo("HEX"));
        }

        [Test]
        public void TestArgumentMatchers()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            // We return 7 when adding any number to 5. We use Arg.Any<int>() to tell NSubstitute to ignore the first argument.
            calculator.Add(Arg.Any<int>(), 5).Returns(7);
            Assert.That(() => { return calculator.Add(1000, 5); }, Is.EqualTo(7));

            // We return 1 when adding two numbers greater than 1000
            calculator.Add(Arg.Is<int>(a => a > 1000), Arg.Is<int>(a => a > 1000)).Returns(1);
            Assert.That(() => { return calculator.Add(2500, 1001); }, Is.EqualTo(1));
        }

        [Test]
        public void TestCallback()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(0, 0).Returns(x => counter++);
            calculator.Add(0, 0);
            Assert.That(counter, Is.EqualTo(1));
        }

        [Test]
        public void TestVoidCall()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.When(f => f.Clear())
                .Do(callInfo => counter++);
            calculator.Clear();
            Assert.That(counter, Is.EqualTo(1));
        }

        [Test]
        public void TestExceptions()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            // Void call
            calculator.When(f => f.Clear())
                .Do(callInfo => throw new Exception());
            Assert.That(() => { calculator.Clear(); }, Throws.Exception);
            // Non-void call
            calculator.Add(1, 1).Returns(x => throw new Exception());
            Assert.That(() => { return calculator.Add(1,1); }, Throws.Exception);
        }

        [Test]
        public void TestEvents()
        {
            var counter = 0;
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.PoweringUp += (sender, args) => counter++;
            calculator.PoweringUp += Raise.EventWith(new object(), new EventArgs());
            Assert.That(counter, Is.EqualTo(1));
        }
    }
}
