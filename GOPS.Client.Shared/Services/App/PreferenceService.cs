using Blazored.LocalStorage;

using GOPS.Client.Shared.Interfaces;
using GOPS.Client.Shared.Models.Preference;

namespace GOPS.Client.Shared.Services.App;
public class PreferenceService : IPreferenceService
{
	public static string Preference = "userPreference";
	private readonly ILocalStorageService _localStorageService;

	public PreferenceService(ILocalStorageService localStorageService)
	{
		_localStorageService = localStorageService;
	}

	public async Task<bool> ToggleDarkModeAsync()
	{
		if (await GetPreference() is Preference preference)
		{
			preference.IsDarkMode = !preference.IsDarkMode;
			await SetPreference(preference);
			return !preference.IsDarkMode;
		}

		return false;
	}
	public async Task<bool> ToggleDrawerAsync()
	{
		if (await GetPreference() is Preference preference)
		{
			preference.IsDrawerOpen = !preference.IsDrawerOpen;
			await SetPreference(preference);
			return preference.IsDrawerOpen;
		}

		return false;
	}


	public async Task<IPreference?> GetPreference()
	{
		return await _localStorageService.GetItemAsync<Preference>(Preference) ?? new ();
	}

	public async Task SetPreference(IPreference preference)
	{
		await _localStorageService.SetItemAsync(Preference, preference as Preference);
	}
}
