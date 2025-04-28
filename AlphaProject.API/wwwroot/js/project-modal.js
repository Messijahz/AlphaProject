// --- TA BORT MEDLEM ---
function removeUser(button) {
    var member = button.closest('.modal-member');
    member.remove();
}

// --- HANTERA KLICK PÅ MEDLEMMAR OCH UPPDATERA DOLD INPUT ---
let selectedMembers = [];

document.querySelectorAll('.modal-member').forEach(member => {
    member.addEventListener('click', function () {
        const userId = this.dataset.userId;
        const index = selectedMembers.indexOf(userId);

        if (index > -1) {
            selectedMembers.splice(index, 1);
            this.classList.remove('selected');
        } else {
            selectedMembers.push(userId);
            this.classList.add('selected');
        }

        // Dynamiskt uppdatera dolda fält:
        const membersInputContainer = document.querySelector('.modal-members');
        membersInputContainer.querySelectorAll('input[name="Members"]').forEach(e => e.remove());

        selectedMembers.forEach(id => {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'Members';
            input.value = id;
            membersInputContainer.appendChild(input);
        });
    });
});

// --- AUTOMATISKT VISA MODAL VID VALIDERINGSFEL ---
document.addEventListener('DOMContentLoaded', function () {
    if (document.querySelector('.field-validation-error')) {
        var myModal = new bootstrap.Modal(document.getElementById('addProjectModal'));
        myModal.show();
    }
});