namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Models;


public class PeopleService : IPeopleService
{
	public void Add(People people)
	{
		throw new NotImplementedException();
	}

	public void Delete(Guid id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<People> Get() => SeederExtensions.Peoples.OrderBy(p => p.FirstName);

	public void Update(People people)
	{
		throw new NotImplementedException();
	}
}
