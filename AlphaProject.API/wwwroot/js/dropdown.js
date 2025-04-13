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