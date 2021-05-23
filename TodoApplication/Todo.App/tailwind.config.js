const colors = require('tailwindcss/colors');
const forms = require("@tailwindcss/forms")({
    strategy: 'class',
});
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
        colors
    },
    variants: {
        extend: {},
    },
    plugins: [
        forms
    ],
}