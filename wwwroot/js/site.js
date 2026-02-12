document.addEventListener("DOMContentLoaded", function () {
    const themeToggle = document.getElementById("themeToggle");
    const themeIcon = document.getElementById("themeIcon");
    const html = document.documentElement;

    if (!themeToggle || !themeIcon) {
        console.error("No se encontró el botón de cambio de tema.");
        return;
    }

    let theme = localStorage.getItem("theme") || "light";
    html.setAttribute("data-bs-theme", theme);
    themeIcon.className = theme === "dark" ? "fas fa-sun" : "fas fa-moon";

    themeToggle.addEventListener("click", function () {
        let newTheme = html.getAttribute("data-bs-theme") === "dark" ? "light" : "dark";
        html.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme);
        themeIcon.className = newTheme === "dark" ? "fas fa-sun" : "fas fa-moon";
    });
});
