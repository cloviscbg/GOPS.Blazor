namespace GOPS.Blazor.Server.Components;

using Microsoft.AspNetCore.Components;

using GOPS.Client.Shared.Enums;
using GOPS.Client.Shared.Models;
using GOPS.Client.Shared.Utils;
using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Services;
using GOPS.Blazor.Server.Components.Internal;

public partial class Scheduler
{
	int columnCount;
	bool groupsExpanded;
	bool fixedHeader;
	bool showPeopleSchedule;
	bool showNotes;
	SchedulerGroup<Group>? schedulerGroup;

	string GridTemplateStyle => new CssBuilder()
		.AddClass($"grid-template-columns: 9rem repeat({columnCount}, minmax(1.5rem, 1fr)) 2rem;", CurrentView is ViewType.DayView)
		.AddClass($"grid-template-columns: minmax(7rem, 12rem) repeat({columnCount}, minmax(9rem, 1fr)) 2rem;", CurrentView is ViewType.WeekView)
		.AddClass($"grid-template-columns: 8rem repeat({columnCount},minmax(4rem, 1fr)) 2rem;", CurrentView is ViewType.MonthView)
		.Build();

	string FixedHeaderStyle => new CssBuilder()
		.AddClass("position: sticky!important;top: 0;", FixedHeader)
		.Build();

	protected override void OnInitialized()
	{
		DefaultMinutesInterval = 30;
		CurrentDate = TimeProvider.System.GetLocalNow().Date;
		SetView(ViewType.DayView);
		UpdateData();
		base.OnInitialized();
	}

	[Inject]
	public INotesService NotesService { get; set; } = default!;

	[Inject]
	public IGroupsService GroupsService { get; set; } = default!;

	[Inject]
	public IPeopleService PeopleService { get; set; } = default!;

	[Inject]
	public IShiftService ShiftService { get; set; } = default!;

	public DateTime CurrentDate { get; private set; }
	public int DefaultMinutesInterval { get; private set; }
	public string CurrentDateText { get; private set; } = "";
	public ViewType? CurrentView { get; private set; }
	public List<DayCell>? DayCells { get; set; }
	public List<Note>? Notes { get; set; }
	public List<Group>? Groups { get; set; }
	public List<People>? Peoples { get; set; }
	public List<Shift>? Shifts { get; set; }

	public int ColumnCount
	{
		get { return columnCount; }
		set { columnCount = value; }
	}

	public bool GroupsExpanded
	{
		get { return groupsExpanded; }
		set
		{
			groupsExpanded = value;
			schedulerGroup?.ExpandGroups(groupsExpanded);
			StateHasChanged();
		}
	}

	public bool ShowPeopleScheduled
	{
		get { return showPeopleSchedule; }
		set
		{
			showPeopleSchedule = value;
			StateHasChanged();
		}
	}
	public bool ShowNotes
	{
		get { return showNotes; }
		set
		{
			showNotes = value;
			StateHasChanged();
		}
	}

	public bool FixedHeader
	{
		get { return fixedHeader; }
		set
		{
			fixedHeader = value;
			StateHasChanged();
		}
	}


	public void SetView(ViewType view)
	{
		if (CurrentView == view)
			return;

		CurrentView = view;
		UpdateView();
		UpdateData();
	}

	public void SetDate(DateTime date)
	{
		if (date.IsDateEqual(CurrentDate))
			return;

		CurrentDate = date;

		UpdateView();
		UpdateData();
	}

	public void OffsetDate(int offset)
	{
		var date = offset == 0 ? DateTime.Now : CurrentDate;

		date = CurrentView switch
		{
			ViewType.DayView => date.AddDays(offset),
			ViewType.WeekView => date.AddDays(offset * 7),
			_ => date.AddMonths(offset),
		};

		SetDate(date);
	}

	void NoteChange(Note note)
	{
		NotesService.Update(note);
		UpdateData();
	}

	void UpdateView()
	{
		DayCells ??= [];
		DayCells.Clear();

		switch (CurrentView)
		{
			case ViewType.DayView:
				columnCount = new TimeSpan().CreateMinutesInterval(DefaultMinutesInterval).Count;
				CurrentDateText = CurrentDate.ToString("ddd, dd MMM yy").Capitalize();
				break;
			case ViewType.WeekView:
				columnCount = 7;
				DayCells = CurrentDate.FromFirstDayOfWeek().CreateDayCells(columnCount).ToList();
				CurrentDateText = CurrentDate.ToString("dd MMM yyyy").Capitalize();
				break;
			case ViewType.MonthView:
				columnCount = CurrentDate.EndOfMonth().Day;
				DayCells = CurrentDate.StartOfMonth().CreateDayCells(columnCount).ToList();
				CurrentDateText = CurrentDate.ToString("MMMM yyyy").Capitalize();
				break;
			default:
				break;
		}
	}

	void UpdateData()
	{
		Notes?.Clear();
		Groups?.Clear();
		Peoples?.Clear();
		Shifts?.Clear();
		Notes = NotesService.Get(CurrentDate).ToList();
		Groups = GroupsService.Get().ToList();
		Peoples = PeopleService.Get().ToList();
		Shifts = ShiftService.GetAllOfMonth(CurrentDate).ToList();
	}

	public void StateChange() => StateHasChanged();

	public IEnumerable<Shift> GetShift(int day, Guid peopleId)
	{
		var shifts = Shifts?
			.Where(x => x.PeopleId == peopleId && x.StartDate.Day == day);

		return shifts ?? Enumerable.Empty<Shift>();
	}
}
