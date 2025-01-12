namespace GOPS.Client.Shared.Enums;

using Ardalis.SmartEnum;

public sealed class ShiftColor : SmartEnum<ShiftColor>
{
	public static readonly ShiftColor Red = new(nameof(Red), 1, "shift-1");
	public static readonly ShiftColor Orange = new(nameof(Orange), 2, "shift-2");
	public static readonly ShiftColor Green = new(nameof(Green), 3, "shift-3");
	public static readonly ShiftColor Emerald = new(nameof(Emerald), 4, "shift-4");
	public static readonly ShiftColor Blue = new(nameof(Blue), 5, "shift-5");
	public static readonly ShiftColor Indigo = new(nameof(Indigo), 6, "shift-6");
	public static readonly ShiftColor Purple = new(nameof(Purple), 7, "shift-7");
	public static readonly ShiftColor Pink = new(nameof(Pink), 8, "shift-8");
	public static readonly ShiftColor Yellow = new(nameof(Yellow), 9, "shift-9");
	public static readonly ShiftColor Gray = new(nameof(Gray), 10, "shift-10");

	public string TailwindName { get; }

	public ShiftColor(string name, int value, string tailwindName) : base(name, value)
	{
		TailwindName = tailwindName;
	}

	public static string GetBackgroundDark(int value)
	{
		return $"bg-{FromValue(value).TailwindName}-dark";
	}

	public override string ToString()
	{
		return $"bg-{TailwindName} border-{TailwindName}-dark";
	}
}