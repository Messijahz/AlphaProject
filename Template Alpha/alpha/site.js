function toggleDropdown(element) {
  let dropdown = element.nextElementSibling;
  let projectCard = element.closest('.wrapper-project-card');


  document.querySelectorAll('.custom-dropdown-menu').forEach(menu => {
      if (menu !== dropdown) {
          menu.style.display = 'none';
          menu.previousElementSibling.classList.remove('custom-dropdown-active');
      }
  });

  document.querySelectorAll('.wrapper-project-card').forEach(card => {
      if (card !== projectCard) {
          card.classList.remove('custom-project-active');
      }
  });


  let isVisible = dropdown.style.display === 'block';
  dropdown.style.display = isVisible ? 'none' : 'block';
  element.classList.toggle('custom-dropdown-active', !isVisible);
  projectCard.classList.toggle('custom-project-active', !isVisible);
}


document.addEventListener('click', function (event) {
  if (!event.target.closest('.custom-dropdown-container')) {
      document.querySelectorAll('.custom-dropdown-menu').forEach(menu => {
          menu.style.display = 'none';
          menu.previousElementSibling.classList.remove('custom-dropdown-active');
      });

      document.querySelectorAll('.wrapper-project-card').forEach(card => {
          card.classList.remove('custom-project-active');
      });
  }
});
