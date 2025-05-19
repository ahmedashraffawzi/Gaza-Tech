// filter products depending on url
let filterAll = document.querySelector("button[data-filter='*']")
let filterMobiles = document.querySelector("button[data-filter='.mobiles']")
let filterLaptops = document.querySelector("button[data-filter='.Laptops']")
let filterAccessories = document.querySelector("button[data-filter='.accessories']")

let pageUrl = window.location.href.toLowerCase();

window.onload = () => {
    if (pageUrl.includes("mobiles"))
        filterMobiles.click();
    else if (pageUrl.includes("laptops"))
        filterLaptops.click()
    else if (pageUrl.includes("accessories"))
        filterAccessories.click()
    else
        filterAll.click()
}

$('.catogery-list').click(function () {
    $(this).addClass('catogery-list-active');
    $(this).siblings().removeClass('catogery-list-active');
})

let filterbtn = document.querySelector(".filterbtn");
let filter = document.querySelector(".filter");
let closeFilter = document.querySelector("#close-f");


//Open Filter
filterbtn.onclick = () => {
    filter.classList.toggle("active");
    console.log("open");
};

//Close Filter
closeFilter.onclick = () => {
    filter.classList.remove("active");
    console.log("close");
};

// filters
var $grid = $('#product-list').isotope({
    // options
});

// filter items on button click
$('.filter-button-group, .other-filter-group').on('click', 'button, li', function () {
    var filterValue = $(this).attr('data-filter');
    $grid.isotope({ filter: filterValue });
});

// filter by Price
let productPrice = document.querySelectorAll(".product-box .price")
let productBox = document.querySelectorAll(".product-box")

for (let i = 0; i < productBox.length; i++) {
    let pPrice = parseInt(productPrice[i].textContent.replace("$", ""))

    if (pPrice >= 0 && pPrice < 100)
        productBox[i].classList.add("from-0to100");

    else if (pPrice >= 100 && pPrice < 500)
        productBox[i].classList.add("from-100to500")

    else if (pPrice >= 500 && pPrice < 1000)
        productBox[i].classList.add("from-500to1000")

    else if (pPrice >= 1000 && pPrice < 5000)
        productBox[i].classList.add("from-1000to5000")

    else if (pPrice >= 5000 && pPrice < 15000)
        productBox[i].classList.add("from-5000to15000")
    else
        productBox[i].classList.add("More-than15000")
}


// filter by Brnad
let modelBrand = document.querySelectorAll(".Model-Brand")

for (let i = 0; i < productBox.length; i++) {
    let pBrand = modelBrand[i].textContent;

    switch (pBrand) {
        case "Hp":
            productBox[i].classList.add("Hp");
            break;
        case "Dell":
            productBox[i].classList.add("Dell");
            break;
        case "Mac":
            productBox[i].classList.add("Mac");
            break;
        case "Acer":
            productBox[i].classList.add("Acer");
            break;
        case "Samsung":
            productBox[i].classList.add("Samsung");
            break;
        case "Huawei":
            productBox[i].classList.add("Huawei");
            break;
        case "IPhone":
            productBox[i].classList.add("IPhone");
            break;
        case "Oppo":
            productBox[i].classList.add("Oppo");
            break;
        case "Realme":
            productBox[i].classList.add("Realme");
            break;
        case "xiaomi":
            productBox[i].classList.add("xiaomi");
            break;
    }
}