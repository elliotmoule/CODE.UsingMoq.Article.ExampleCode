namespace ExampleProject.Library;
public class RateCalculator
{
	public decimal GetPayRate(decimal baseRate)
	{
		return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ?
			   baseRate * 1.25m :
			   baseRate;
	}

	public decimal GetPayRate(decimal baseRate,
		IDateTimeProvider dateTimeProvider)
	{
		return dateTimeProvider.DayOfWeek() == DayOfWeek.Sunday ?
			   baseRate * 1.25m :
			   baseRate;
	}


}
