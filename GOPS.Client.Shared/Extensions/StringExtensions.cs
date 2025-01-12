using System.Globalization;
using System.Text;

namespace GOPS.Client.Shared.Extensions;
public static class StringExtensions
{
	/// <summary>
	///     A string extension method that converts the @this to a title case.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <returns>@this as a string.</returns>
	public static string Capitalize(this string @this) =>
		CultureInfo.CurrentCulture.TextInfo.ToTitleCase(@this);

	/// <summary>
	///     A string extension method that converts the @this to a title case.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="culture">Information describing the culture.</param>
	/// <returns>@this as a string.</returns>
	public static string Capitalize(this string @this, string culture) =>
		new CultureInfo(culture).TextInfo.ToTitleCase(@this);

	/// <summary>
	///     A string extension method that converts the @this to a title case.
	/// </summary>
	/// <param name="this">The @this to act on.</param>
	/// <param name="cultureInfo">Information describing the culture.</param>
	/// <returns>@this as a string.</returns>
	public static string Capitalize(this string @this, CultureInfo cultureInfo) =>
		cultureInfo.TextInfo.ToTitleCase(@this);

	public static string ExtractInitials(this string name)
	{
		// StringSplitOptions.RemoveEmptyEntries excludes empty spaces returned by the Split method

		var nameSplit = name.Split([",", " "] , StringSplitOptions.RemoveEmptyEntries);

		StringBuilder initials = new();

		foreach (string item in nameSplit)
		{
			initials.Append(item[..1].ToUpper());
		}

		return initials.ToString();
	}
}
