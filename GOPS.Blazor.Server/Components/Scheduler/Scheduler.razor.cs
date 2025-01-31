﻿using Microsoft.AspNetCore.Components;

using GOPS.Client.Shared.Enums;
using GOPS.Client.Shared.Models;
using GOPS.Client.Shared.Utils;
using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Services;
using GOPS.Blazor.Server.Components.Internal;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace GOPS.Blazor.Server.Components;
public partial class Scheduler
{
	DateTime? currentDate;
	int columnCount;
	bool groupsExpanded;
	bool fixedHeader;
	bool fixedMember;
	bool viewByPeople;
	bool viewByShift;
	bool showGroups;
	bool showPeopleSchedule;
	bool showNotes;
	SchedulerGroup<Group>? schedulerGroup;
	MudExpansionPanels? expansionPanels;

	readonly object options = new
	{
		animation = 200,
		easing = "cubic-bezier(.17,.67,.83,.67)",
		handle = ".sortable-handle",
		ghostClass = "sortable-ghost",
		touchStartThreshold = 0.5,
		fallbackTolerance = 3
	};

	public string GridTemplateStyle => new CssBuilder()
		.AddClass($"grid-template-columns: auto repeat({columnCount}, minmax(1.5rem, 1fr)) 2rem;", CurrentView is ViewType.DayView)
		.AddClass($"grid-template-columns: auto repeat({columnCount}, minmax(9rem, 1fr)) 1rem;", CurrentView is ViewType.WeekView)
		.AddClass($"grid-template-columns: auto repeat({columnCount},minmax(4rem, 1fr)) 1rem;", CurrentView is ViewType.MonthView)
		.Build();

	public string FixedStyle => new CssBuilder()
		.AddClass("position: sticky!important;top: 0;", FixedHeader)
		.AddClass("position: sticky!important;left: 0;", FixedMember)
		.Build();

	protected override void OnInitialized()
	{
		DefaultMinutesInterval = 30;
		CurrentDate = TimeProvider.System.GetLocalNow().Date;
		viewByPeople = true;
		showGroups = true;
		SetView(ViewType.WeekView);
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

	public int DefaultMinutesInterval { get; private set; }
	public string CurrentDateFormat { get; set; } = string.Empty;
	public string CurrentDateText { get; set; } = string.Empty;
	public ViewType? CurrentView { get; private set; }
	public List<DayCell>? DayCells { get; set; }
	public List<Note>? Notes { get; set; }
	public List<Group>? Groups { get; set; }
	public List<People>? Peoples { get; set; }
	public List<Shift>? Shifts { get; set; }

	public DateTime? CurrentDate
	{
		get { return currentDate; }
		set
		{
			if (!currentDate.HasValue)
			{
				currentDate = value;
				UpdateView();
				return;
			}

			if (CheckDate(value))
				return;

			currentDate = value;
			UpdateView();
			UpdateData();
			StateHasChanged();
		}
	}

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

			if (groupsExpanded)
			{
				expansionPanels?.ExpandAll();
				StateHasChanged();
				return;
			}

			expansionPanels?.CollapseAll();
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

	public bool FixedMember
	{
		get { return fixedMember; }
		set
		{
			fixedMember = value;
			StateHasChanged();
		}
	}

	public bool ViewByPeople
	{
		get { return viewByPeople; }
		set
		{
			viewByPeople = value;
			if (viewByPeople)
			{
				viewByShift = false;
			}
			else
			{
				viewByShift = true;
			}
			StateHasChanged();
		}
	}

	public bool ViewByShift
	{
		get { return viewByShift; }
		set
		{
			viewByShift = value;
			if (viewByShift)
			{
				viewByPeople = false;
			}
			else
			{
				viewByPeople = true;
			}
			StateHasChanged();
		}
	}

	public bool ShowGroups
	{
		get { return showGroups; }
		set
		{
			showGroups = value;
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

	public void SetView(ViewType view)
	{
		if (CurrentView == view)
			return;

		CurrentView = view;
		UpdateView();
		UpdateData();
	}

	public void SetDate(DateTime? date)
	{
		CurrentDate = date;
	}

	public void OffsetDate(int offset)
	{
		var date = offset == 0 ? DateTime.Now : CurrentDate;

		date = CurrentView switch
		{
			ViewType.DayView => date?.AddDays(offset),
			ViewType.WeekView => date?.AddDays(offset * 7),
			_ => date?.AddMonths(offset),
		};

		SetDate(date);
	}

	bool CheckDate(DateTime? date)
	{
		return CurrentView switch
		{
			ViewType.DayView => date!.Value.IsDateEqual(currentDate!.Value),
			ViewType.WeekView => date!.Value.GetWeekNumber() == currentDate!.Value.GetWeekNumber(),
			ViewType.MonthView => currentDate!.Value.Month == date!.Value.Month && currentDate!.Value.Year == date.Value.Year,
			_ => false
		};
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
				//Get the total minutes of 24h and then divide for the desired DefaultMinutesInterval to get the total column count of the grid;
				columnCount = 1440 / DefaultMinutesInterval;
				CurrentDateFormat = "ddd, dd MMMM yyyy";
				CurrentDateText = CurrentDate?.ToString("ddd, dd MMMM yyyy").Capitalize()!;
				break;
			case ViewType.WeekView:
				columnCount = 7;
				DayCells = CurrentDate?.FromFirstDayOfWeek().CreateDayCells(columnCount).ToList();
				var week = CurrentDate!.Value.GetWeekNumber();
				CurrentDateFormat = $"Week {week} - dd MMMM yyyy";
				CurrentDateText = CurrentDate?.ToString("dd MMMM yyyy").Capitalize()!;
				break;
			case ViewType.MonthView:
				columnCount = CurrentDate!.Value.EndOfMonth().Day;
				DayCells = CurrentDate?.StartOfMonth().CreateDayCells(columnCount).ToList();
				CurrentDateFormat = "MMMM yyyy";
				CurrentDateText = CurrentDate?.ToString("MMMM yyyy").Capitalize()!;
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
		Notes = NotesService.Get(CurrentDate!.Value).ToList();
		Groups = GroupsService.Get().ToList();
		Peoples = PeopleService.Get().ToList();
		Shifts = ShiftService.GetAllOfMonth(CurrentDate!.Value).ToList();
	}

	public IEnumerable<Shift> GetShiftsBetweenDates(Guid? peopleId = null)
	{
		List<Shift> foundShifts = [];

		if (CurrentView is ViewType.DayView)
		{
			if (peopleId is not null)
			{
				foundShifts = Shifts?
					.Where(x => x.PeopleId == peopleId && x.StartDate.IsDateEqual(CurrentDate!.Value))
					.ToList() ?? [];

				return foundShifts;
			}

			foundShifts = Shifts?
				.Where(x => x.StartDate.IsDateEqual(CurrentDate!.Value))
				.ToList() ?? [];

			return foundShifts;
		}

		var start = DayCells?[0].Date ?? new();
		var end = DayCells?[^1].Date ?? new();

		if (peopleId is not null)
		{
			foundShifts = Shifts?
				.Where(x => x.PeopleId == peopleId && x.StartDate.IsBetweenDates(start, end))
				.ToList() ?? [];

			return foundShifts;
		}

		foundShifts = Shifts?
			.Where(x => x.StartDate.IsBetweenDates(start, end))
			.ToList() ?? [];

		return foundShifts;
	}

	public IEnumerable<Shift> GetShiftsByDate(DateTime date)
	{
		var foundShifts = Shifts?.Where(x => x.StartDate.IsDateEqual(date)) ?? [];
		return foundShifts;
	}

	public int GetTotalHours()
	{
		var totalHours = GetShiftsBetweenDates().Sum(x => (int)x.TotalTime.TotalHours);
		return totalHours;
	}

	public int GetTotalHoursPerDay(DateTime date)
		=> GetShiftsByDate(date).Sum(x => (int)x.TotalTime.TotalHours);

	public int GetTotalHoursPerPeople(Guid peopleId)
	{
		var totalHours = GetShiftsBetweenDates(peopleId).Sum(x => (int)x.TotalTime.TotalHours);
		return totalHours;
	}

	public int GetTotalHoursPerGroup(int groupId)
	{
		var totalGroupHours =
			Groups?.SingleOrDefault(g => g.Id == groupId)?.Peoples
				.Select(p => GetTotalHoursPerPeople(p.Id))
				.Sum() ?? 0;

		return totalGroupHours;
	}

	public int GetTotalPeoplesSchedule(DateTime? date = null, int hour = 0)
	{
		int count = 0;

		if (CurrentView is ViewType.DayView)
		{
			if (hour < 0 || hour > 23)
				return count;

			var dateToCheck = CurrentDate + TimeSpan.FromHours(hour);

			count = GetShiftsByDate(CurrentDate!.Value)
				.Count(s => dateToCheck!.Value.IsBetween(s.StartDate, s.EndDate.AddHours(-1)));

			return count;
		}

		count = Shifts?.Count(s => s.StartDate.IsDateEqual(date!.Value)) ?? 0;
		return count;
	}

	public string GetGridTemplateStyle() => GridTemplateStyle;

	public void StateChange() => StateHasChanged();
}
