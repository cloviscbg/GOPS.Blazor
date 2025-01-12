namespace GOPS.Client.Shared.Models;
public class Group
{
	public int Id { get; set; }
	public string? Name { get; set; } = "Unnamed Group";
	public List<People> Peoples { get; set; } = [];
}
