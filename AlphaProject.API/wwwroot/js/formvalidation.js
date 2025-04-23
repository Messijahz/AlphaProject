// ------------------- FORM VALIDATION -------------------
const validateField = (field) => {
    const errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`)
    if (!errorSpan) return

    let errorMessage = ""
    const value = field.value.trim()

    // Required validation
    if (field.hasAttribute("data-val-required") && value === "") {
        errorMessage = field.getAttribute("data-val-required")
        field.dataset.errorType = "required"
    }

    // Regex validation (only if not empty and no required error)
    else if (field.hasAttribute("data-val-regex") && value !== "") {
        const pattern = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!pattern.test(value)) {
            errorMessage = field.getAttribute("data-val-regex")
            field.dataset.errorType = "regex"
        }
    }

    // Show or clear error
    if (errorMessage !== "") {
        field.classList.add("input-validation-error")
        errorSpan.classList.remove("field-validation-valid")
        errorSpan.classList.add("field-validation-error")
        errorSpan.textContent = errorMessage
    } else {
        field.classList.remove("input-validation-error")
        errorSpan.classList.remove("field-validation-error")
        errorSpan.classList.add("field-validation-valid")
        errorSpan.textContent = ""
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form")
    if (!form) return

    const fields = form.querySelectorAll("input[data-val='true']")

    // Validate on input
    fields.forEach(field => {
        field.addEventListener("input", () => validateField(field))
    })
})