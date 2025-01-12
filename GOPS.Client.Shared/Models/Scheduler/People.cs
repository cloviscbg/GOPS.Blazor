namespace GOPS.Client.Shared.Models;
public class People
{
	public Guid Id { get; set; }
	public int GroupId { get; set; } = new();
	public string? FirstName { get; set; } = string.Empty;
	public string? LastName { get; set; } = string.Empty;
	public string? ImageProfile { get; set; }

	public override bool Equals(object? o)
	{
		var other = o as People;
		return other?.Id == Id;
	}

	public override int GetHashCode() => Id.GetHashCode();

	public override string ToString() => $"{FirstName} {LastName}";
}
