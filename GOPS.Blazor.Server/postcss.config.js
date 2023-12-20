module.exports = ({ env }) => ({
	plugins: {
		'tailwindcss/nesting': 'postcss-nesting',
		tailwindcss: {},
		autoprefixer: {},
		cssnano: env === "productions" ? { preset: "default" } : false
	}
})