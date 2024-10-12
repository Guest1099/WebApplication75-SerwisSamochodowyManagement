

document.addEventListener("DOMContentLoaded", function () {

    var leftNavigation = document.getElementById("leftNavigation");
    var sliderAreaWidth = 1;

    leftNavigation.addEventListener("mouseenter", function () {
        leftNavigation.style.left = "0";
    });

    leftNavigation.addEventListener("mouseleave", function () {
        leftNavigation.style.left = `-${210}px`;
    });

    document.addEventListener("mousemove", function (event) {
        var mouseX = event.clientX;

        if (mouseX >= 0 && mouseX <= sliderAreaWidth) {
            leftNavigation.style.left = "0";
        }
    });



    var navigationLinks = document.querySelectorAll('.leftNavigationUl .leftNavigationA');

    navigationLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            //event.preventDefault();

            // Usuń klasę 'active' ze wszystkich linków
            navigationLinks.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            // Dodaj klasę 'active' do klikniętego linku
            link.classList.add('active');

            // Zapisz informacje o klikniętym linku w localStorage
            localStorage.setItem('activeLink', link.href);
        });

        // Sprawdź, czy istnieją informacje o aktywnym linku w localStorage
        var storedLink = localStorage.getItem('activeLink');
        if (storedLink && link.href === storedLink) {
            link.classList.add('active');
        }
    });



    initializeDropdownMenu();

});



// Rozwijane menu
function initializeDropdownMenu() {
    var dropdowns = document.querySelectorAll('.dropdown');
    dropdowns.forEach(function (dropdown) {
        var dropbtn = dropdown.querySelector('.dropbtn');
        var content = dropdown.querySelector('.dropdown-content');

        dropbtn.addEventListener('click', function () {
            closeAllDropdowns();
            content.classList.toggle('show');
        });

        window.addEventListener('click', function (event) {
            if (!event.target.matches('.dropbtn')) {
                if (content.classList.contains('show')) {
                    content.classList.remove('show');
                }
            }
        });
    });
    function closeAllDropdowns() {
        dropdowns.forEach(function (otherDropdown) {
            var otherContent = otherDropdown.querySelector('.dropdown-content');
            if (otherContent.classList.contains('show')) {
                otherContent.classList.remove('show');
            }
        });
    }
}




function DisabledButton(object, value) {
    if (value == true) {
        object.setAttribute('disabled', 'disabled')
    }
    else {
        object.removeAttribute('disabled')
    }
}





// Opcje płatności
ukryjWszystkieKontenery();
function ukryjWszystkieKontenery() { 
    var containers = document.querySelectorAll('.container-payment-options');
    containers.forEach(function (container) {
        container.style.display = 'none';
    });
}
function showContainer(containerId) { 
    ukryjWszystkieKontenery();
    // Pokaż wybrany kontener
    var selectedContainer = document.getElementById(containerId);
    selectedContainer.style.display = 'block';
} 









// Edytor produktu
function toggleFormat(format) {
    document.execCommand(format, false, null);
}

function changeFontSize() {
    const size = document.querySelector('select').value;
    if (size) {
        document.execCommand('fontSize', false, size);
    }
}

function changeTextColor() {
    const color = document.querySelector('input[type="color"]').value;
    document.execCommand('foreColor', false, color);
}

function alignText(align) {
    document.execCommand('justify' + align.charAt(0).toUpperCase() + align.slice(1), false, null);
}

function toggleList(type) {
    document.execCommand('insert' + (type === 'ordered' ? 'Order' : 'Unordered') + 'List', false, null);
}

// funkcja odpowiedzialna za wstawiania zdjęcia do pola edycyjnego
function insertImages(input) {
    const files = input.files;
    for (let i = 0; i < files.length; i++) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const img = document.createElement('img');
            img.src = e.target.result;
            img.style.maxWidth = '100%';
            document.execCommand('insertHTML', false, img.outerHTML);
        };
        reader.readAsDataURL(files[i]);
    }
}


var descriptionHidden = document.getElementById("descriptionHidden");
var editor = document.getElementById("editor");

// Pobiera tekst z ukrytej kontrolki i przesyła go do edytora
editor.innerText = descriptionHidden.value;

// Przechwytuje wysłanie formularza i ustawia wartość pola przed przesłaniem
document.getElementById('form').addEventListener('submit', function () {
    descriptionHidden.value = editor.innerText;
});















// Skrypt pozwalający zapamiętać wybrane zdjęcia po przeładowaniu strony
    var filesInput = document.getElementById('files');
    // Funkcja do zapisywania przesłanych plików w sessionStorage
    function saveFiles() {
            var files = filesInput.files;

            if (files.length > 0) {
                var filesArray = Array.from(files);
                var filesData = filesArray.map(file => {
                    return {
        name: file.name,
    type: file.type,
    size: file.size
                    };
                });

    sessionStorage.setItem('uploadedFiles', JSON.stringify(filesData));
            }
        }

    // Funkcja pobiera dane z sesji do zmiennej po załadowaniu okna
    window.onload = function () {
            var uploadedFilesData = sessionStorage.getItem('uploadedFiles');
    if (uploadedFilesData) {
                var uploadedFiles = JSON.parse(uploadedFilesData); // zamienia tekst na obiekt


    // Utworzenie nowej listy plików
    var newFilesList = new DataTransfer();

                uploadedFiles.forEach(file => {
                    // Utworzenie nowego pliku i dodanie go do listy plików
                    var newFile = new File([], file.name, {type: file.type, size: file.size });
    newFilesList.items.add(newFile);
                });

    // Przypisanie nowej listy plików do pola input
    filesInput.files = newFilesList.files;
    sessionStorage.clear();
            }
        }; 