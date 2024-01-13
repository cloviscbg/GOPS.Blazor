/** @type {import('tailwindcss').Config} */

import colors from 'tailwindcss/colors'
import defaultTheme from 'tailwindcss/defaultTheme'

module.exports = {
	content: ["**/*.razor", "**/*.cshtml", "**/*.html"],

	darkMode: 'class',

	theme: {
		screens: {
			'xs': { 'max': '600px' },
			'sm': '600px',
			'max-sm': { 'max': '960px' },
			'md': '960px',
			'max-md': { 'max': '1280px' },
			'lg': '1280px',
			'max-lg': { 'max': '1920px'},
			'xl': '1920px',
			'max-xl': { 'max': '2560px' },
			'2xl': '2560px',
		},
		extend: {
			fontFamily: {
				sans: ['Inter var', ...defaultTheme.fontFamily.sans],
			},
			height: {
				content: 'calc(100vh - 16px)'
			},
			colors: {
				background: {
					DEFAULT: "#fafafa",
					dark: "#181824"
				},
				foreground: {
					DEFAULT: "#5C5C65",
					accent: "#11181C",
					dark: "#AEB2C4",
					accentdark: "#FFFFFF",
				},
				surface: {
					DEFAULT: '#ffffff',
					dark: '#1F1E2F'
				},
				container: {
					DEFAULT: '#F4F4F5',
					dark: '#11111B'
				},
				divider: {
					DEFAULT: 'rgba(224, 224, 224, 1)',
					dark: '#2A2A37'
				},
				border: {
					DEFAULT: 'rgba(17, 17, 17, 0.09)',
					dark: 'rgba(48, 48, 61, 0.5)'
				},
				hover: {
					DEFAULT: 'rgba(0, 0, 0, 0.058823529411764705)',
					dark: 'rgba(0, 0, 0, 0.358824)'
				},
				disabled: {
					DEFAULT: 'rgba(0, 0, 0, 0.176471)',
					dark: '#393C48'
				},
				label: {
					DEFAULT: '#818CF8',
				},
				default: {
					DEFAULT: '#181824',
					50: '#E3E3E5',
					100: '#BABABD',
					200: '#8C8C92',
					300: '#5D5D66',
					400: '#3B3B45',
					500: '#181824',
					600: '#151520',
					700: '#11111B',
					800: '#0E0E16',
					900: '#08080D'
				},
				primary: {
					DEFAULT: '#4338CA',
					50: '#EEF2FF',
					100: '#E0E7FF',
					200: '#C7D2FE',
					300: '#A5B4FC',
					400: '#818CF8',
					500: '#6366F1',
					600: '#4F46E5',
					700: '#4338CA',
					800: '#3730A3',
					900: '#312E81',
					950: '#1E1B4B',
					hover: 'rgba(99, 102, 241, 0.1)',
				},
				secondary:
				{
					DEFAULT: "#14b8a6",
					hover: 'rgba(20, 184, 166, 0.1)'
				},
				success: {
					DEFAULT: '#17C964',
					50: '#E8FAF0',
					100: '#D1F4E0',
					200: '#A2E9C1',
					300: '#74DFA2',
					400: '#45D483',
					500: '#17C964',
					600: '#12A150',
					700: '#0E793C',
					800: '#095028',
					900: '#052814',
				},
				warning: {
					DEFAULT: '#F97316',
					50: '#FFF7ED',
					100: '#FFEDD5',
					200: '#FED7AA',
					300: '#FDBA74',
					400: '#FB923C',
					500: '#F97316',
					600: '#EA580C',
					700: '#C2410C',
					800: '#9A3412',
					900: '#7C2D12',
					950: '#431407'
				},
				error: {
					DEFAULT: '#EF4444',
					50: '#FEF2F2',
					100: '#FEE2E2',
					200: '#FECACA',
					300: '#FCA5A5',
					400: '#F87171',
					500: '#EF4444',
					600: '#DC2626',
					700: '#B91C1C',
					800: '#991B1B',
					900: '#7F1D1D',
					950: '#450A0A'
				},
				shift: {
					1: {
						DEFAULT: 'rgba(248, 113, 113, 0.2)',
						dark: 'rgba(239, 68, 68, 0.8)'
					},
					2: {
						DEFAULT: 'rgba(251, 146, 60, 0.2)',
						dark: 'rgba(249, 115, 22, 0.8)'
					},
					3: {
						DEFAULT: 'rgba(163, 230, 53, 0.2)',
						dark: 'rgba(132, 204, 22, 0.8)'
					},
					4: {
						DEFAULT: 'rgba(45, 212, 191, 0.2)',
						dark: 'rgba(20, 184, 166, 0.8)'
					},
					5: {
						DEFAULT: 'rgba(56, 189, 248, 0.2)',
						dark: 'rgba(14, 165, 233, 0.8)'
					},
					6: {
						DEFAULT: 'rgba(129, 140, 248, 0.2)',
						dark: 'rgba(99, 102, 241, 0.8)'
					},
					7: {
						DEFAULT: 'rgba(192, 132, 252, 0.2)',
						dark: 'rgba(168, 85, 247, 0.8)'
					},
					8: {
						DEFAULT: 'rgba(244, 114, 182, 0.2)',
						dark: 'rgba(236, 72, 153, 0.8)'
					},
					9: {
						DEFAULT: 'rgba(253, 224, 71, 0.2)',
						dark: 'rgba(250, 204, 21, 0.8)'
					},
					10: {
						DEFAULT: 'rgba(203, 213, 225, 0.2)',
						dark: 'rgba(148, 163, 184, 0.8)'
					}
				}
			}
		},
	},

	safelist: [
		'bg-shift-1', 'bg-shift-1-dark', 'border-shift-1-dark', 'border-l-shift-1-dark', 'border-r-shift-1', 'border-y-shift-1', 'dark:!border-shift-1-dark',
		'bg-shift-2', 'bg-shift-2-dark', 'border-shift-2-dark', 'border-l-shift-2-dark', 'border-r-shift-2', 'border-y-shift-2', 'dark:!border-shift-2-dark',
		'bg-shift-3', 'bg-shift-3-dark', 'border-shift-3-dark', 'border-l-shift-3-dark', 'border-r-shift-3', 'border-y-shift-3', 'dark:!border-shift-3-dark',
		'bg-shift-4', 'bg-shift-4-dark', 'border-shift-4-dark', 'border-l-shift-4-dark', 'border-r-shift-4', 'border-y-shift-4', 'dark:!border-shift-4-dark',
		'bg-shift-5', 'bg-shift-5-dark', 'border-shift-5-dark', 'border-l-shift-5-dark', 'border-r-shift-5', 'border-y-shift-5', 'dark:!border-shift-5-dark',
		'bg-shift-6', 'bg-shift-6-dark', 'border-shift-6-dark', 'border-l-shift-6-dark', 'border-r-shift-6', 'border-y-shift-6', 'dark:!border-shift-6-dark',
		'bg-shift-7', 'bg-shift-7-dark', 'border-shift-7-dark', 'border-l-shift-7-dark', 'border-r-shift-7', 'border-y-shift-7', 'dark:!border-shift-7-dark',
		'bg-shift-8', 'bg-shift-8-dark', 'border-shift-8-dark', 'border-l-shift-8-dark', 'border-r-shift-8', 'border-y-shift-8', 'dark:!border-shift-8-dark',
		'bg-shift-9', 'bg-shift-9-dark', 'border-shift-9-dark', 'border-l-shift-9-dark', 'border-r-shift-9', 'border-y-shift-9', 'dark:!border-shift-9-dark',
		'bg-shift-10', 'bg-shift-10-dark', 'border-shift-10-dark', 'border-l-shift-10-dark', 'border-r-shift-10', 'border-y-shift-10', 'dark:!border-shift-10-dark'

	],

	plugins: [],
}