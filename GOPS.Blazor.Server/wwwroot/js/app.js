
window.theme = {
	toggleDarkMode: function () {
		const userPreferenceStr = localStorage.getItem('userPreference');
		const userPreference = userPreferenceStr ? JSON.parse(userPreferenceStr) : {};

		if (userPreference.IsDarkMode) {
			document.querySelector('#app')?.classList.remove('dark')
			userPreference.IsDarkMode = false;
		}
		else {
			document.querySelector('#app')?.classList.add('dark')
			userPreference.IsDarkMode = true;
		}

		localStorage.setItem('userPreference', JSON.stringify(userPreference))
	}
}