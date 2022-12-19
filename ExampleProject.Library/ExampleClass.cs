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