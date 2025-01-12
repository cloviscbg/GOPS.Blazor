namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Models;


public class GroupsService : IGroupsService
{
	public void Add(string name)
	{
		SeederExtensions.Groups.Add(new Group
		{
			Id = SeederExtensions.Groups[^1].Id + 1,
			Name = name,
			Peoples = []
		});
	}

	public void Delete(int id)
	{
		var existentGroup = GetById(id);

		if (existentGroup is null)
		{
			return;
		}

		SeederExtensions.Peoples
			.Where(g => g.GroupId == existentGroup.Id)
			.ToList()
			.ForEach(p => { p.GroupId = -1; });

		SeederExtensions.Groups.Remove(existentGroup);
	}

	public IEnumerable<Group> Get() => SeederExtensions.Groups;

	public Group? GetById(int id) => SeederExtensions.Groups.SingleOrDefault(x => x.Id == id);

	public IEnumerable<Group> GetByName(string name) => SeederExtensions.Groups.Where(x => x.Name == name);

	public void Update(Group group)
	{
		var existentGroup = GetById(group.Id);

		if (existentGroup is null)
		{
			return;
		}

		existentGroup.Name = group.Name;
		existentGroup.Peoples = group.Peoples;
	}
}
