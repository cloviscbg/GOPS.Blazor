namespace GOPS.Client.Shared.Extensions;
public static class RandomExtension
{
	private static readonly Random random = new();


	public static T? PickRandom<T>(this IEnumerable<T> list)
	{
		// If there are no elements in the collection, return the default value of T
		if (!list.Any())
			return default;

		return list.ElementAt(random.Next(list.Count()));
	}
}
