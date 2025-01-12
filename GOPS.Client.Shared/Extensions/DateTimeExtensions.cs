using System.Globalization;

using Bogus.Bson;

namespace GOPS.Client.Shared.Extensions;
public static class DateTimeExtensions
{
	/// <summary>
	///     A DateTime extension method that query if 'date' is date equal.
	/// </summary>
	/// <param name="date">The date to act on.</param>
	/// <param name="dateToCompare">Date/Time of the date to compare.</param>
	/// <returns>true if date equal, false if not.</returns>
	public static bool IsDateEqual(this DateTime date, DateTime dateToCompare) => date.Date == dateToCompare.Date;


	/// <summary>
	///     A DateTime extension method to determines whether the object is equal to any of the provided values.
	/// </summary>
	/// <param name="this">The object to be compared.</param>
	/// <param name="values">The value list to compare with the object.</param>
	/// <returns>true if the values list contains the object, else false.</returns>
	/// ###
	public static bool In(this DateTime @this, params DateTime[] values) => Array.IndexOf(values, @this) != -1;

	/// <summary>
	///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="minValue">The minimum value.</param>
	/// <param name="maxValue">The maximum value.</param>
	/// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
	/// ###
	/// <typeparam name="T">Generic type parameter.</typeparam>
	/// 
	public static bool IsBetween(this DateTime @this, DateTime minValue, DateTime maxValue)
	{
		return (minValue <= @this) && (@this <= maxValue);
	}

	public static bool IsBet(this DateTime @this, DateTime minValue, DateTime maxValue)
	{
		if (minValue <= @this)
		{
			if (@this <= maxValue)
				return true;
		}
		return false;
	}

	public static bool IsBetweenDates(this DateTime @this, DateTime minValue, DateTime maxValue)
	{
		return (minValue.Date <= @this.Date) && (@this.Date <= maxValue.Date);
	}

	/// <summary>
	///     A DateTime extension method that query if '@this' is in the past.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>true if the value is in the past, false if not.</returns>
	public static bool IsPast(this DateTime @this) => @this < DateTime.Now;

	public static bool IsPastDate(this DateTime @this) => @this.Date < DateTime.Now.Date;

	/// <summary>
	///     A DateTime extension method that query if the only the date part of @this' is in the past.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>true if the value is in the past, false if not.</returns>
	public static bool IsPastOnlyDate(this DateTime @this) => @this.Date < DateTime.Now.Date;

	/// <summary>
	///     A DateTime extension method that query if '@this' is today.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>true if today, false if not.</returns>
	public static bool IsToday(this DateTime @this) => @this.Date == DateTime.Today;

	/// <summary>
	///     A DateTime extension method that query if '@this' is in the future.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>true if the value is in the future, false if not.</returns>
	public static bool IsFuture(this DateTime @this) => @this > DateTime.Now;

	/// <summary>
	///     A DateTime extension method that returns the week number of this date.
	/// </summary>
	/// <param name="this">The date to act on.</param>
	/// <returns>The week number of this date.</returns>
	public static int GetWeekNumber(this DateTime @this)
	{
		var culture = CultureInfo.CurrentCulture;
		return culture.Calendar.GetWeekOfYear(@this, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
	}

	/// <summary>
	///     A DateTime extension method that return the first day of week.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime.</returns>
	public static DateTime FirstDayOfWeek(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-(int)(@this.DayOfWeek));

	/// <summary>
	///     A DateTime extension method that return the first day of week starting from a given DayOfWeek.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="dayOfWeek">The DayOfWeek to start</param>
	/// <returns>A DateTime.</returns>
	public static DateTime FromFirstDayOfWeek(this DateTime @this, DayOfWeek dayOfWeek = DayOfWeek.Monday)
	{
		while (@this.DayOfWeek != dayOfWeek)
			@this = @this.AddDays(-1);

		return @this;
	}

	/// <summary>
	///     A DateTime extension method that return the last day of week.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime.</returns>
	public static DateTime LastDayOfWeek(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, @this.Day).AddDays(6 - (int)@this.DayOfWeek);

	/// <summary>
	///     A DateTime extension method that return the last day of week.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	public static DateTime FromLastDayOfWeek(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, @this.Day).AddDays(7 - (int)@this.DayOfWeek);

	/// <summary>
	///     A DateTime extension method that return a DateTime of the first day of the month with the time set to
	///     "00:00:00:000". The first moment of the first day of the month.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime of the first day of the month with the time set to "00:00:00:000".</returns>
	public static DateTime StartOfMonth(this DateTime @this) => new DateTime(@this.Year, @this.Month, 1);

	/// <summary>
	///     A DateTime extension method that return a DateTime of the last day of the month with the time set to
	///     "23:59:59:999". The last moment of the last day of the month.  Use "DateTime2" column type in sql to keep the
	///     precision.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime of the last day of the month with the time set to "23:59:59:999".</returns>
	public static DateTime EndOfMonth(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));

	/// <summary>
	///     A DateTime extension method that return a DateTime of the first day of the year with the time set to
	///     "00:00:00:000". The first moment of the first day of the year.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime of the first day of the year with the time set to "00:00:00:000".</returns>
	public static DateTime StartOfYear(this DateTime @this) => new DateTime(@this.Year, 1, 1);

	/// <summary>
	///     A DateTime extension method that return a DateTime of the last day of the year with the time set to
	///     "23:59:59:999". The last moment of the last day of the year.  Use "DateTime2" column type in sql to keep the
	///     precision.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>A DateTime of the last day of the year with the time set to "23:59:59:999".</returns>
	public static DateTime EndOfYear(this DateTime @this) =>
		new DateTime(@this.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));

	/// <summary>
	///     A DateTime extension method that converts this object to a day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToDayString(this DateTime @this) =>
		@this.ToString("dddd", DateTimeFormatInfo.CurrentInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">The culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToDayString(this DateTime @this, string culture) =>
		@this.ToString("dddd", new CultureInfo(culture));

	/// <summary>
	///     A DateTime extension method that converts this object to a day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToDayString(this DateTime @this, CultureInfo cultureInfo) =>
		@this.ToString("dddd", cultureInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToMontString(this DateTime @this) =>
		@this.ToString("MMMM", DateTimeFormatInfo.CurrentInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">The culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToMontString(this DateTime @this, string culture) =>
		@this.ToString("MMMM", new CultureInfo(culture));

	/// <summary>
	///     A DateTime extension method that converts this object to a month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToMontString(this DateTime @this, CultureInfo cultureInfo) =>
		@this.ToString("MMMM", cultureInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this) =>
		@this.ToString("ddd", DateTimeFormatInfo.CurrentInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">The culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this, string culture) =>
		@this.ToString("ddd", new CultureInfo(culture));


	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this, CultureInfo cultureInfo) =>
		@this.ToString("ddd", cultureInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="count">Max of characters to subtract from the string.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this, int count) =>
		@this.ToString("ddd", CultureInfo.CurrentUICulture)[..count];

	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <param name="count">Max of characters to subtract from the string.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this, CultureInfo cultureInfo, int count) =>
		@this.ToString("ddd", cultureInfo)[..count];

	/// <summary>
	///     A DateTime extension method that converts this object to a short day string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">The culture.</param>
	/// <param name="count">Max of characters to subtract from the string.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortDayString(this DateTime @this, string culture, int count) =>
		@this.ToString("ddd", new CultureInfo(culture))[..count];

	/// <summary>
	///     A DateTime extension method that converts this object to a short month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortMonthString(this DateTime @this) =>
		@this.ToString("MMM", DateTimeFormatInfo.CurrentInfo);

	/// <summary>
	///     A DateTime extension method that converts this object to a short month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">The culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortMonthString(this DateTime @this, string culture) =>
		@this.ToString("MMM", new CultureInfo(culture));

	/// <summary>
	///     A DateTime extension method that converts this object to a short month string.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <returns>The given data converted to a string.</returns>
	public static string ToShortMonthString(this DateTime @this, CultureInfo cultureInfo) =>
		@this.ToString("MMM", cultureInfo);

	/// <summary>
	///    A DateTime extension that determines difference of time between a given DateTime object.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="endDate">The end DateTime off the difference</param>
	/// <returns>A TimeSpan of the elapsed time</returns>
	public static TimeSpan TimeElapsed(this DateTime @this, DateTime endDate)
	{
		return DateTime.Now - @this;
	}

	/// <summary>
	///     Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
	/// </summary>
	/// <param name="this">The Coordinated Universal Time (UTC).</param>
	/// <param name="destinationTimeZone">The time zone to convert  to.</param>
	/// <returns>
	///     The date and time in the destination time zone. Its  property is  if  is ; otherwise, its  property is .
	/// </returns>
	public static DateTime ConvertTimeFromUtc(this DateTime @this, TimeZoneInfo destinationTimeZone) =>
		TimeZoneInfo.ConvertTimeFromUtc(@this, destinationTimeZone);

	/// <summary>
	///     A Datetime extension method that creates a list of dates from a given start date.
	/// </summary>
	/// <param name="this">The start date to start to.</param>
	/// <param name="days">Maximum of days.</param>
	/// <returns>A list of DateTime</returns>
	public static List<DateTime> CreateListOfDates(this DateTime @this, int days)
	{
		var listDates = new List<DateTime>();
		for (var i = 0; i < days; i++)
		{
			listDates.Add(@this.AddDays(i));
		}

		return listDates;
	}

	public static IEnumerable<DateTime> GetDaysOfWeek(this DateTime @this, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
	{
		var days = startDayOfWeek - @this.DayOfWeek;
		days = days > 0 ? days - 7 : days;
		var startDate = @this.AddDays(days);

		return Enumerable.Range(0, 7).Select(i => startDate.AddDays(i));
	}

	public static IEnumerable<DateTime> GetDaysOfMonth(this DateTime @this)
	{
		var start = new DateTime(@this.Year, @this.Month, 1);
		var end = new DateTime(@this.Year, @this.Month, 1).AddMonths(1).AddDays(-1);
		var listDates = new List<DateTime>();
		for (var i = 0; i < end.Day; i++)
		{
			listDates.Add(start.AddDays(i));
		}

		return listDates;
	}

	public static Dictionary<TimeSpan, int> CreateMinutesInterval(this TimeSpan @this, int minuteInterval)
	{
		//60min is 1h, so multiply to 24h to get the number of minutes in 1day then divide for the desired minuteInterval
		var query = Enumerable
			.Range(0, 60 / minuteInterval * 24)
			.Select(i => new
			{
				Key = @this + TimeSpan.FromMinutes(minuteInterval * i),
				Value = i,
			})
			.ToDictionary(x => x.Key, x => x.Value);

		return query;
	}

	public static IEnumerable<TimeSpan?> CreateMinutes(this TimeSpan? @this, int minuteInterval)
	{
		@this ??= new();
		var t = @this;
		var query = Enumerable
			.Range(0, 60 / minuteInterval * 24)
			.Select(i => @this + TimeSpan.FromMinutes(minuteInterval * i));

		return query;
	}

	public static IEnumerable<TimeSpan> CreateMinutes(this TimeSpan @this, int minuteInterval)
	{
		var t = @this;
		var query = Enumerable
			.Range(0, 60 / minuteInterval * 24)
			.Select(i => @this + TimeSpan.FromMinutes(minuteInterval * i));

		return query;
	}

	public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
	{
		return source.Select(selector).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);
	}
}
