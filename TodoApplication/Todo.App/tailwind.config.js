const colors = require('tailwindcss/colors');
module.exports = {
    purge: {
        enbaled: true,
        content: [
            './**/*.html',
            './**/*.razor'
        ]
    },
    darkMode: false,
    theme: {
        minHeight: {
            '0': '0',
            '1/4': '25%',
            '1/2': '50%',
            '3/4': '75%',
            'full': '100%',
        }
    },
    variants: {
        extend: {},
    },
    plugins: [],
}