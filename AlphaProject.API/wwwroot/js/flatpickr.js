// ---------------------- FLATPICKR ----------------------
document.addEventListener("DOMContentLoaded", function () {
    flatpickr(".datepicker", {
        dateFormat: "F d, Y",
        allowInput: true
    });

    document.querySelectorAll(".modal-calendar-icon").forEach(icon => {
        icon.addEventListener("click", function () {
            let inputField = this.previousElementSibling;
            if (inputField.classList.contains("datepicker")) {
                inputField._flatpickr.open();
            }
        });
    });
});