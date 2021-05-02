module.exports = {
    prefix: '',
    purge: {
      content: [
        './src/**/*.{html,ts,css,scss,sass,less,styl}',
      ]
    },
    darkMode: 'class', // or 'media' or 'class'
    theme: {
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
      },
      extend: {
        colors: {
          wms: {
            100: '#BFBFC0',
            200: '#7F7F82',
            300: '#3F3F42',
            400: '#2A2A2E',
            500: '#18181A',
            accent: '#1EFFE4'
          }
        }
      },
    },
    variants: {
      extend: {},
    },
    plugins: [require('@tailwindcss/forms'),require('@tailwindcss/typography')],
};
