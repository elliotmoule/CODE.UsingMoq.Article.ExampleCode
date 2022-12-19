namespace ExampleProject.Library;
public interface IDateTimeProvider
{
	DayOfWeek DayOfWeek();
	string Name { get; set; }
}
