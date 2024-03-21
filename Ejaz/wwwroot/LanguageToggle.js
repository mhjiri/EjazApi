document.addEventListener('DOMContentLoaded', function () {
    const button = document.getElementById('LangBtnId');
    button.addEventListener('click', toggleLanguage);
});


function toggleLanguage() {
    const currentLanguage = document.documentElement.lang || 'en';
    const newLanguage = currentLanguage === 'en' ? 'ar' : 'en';

    // Set the lang attribute for the document
    document.documentElement.lang = newLanguage;

    // Hide all elements with lang attribute
    const elementsToToggle = document.querySelectorAll('[lang]');
    elementsToToggle.forEach(element => {
        element.style.display = 'none';
    });

    // Show only the elements with the selected language and non-empty content
    const elementsToShow = document.querySelectorAll(`[lang="${newLanguage}"]:not(:empty)`);
    elementsToShow.forEach(element => {
        element.style.display = 'block';
    });
}
