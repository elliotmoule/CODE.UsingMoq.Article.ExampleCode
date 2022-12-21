using ExampleProject.Library;
using Moq;
using Moq.Protected;

namespace ExampleProject.Testing;
public class RateCalculatorTests
{
	[Test]
	[Ignore(reason: "This test is designed to fail, so should be skipped, given that it might not be Sunday when it is ran.")]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet2()
	{
		// Arrange
		var rateCalculator = new RateCalculator();

		// Act
		var actual = rateCalculator.GetPayRate(10.00m);

		// Assert - This will fail, unless it is Sunday.
		Assert.That(actual, Is.EqualTo(12.5m));
	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet4()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock =
			new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);

		// Act
		var actual = rateCalculator.GetPayRate(10.00m,
			dateTimeProviderMock.Object);

		// Assert
		Assert.That(actual, Is.EqualTo(12.5m));
	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet5()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock =
			new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);
		dateTimeProviderMock.Object.Name = "DateTimeProvide#1";

		// Act
		var actual = rateCalculator.GetPayRate(10.00m,
			dateTimeProviderMock.Object);

		// Assert
		Assert.That(actual, Is.EqualTo(12.5m));
	}

	[Test]
	public void DoNewAction_MockGet_ReturnsFalse_Snippet6()
	{
		IExampleInterface foo =
			ExampleClass.ReturnedFromSomewhereElse();

		var fooMock = Mock.Get(foo);
		fooMock.Setup(m => m.DoNewAction()).Returns(false);

		Assert.That(fooMock.Object.DoNewAction(), Is.EqualTo(false));
	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet7()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock
				  = new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);

		// Act
		var actual = rateCalculator.GetPayRate(10.00m,
			dateTimeProviderMock.Object);

		// Assert
		Assert.That(actual, Is.EqualTo(12.5m));
		dateTimeProviderMock.Verify(m => m.DayOfWeek(),
										 Times.Once());

	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet8()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock
				  = new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);

		var rates = new List<decimal>();
		// Act
		for (int i = 0; i < 3; i++)
		{
			rates.Add(rateCalculator.GetPayRate(10.00m,
					dateTimeProviderMock.Object));
		}

		// Assert
		Assert.That(rates.Sum() / 3, Is.EqualTo(12.5m));

		// This is expected to run exactly three (3) times.
		dateTimeProviderMock.Verify(m => m.DayOfWeek(),
										 Times.Exactly(3));

		// And this property should never run.
		dateTimeProviderMock.Verify(m => m.Name,
										 Times.Never());
	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet9()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock = new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);

		// Act
		var actual = rateCalculator.GetPayRate(10.00m,
			dateTimeProviderMock.Object);

		// Assert
		Assert.That(actual, Is.EqualTo(12.5m));
		dateTimeProviderMock.VerifyAll();
	}

	[Test]
	public void GetPayRate_IsSunday_ReturnsHigherRate_Snippet10()
	{
		// Arrange
		var rateCalculator = new RateCalculator();
		var dateTimeProviderMock
				  = new Mock<IDateTimeProvider>();
		dateTimeProviderMock.Setup(m => m.DayOfWeek())
							.Returns(DayOfWeek.Sunday);

		var rates = new List<decimal>();
		// Act
		var actual = rateCalculator.GetPayRate(10.00m,
						 dateTimeProviderMock.Object);


		// Assert
		Assert.That(actual, Is.EqualTo(12.5m));

		// This is expected to run exactly three (3) times.
		dateTimeProviderMock.Verify(m => m.DayOfWeek(),
										 Times.Exactly(1));

		// And this property should never run.
		dateTimeProviderMock.VerifyNoOtherCalls();
	}

	[Test]
	public void MyCalculatorMethod_ReturnsTrue_Snippet11()
	{
		// Arrange
		var mock = new Mock<IExampleInterface>();
		mock.Setup(m => m.MyCalculatorMethod("Provided Name", 10.00m))
			.Returns(true);

		mock.Setup(m => m.MyCalculatorMethod(It.IsAny<string>(), It.IsAny<decimal>()))
			.Returns(true);

		// Act
		_ = mock.Object.MyCalculatorMethod("Provided Name", 10.00m);

		// Assert	- This will fail, as only the second setup is checked.
		Assert.Throws<MockException>(() => mock.VerifyAll());
	}

	[Test]
	public void MoqDemo_ThrowException_Snippet12()
	{
		// Arrange
		var mock = new Mock<IDateTimeProvider>();
		mock.Setup(m => m.DayOfWeek())
			.Throws(new Exception());

		// Act
		var result = ExampleClass.MethodWhichMakesUseOfMock(mock.Object);

		// Assert
		Assert.That(result, Is.EqualTo(true));
		mock.VerifyAll();
	}

	[Test]
	public void MoqDemo_ProtectedMocks_Snippet13()
	{
		// Arrange
		var mock = new Mock<InheritingExample>();
		mock.Protected()
			.Setup<bool>("MyProtectedMethod",
						 "AStringToPassToThisMethod",
						 ItExpr.IsAny<string>())
			.Returns(true);

		// Act
		var result = mock.Object.AMethodWhichRunsTheProtectedMethod();

		// Assert
		Assert.That(result, Is.EqualTo(true));
		mock.VerifyAll();
	}

	[Test]
	public void MoqDemo_Callbacks_Snippet14()
	{
		// Arrange
		var values = new bool[] { false, false, false };
		var mock = new Mock<IExampleInterface>();
		mock.Setup(m => m.DoNewAction())
			.Callback(() => values[0] = true)
			.Returns(true)
			.Callback(() => values[1] = true);

		// Act
		values[2] = mock.Object.DoNewAction();

		// Assert
		Assert.That(values.All(x => x == true), Is.EqualTo(true));
		mock.VerifyAll();
	}

	[Test]
	public void MoqDemo_Sequences_Snippet15()
	{
		// Arrange
		var mock = new Mock<IDateTimeProvider>();
		mock.SetupSequence(m => m.DayOfWeek())
			.Returns(DayOfWeek.Sunday)
			.Returns(DayOfWeek.Tuesday)
			.Returns(DayOfWeek.Saturday);

		// Act
		var result = 
			ExampleClass.ChecksSequenceIsCorrect(mock.Object);

		// Assert
		Assert.That(result, Is.EqualTo(true));
		mock.VerifyAll();
	}
}
