	
let _selectors = undefined;
let _dotnetRef = undefined;

export const clickOutside = {
	register(selectors, dotnetRef) {
		_selectors = selectors;
		_dotnetRef = dotnetRef;

		window.addEventListener("click", this.onClickOutside);
	},

	onClickOutside(event) {
		if (!_selectors || !_dotnetRef) return;
		const clickWasOutside = _selectors.every(x => document.querySelector(x)?.contains(event.target) == false)

		if (clickWasOutside) {
			_dotnetRef.invokeMethodAsync("OnClickOutside")
		}
	},

	dispose() {
		window.removeEventListener('click', this.onClickOutside);
		_selectors = undefined;
		_dotnetRef = undefined;
	},

}