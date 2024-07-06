//Regex Expressions
const numericsRegex = /[0-9]+$/gm;
const properCaseRegex = /^[A-Z][a-z]*(\s[A-Z][a-z]*)*$/;
const titleCaseRegex = /^[A-Z0-9][A-Za-z0-9]*(\s[A-Z0-9][A-Za-z0-9]*)*$/; //Incl. Numbers
const sentenceCaseRegex = /^[A-Z][a-zA-Z0-9',. -:;?"]*(\s[a-zA-Z0-9',. -:;?"]+)*$/;
const youtubeRegex = /(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})/;


//Dropdown Function

// document.addEventListener('click', e => {
//     const isDropdown = e.target.matches("[data-dropdown-button]");
//     if (!isDropdown && e.target.closest("[data-dropdown-button]") != null) return;   
    
//     let currentDropdown;
//     if (isDropdown) {
//         currentDropdown = e.target.closest("[data-dropdown]");
//         currentDropdown.classList.toggle("active");
//     }

//     document.querySelectorAll("[data-dropdown].active").forEach(dropdown => {
//         if (dropdown === currentDropdown) return;
//         dropdown.classList.remove("active");
//     })
// })