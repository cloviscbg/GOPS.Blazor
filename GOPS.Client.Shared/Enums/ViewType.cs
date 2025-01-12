namespace GOPS.Client.Shared.Enums;

using System.ComponentModel;

public enum ViewType
{
    [Description("Day view")]
    DayView = 0,

    [Description("Week view")]
    WeekView = 1,

    [Description("Month view")]
    MonthView = 2
}