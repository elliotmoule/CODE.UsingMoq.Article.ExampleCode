using Moq;

namespace ExampleProject.Library;
public static class ExampleClass
{
	public static IExampleInterface ReturnedFromSomewhereElse()
	{
		var mock = new Mock<IExampleInterface>();
		return mock.Object;
	}

	public static bool MethodWhichMakesUseOfMock(IDateTimeProvider dateTimeProvider)
	{
		try
		{
			dateTimeProvider.DayOfWeek();
		}
		catch
		{
			return true;
		}

		return false;
	}

	public static bool ChecksSequenceIsCorrect(IDateTimeProvider dateTimeProvider)
	{
		var values = new bool[] { false, false, false };
		values[0] = dateTimeProvider.DayOfWeek() == DayOfWeek.Sunday;
		values[1] = dateTimeProvider.DayOfWeek() == DayOfWeek.Tuesday;
		values[2] = dateTimeProvider.DayOfWeek() == DayOfWeek.Saturday;

		return values.All(x => x == true);
	}
}

public class InheritingExample : BaseExample
{
	public bool AMethodWhichRunsTheProtectedMethod()
	{
		return MyProtectedMethod("AStringToPassToThisMethod", string.Empty);
	}

	protected override bool MyProtectedMethod(string data, string otherData)
	{
		return true;
	}
}

public class BaseExample
{
	protected virtual bool MyProtectedMethod(string data, string otherData)
	{
		return false;
	}
}