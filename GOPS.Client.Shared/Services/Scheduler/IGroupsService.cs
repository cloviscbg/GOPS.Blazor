namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Models;

public interface IGroupsService
{
	public IEnumerable<Group> Get();
	public Group? GetById(int id);
	public IEnumerable<Group> GetByName(string name);
	public void Add(string name);
	public void Update(Group group);
	public void Delete(int id);
}
