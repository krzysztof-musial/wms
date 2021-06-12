const { guessProductionMode } = require("@ngneat/tailwind");
const colors = require('tailwindcss/colors');

module.exports = {
    prefix: '',
    purge: {
      enabled: guessProductionMode(),
      content: [
        './src/**/*.{html,ts}',
      ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
      },
      extend: {
        colors: {
          wms: {
            blue: '#0266d6',
            // 
            100: '#BFBFC0',
            200: '#7F7F82',
            300: '#3F3F42',
            400: '#2A2A2E',
            500: '#18181A',
            accent: '#1EFFE4'
          },
          blueGray: colors.blueGray,
          coolGray: colors.coolGray,
          // gray: colors.gray,
          trueGray: colors.trueGray,
          warmGray: colors.warmGray,
          // red: colors.red,
          orange: colors.orange,
          amber: colors.amber,
          // yellow: colors.yellow,
          lime: colors.lime,
          // green: colors.green,
          emerald: colors.emerald,
          teal: colors.teal,
          cyan: colors.cyan,
          lightBlue: colors.lightBlue,
          // blue: colors.blue,
          // indigo: colors.indigo,
          violet: colors.violet,
          // purple: colors.purple,
          fuchsia: colors.fuchsia,
          // pink: colors.pink,
          rose: colors.rose
        }
      },
    },
    variants: {
      extend: {},
    },
    plugins: [require('@tailwindcss/forms')],
};
