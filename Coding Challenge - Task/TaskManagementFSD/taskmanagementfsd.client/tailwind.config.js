/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./src/**/*.{js,jsx,ts,tsx}'], // Adjust the path according to your folder structure
    theme: {
        extend: {
            colors: {
                background: '#f7fafc', // Add your custom colors here
                jasmine: '#f6e05e',
                darkjasmine: '#d69e2e',
                spacecadet: '#2b2d42',
                uorange: '#ff4d00',
                accent: '#edf2f7',
            },
        },
    },
    plugins: [],
};

