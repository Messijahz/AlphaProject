// --- TA BORT MEDLEM ---
function removeUser(button) {
    const member = button.closest('.modal-member');
    if (member) member.remove();
}

// --- VAL AV MEDLEMMAR ---
let selectedMembers = [];

function handleMemberSelection() {
    document.querySelectorAll('.modal-member').forEach(member => {
        member.addEventListener('click', () => {
            const userId = member.dataset.userId;
            const index = selectedMembers.indexOf(userId);

            if (index > -1) {
                selectedMembers.splice(index, 1);
                member.classList.remove('selected');
            } else {
                selectedMembers.push(userId);
                member.classList.add('selected');
            }

            const container = document.querySelector('.modal-members');
            container.querySelectorAll('input[name="Members"]').forEach(e => e.remove());

            selectedMembers.forEach(id => {
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'Members';
                input.value = id;
                container.appendChild(input);
            });
        });
    });
}

// --- VISA MODAL VID VALIDERINGSFEL ---
function showValidationModal() {
    const error = document.querySelector('.field-validation-error');
    if (!error) return;

    const addModal = document.getElementById('addProjectModal');
    const editModal = document.getElementById('editProjectModal');

    if (addModal) new bootstrap.Modal(addModal).show();
    else if (editModal) new bootstrap.Modal(editModal).show();
}

// --- INITIERA QUILL ---
function initQuill(editorId, hiddenInputId, globalKey) {
    const editor = document.getElementById(editorId);
    const hiddenInput = document.getElementById(hiddenInputId);
    if (!editor || !hiddenInput) return;

    const quill = new Quill(editor, {
        theme: 'snow',
        placeholder: 'Type something',
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline'],
                [{ list: 'ordered' }, { list: 'bullet' }],
                [{ align: [] }],
                ['link']
            ]
        }
    });

    quill.root.innerHTML = hiddenInput.value;

    quill.on('text-change', () => {
        hiddenInput.value = quill.root.innerHTML;
    });

    window[globalKey] = quill;
}

// --- INITIERA FLATPICKR ---
function initFlatpickr() {
    flatpickr(".datepicker", {
        dateFormat: "Y-m-d",
        allowInput: true,
        disableMobile: true
    });

    document.querySelectorAll(".modal-calendar-icon").forEach(icon => {
        icon.addEventListener("click", () => icon.previousElementSibling._flatpickr.open());
    });
}

// --- LADDA IN EDIT-PROJEKT MODAL ---
function loadEditProjectModal(projectId) {
    fetch(`/projects/edit/${projectId}`)
        .then(res => res.text())
        .then(html => {
            const modal = document.getElementById('editProjectModal');
            const content = modal.querySelector('.modal-content');

            content.innerHTML = `
                <div class="modal-header">
                    <h3 class="modal-title">Edit Project</h3>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">${html}</div>
            `;

            new bootstrap.Modal(modal).show();

            // Init funktioner efter laddning
            initQuill("editor-edit", "DescriptionEdit", "editQuill");
            initFlatpickr();
            handleMemberSelection();
        });
}

// --- PÅ SIDLADDNING ---
document.addEventListener('DOMContentLoaded', () => {
    initFlatpickr();
    initQuill("editor-add", "DescriptionAdd", "addQuill");
    handleMemberSelection();
    showValidationModal();

    // Koppla hidden quill value innan submit
    const addForm = document.querySelector('#addProjectModal form');
    if (addForm) {
        addForm.addEventListener('submit', () => {
            document.getElementById('DescriptionAdd').value = window.addQuill?.root.innerHTML;
        });
    }

    const editForm = document.querySelector('#editProjectModal form');
    if (editForm) {
        editForm.addEventListener('submit', () => {
            document.getElementById('DescriptionEdit').value = window.editQuill?.root.innerHTML;
        });
    }
});
