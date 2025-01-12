namespace GOPS.Client.Shared.Models;
public class Shift
{
	public Guid Id { get; set; }
	public Guid PeopleId { get; set; }
	public Guid? LinkedShift { get; set; }
	public int Color { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public TimeSpan TotalTime { get; set; }
	public string? CustomLabel { get; set; }
	public string? ShiftNote { get; set; }
    public bool IsLinked { get; set; }

    public Shift()
    {
        TotalTime = StartDate - EndDate;
    }
}
