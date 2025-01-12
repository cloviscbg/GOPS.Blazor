namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Models;

public interface IShiftService
{
	public IEnumerable<Shift> Get();
	public IEnumerable<Shift> GetAllOfMonth(DateTime dateTime);
	public Shift GetByDate(DateTime date, Guid peopleId);
}
