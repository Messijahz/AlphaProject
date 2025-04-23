// ---------------------- DARK MODE ----------------------
document.addEventListener("DOMContentLoaded", function () {
    const darkModeCheckbox = document.getElementById("dark-mode-checkbox");
    const htmlElement = document.documentElement;

    if (localStorage.getItem("theme") === "dark") {
        htmlElement.setAttribute("data-theme", "dark");
        if (darkModeCheckbox) darkModeCheckbox.checked = true;
    }

    if (darkModeCheckbox) {
        darkModeCheckbox.addEventListener("change", function () {
            if (this.checked) {
                htmlElement.setAttribute("data-theme", "dark");
                localStorage.setItem("theme", "dark");
            } else {
                htmlElement.setAttribute("data-theme", "light");
                localStorage.setItem("theme", "light");
            }
        });
    }
});