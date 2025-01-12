using System.ComponentModel;

namespace GOPS.Client.Shared.Extensions;
public static class EnumExtensions
{
	public static string GetEnumName<T>(this Enum @this) =>
		Enum.GetName(typeof(T), @this) ?? throw new InvalidOperationException();

	public static string GetEnumDescription(this Enum @this)
	{
		var field = @this.GetType().GetField(@this.ToString());
		if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
		{
			return attribute.Description;
		}
		throw new ArgumentException("Item not found.", nameof(@this));
	}

	public static T GetEnumValueByDescription<T>(this string @this) where T : Enum
	{
		foreach (Enum enumItem in Enum.GetValues(typeof(T)))
		{
			if (enumItem.GetEnumDescription() == @this)
			{
				return (T)enumItem;
			}
		}
		throw new ArgumentException("Not found.", nameof(@this));
	}
}