namespace GOPS.Client.Shared;

using Blazored.LocalStorage;

using GOPS.Client.Shared.Interfaces;
using GOPS.Client.Shared.Services;
using GOPS.Client.Shared.Services.App;

using Microsoft.Extensions.DependencyInjection;

public static class Init
{
	public static IServiceCollection AddAppServices(this IServiceCollection services)
	{
		services.AddBlazoredLocalStorage()
				.AddScoped<IPreferenceService, PreferenceService>()
				.AddTransient<IGroupsService, GroupsService>()
				.AddTransient<IPeopleService, PeopleService>()
				.AddTransient<IShiftService, ShiftService>()
				.AddTransient<INotesService, NotesService>()
				.AutoRegisterInterfaces<IAppService>();

		return services;
	}

	public static IServiceCollection AutoRegisterInterfaces<T>(this IServiceCollection services)
	{
		var @interface = typeof(T);

		var types = @interface
			.Assembly
			.GetExportedTypes()
			.Where(t => t.IsClass && !t.IsAbstract)
			.Select(t => new
			{
				Service = t.GetInterface($"I{t.Name}"),
				Implementation = t
			})
			.Where(t => t.Service != null);

		foreach (var type in types)
		{
			if (@interface.IsAssignableFrom(type.Service))
			{
				services.AddTransient(type.Service, type.Implementation);
			}
		}

		return services;
	}
}
