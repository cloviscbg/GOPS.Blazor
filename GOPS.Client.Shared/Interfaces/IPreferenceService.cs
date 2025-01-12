using GOPS.Client.Shared.Models.Preference;

namespace GOPS.Client.Shared.Interfaces;
public interface IPreferenceService : IAppService
{
	Task SetPreference(IPreference preference);

	Task<IPreference?> GetPreference();

}
