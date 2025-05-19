// hide nav bar
let menu = document.querySelector('#menu-bar i')
let navbar = document.querySelector('.top-nav, .menu-links')

menu.onclick = function () {
    menu.classList.toggle('fa-times')
    navbar.classList.toggle('active')
}

// back to top
let upBtn = document.querySelector(".back-top")

window.onscroll = () => {
    // console.log(window.scrollY)
    if (window.scrollY >= 400) {
        upBtn.style = ("opacity: 1;")
    } else {
        upBtn.style = ("opacity: 0;")
    }
}

upBtn.onclick = () => {
    scrollTo({ top: 0, behavior: "smooth" });
}

// autocomplete

$(async function () {
    var availableTags = [
        "samsung",
        "apple",
        "dell",
        "hp",
        "mobile",
        "laptop",
        "realme",
        "oppo",
        "xiaomi",
        "lenovo",
        "accessories",
        "hand free",
        "covers"
    ];
    $("#tags").autocomplete({
        source: availableTags
    });
});

/*console.log("ahmedaaaaaaaadfasdfasa")*/
// Preloader
let preloader = document.querySelector('#preloader, #preloader0');
if (preloader) {
    window.addEventListener('load', async () => {
        setTimeout(() => preloader.remove(), 250)

    });
}