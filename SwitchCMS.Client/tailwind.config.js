// tailwind.config.js
module.exports = {
    content: [
        "./**/*.razor",
        "./**/*.cshtml",
        "./**/*.html",
        "./**/*.js"
    ],
    safelist: [
        // common families — keep these narrow for safety
        { pattern: /^bg-(red|green|blue|yellow|gray|indigo|purple|pink)-/ },
        { pattern: /^text-(xs|sm|base|lg|xl|2xl|3xl|4xl)/ },

        // allow arbitrary-value classes (like bg-[#123456], w-[52px], text-[13px])
        { pattern: /\[.*\]/ },

        // allow the "important" prefix classes like !grid which compile to .\!grid
        { pattern: /^!?[a-z0-9-:!\[\]#\/]+$/ }
    ],
    theme: { extend: {} },
    plugins: []
}
