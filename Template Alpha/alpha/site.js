// ---------------------- DROPDOWN ----------------------
function toggleDropdown(element) {
    let dropdown = element.nextElementSibling;
    let container = element.closest('.custom-dropdown-container');

    document.querySelectorAll('.custom-dropdown-menu').forEach(menu => {
        if (menu !== dropdown) {
            menu.style.display = 'none';
            if (menu.previousElementSibling) {
                menu.previousElementSibling.classList.remove('custom-dropdown-active');
            }
        }
    });

    let isVisible = dropdown.style.display === 'block';
    dropdown.style.display = isVisible ? 'none' : 'block';
    element.classList.toggle('custom-dropdown-active', !isVisible);

    if (container.closest('.wrapper-project-card')) {
        document.querySelectorAll('.wrapper-project-card').forEach(card => {
            if (card !== container.closest('.wrapper-project-card')) {
                card.classList.remove('custom-project-active');
            }
        });

        container.closest('.wrapper-project-card').classList.toggle('custom-project-active', !isVisible);
    }
}

document.addEventListener('click', function (event) {
    if (!event.target.closest('.custom-dropdown-container')) {
        document.querySelectorAll('.custom-dropdown-menu').forEach(menu => {
            menu.style.display = 'none';
            if (menu.previousElementSibling) {
                menu.previousElementSibling.classList.remove('custom-dropdown-active');
            }
        });

        document.querySelectorAll('.wrapper-project-card').forEach(card => {
            card.classList.remove('custom-project-active');
        });
    }
});

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

// ---------------------- QUILL ----------------------
document.addEventListener("DOMContentLoaded", function () {
    var quill = new Quill('#editor', {
        theme: 'snow',
        placeholder: 'Type something',
        modules: {
            toolbar: {
                container: [
                    ['bold', 'italic', 'underline'],
                    [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                    [{ 'align': [] }],
                    ['link']
                ],
                handlers: {
                    
                    bottom: true
                }
            }
        }
    });
});



// ---------------------- AJAX ----------------------
$('#submit-button').click(function () {
    var descriptionContent = $('#description').summernote('code');

    $.ajax({
        url: 'submit.php',
        type: 'POST',
        data: { description: descriptionContent },
        success: function (response) {
            alert('Project saved successfully!');
        },
        error: function () {
            alert('Error saving project.');
        }
    });
});

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

// ------------------- FORM VALIDATION -------------------
const validateField = (field) => {
    let errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`)
    if (!errorSpan) return;

    let errorMessage = ""
    let value = field.value.trim()

    if (field.hasAttribute("data-val-required") && value === "")
        errorMessage = field.getAttribute("data-val-required")

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let pattern = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!pattern.test(value))
            errorMessage = field.getAttribute("data-val-regex")
    }

    if (errorMessage) {
        field.classlist.add("input-validation-error")
        errorSpan.classList.remove("field-validation-valid")
        errorSpan.classList.add("field-validation-error")
        errorSpan.textContent = errorMessage
    } else {
        field.classlist.remove("input-validation-error")
        errorSpan.classList.remove("field-validation-error")
        errorSpan.classList.add("field-validation-valid")
        errorSpan.textContent = ""
    }

}

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form")

    if (!form) return;

    const fields = form.querySelectorAll("input[data-val='true']")

    fields.forEach(field => {
        field.addEventListener("input", function () {
            validateField(field)
        })
    })
})