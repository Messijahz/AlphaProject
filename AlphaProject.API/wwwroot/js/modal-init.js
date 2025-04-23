const addProjectModal = document.getElementById('addProjectModal');

addProjectModal.addEventListener('shown.bs.modal', function () {
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

    if (!window.quill) {
        window.quill = new Quill('#editor', {
            theme: 'snow',
            placeholder: 'Type something',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'align': [] }],
                    ['link']
                ]
            }
        });

        window.quill.on('text-change', function () {
            document.querySelector('input[name="Description"]').value = window.quill.root.innerHTML;
        });
    }
});
