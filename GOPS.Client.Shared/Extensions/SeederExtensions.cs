namespace GOPS.Client.Shared.Extensions;

using Bogus;

using GOPS.Client.Shared.Enums;
using GOPS.Client.Shared.Models;


public class SeederExtensions
{
	private const int NumberOfPeoples = 50;
	private const int NumberOfGroups = 3;
	private const int NumberOfShifts = 100;
	private const int NumberOfNotes = 100;
	public static readonly List<Note> Notes = [];
	public static readonly List<Group> Groups = [];
	public static readonly List<People> Peoples = [];
	public static readonly List<Shift> Shifts = [];

	public static void Init()
	{
		var startDate = DateTime.Now.StartOfMonth();
		var endDate = startDate.AddMonths(2);

		if (Groups.Count != 0 && Peoples.Count != 0 && Shifts.Count != 0)
			return;

		SeedGroups();
		SeedPeople();
		SeedShifts(startDate, endDate);
		SeedNotes(startDate, endDate);
	}

	private static void SeedGroups()
	{
		var id = 1;

		var generatedGroups = new Faker<Group>()
			.RuleFor(p => p.Id, _ => id++)
			.RuleFor(p => p.Name, f => $"{f.Name.JobArea()} Team")
			.Generate(NumberOfGroups);

		Groups.AddRange(generatedGroups);
	}

	private static void SeedPeople()
	{
		var generatedPeoples = new Faker<People>(locale: "en")
			.RuleFor(p => p.Id, _ => Guid.NewGuid())
			.RuleFor(p => p.GroupId, f => f.PickRandom(Groups).Id)
			.RuleFor(p => p.FirstName, f => f.Name.FirstName())
			.RuleFor(p => p.LastName, f => f.Name.LastName())
			.Generate(NumberOfPeoples);

		Peoples.AddRange(generatedPeoples);

		foreach (var people in Peoples)
		{
			foreach (var group in Groups.Where(group => people.GroupId == group.Id))
			{
				group.Peoples.Add(people);
			}
		}
	}

	private static void SeedShifts(DateTime startDate, DateTime endDate)
	{
		int[] shiftsDuration = [6];
		List<Shift> listOfShifts = [];

		foreach (var distinctShifts in Peoples.Select(people => new Faker<Shift>(locale: "en")
			.RuleFor(p => p.Id, _ => Guid.NewGuid())
			.RuleFor(p => p.PeopleId, (_, o) => o.PeopleId = people.Id)
			.RuleFor(p => p.StartDate, f => f.Date.Between(startDate, endDate.AddDays(1)).Date.AddHours(f.PickRandom(Enumerable.Range(0, 23))))
			.RuleFor(p => p.EndDate, (f, o) => o.StartDate.AddHours(f.PickRandom(shiftsDuration)))
			.RuleFor(p => p.Color, f => f.PickRandom<ShiftColor>(ShiftColor.List).Value)
			.RuleFor(p => p.TotalTime, (_, o) => o.TotalTime = o.EndDate - o.StartDate)
			.Generate(NumberOfShifts))
			.Select(generatedShifts => generatedShifts.DistinctBy(x => x.StartDate.Date)))
		{
			listOfShifts.AddRange(distinctShifts);
		}

		foreach (var shift in listOfShifts)
		{
			if (shift.StartDate.IsDateEqual(shift.EndDate))
			{
				Shifts.Add(shift);
				continue;
			}

			var beforeMidnightShift = new Shift
			{
				Id = Guid.NewGuid(),
				PeopleId = shift.PeopleId,
				StartDate = shift.StartDate,
				EndDate = shift.EndDate.Date,
				TotalTime = shift.EndDate.Date - shift.StartDate,
				Color = shift.Color,
			};

			var totalHours = shift.EndDate - shift.EndDate.Date;

			if (totalHours.TotalHours == 0)
			{
				continue;
			}

			var afterMidnightShift = new Shift
			{
				Id = Guid.NewGuid(),
				PeopleId = shift.PeopleId,
				LinkedShift = beforeMidnightShift.Id,
				StartDate = shift.EndDate.Date,
				EndDate = shift.EndDate,
				TotalTime = totalHours,
				Color = shift.Color,
				IsLinked = true
			};

			beforeMidnightShift.LinkedShift = afterMidnightShift.Id;
			beforeMidnightShift.IsLinked = true;
			Shifts.Add(beforeMidnightShift);
			Shifts.Add(afterMidnightShift);
		}
	}

	private static void SeedNotes(DateTime startDate, DateTime endDate)
	{
		var generateNotes = new Faker<Note>(locale: "en")
			.RuleFor(p => p.Id, _ => Guid.NewGuid())
			.RuleFor(p => p.Date, f => f.Date.Between(startDate, endDate.AddDays(1)))
			.RuleFor(p => p.Text, f => f.Rant.Review())
			.Generate(NumberOfNotes);

		var distinctDayNotes = generateNotes.DistinctBy(x => x.Date.Date);

		Notes.AddRange(distinctDayNotes);
	}
}