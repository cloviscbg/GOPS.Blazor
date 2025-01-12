namespace GOPS.Client.Shared.Extensions;

using GOPS.Client.Shared.Models;

public static class DataExtensions
{
	public static IEnumerable<DayCell> CreateDayCells(this DateTime @this, int days)
	{
		List<DayCell> dates = [];
		for (var i = 0; i < days; i++)
		{
			dates.Add(new()
			{
				Date = @this.AddDays(i)
			});
		}

		return dates;
	}


	public static bool IsShiftInRange(this Shift @this, DateTime currentDate)
	{
		return @this.StartDate.Date >= currentDate.Date && @this.EndDate.Date >= currentDate.Date;
	}

	//public static List<Shift> GetShiftsBetweenDates(this List<Shift> shifts, Guid peopleId, DateTime start, DateTime end, ViewType currentView)
	//{
	//	List<Shift> foundShifts = [];

	//	if (currentView is ViewType.DayView)
	//	{
	//		return foundShifts;
	//	}

	//	foundShifts = shifts
	//		.Where(x => x.PeopleId == peopleId && x.StartDate.IsBetweenDates(start, end))
	//		.ToList() ?? [];

	//	return foundShifts;
	//}

	//public static List<Shift> GetShift(this List<Shift> shifts,int day, Guid peopleId)
	//{
	//	var foundShifts = shifts
	//		.Where(x => x.PeopleId == peopleId && x.StartDate.Day == day)
	//		.ToList();

	//	return foundShifts ?? [];
	//}

	//int GetTotalHours(Guid peopleId)
	//{
	//	var totalHours = GetShifts(peopleId).Sum(x => (int)x.TotalTime.TotalHours);
	//	return totalHours;
	//}
}
