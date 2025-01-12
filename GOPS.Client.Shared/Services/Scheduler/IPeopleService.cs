namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Models;

public interface IPeopleService
{
	public IEnumerable<People> Get();
	public void Add(People people);
	public void Update(People people);
	public void Delete(Guid id);
}
