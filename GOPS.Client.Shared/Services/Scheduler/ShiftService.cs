namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Models;

public class ShiftService : IShiftService
{
	public IEnumerable<Shift> Get() => SeederExtensions.Shifts;

	public IEnumerable<Shift> GetAllOfMonth(DateTime date)
		=> SeederExtensions.Shifts
			.Where(s => ((s.StartDate.Month == date.Month && s.StartDate.Year == date.Year)) || (s.EndDate.Month == date.Month && s.EndDate.Year == date.Year))
			.OrderBy(x => x.PeopleId)
			.ThenBy(x => x.StartDate);

	public IEnumerable<Shift> GetAllBetweenDates(DateTime start, DateTime end, Guid peopleId)
	{
		throw new NotImplementedException();
	}

	public Shift GetByDate(DateTime date, Guid peopleId)
	{
		throw new NotImplementedException();
	}
}
