let createFrom = document.getElementById("Create");
let categ = document.getElementById("Category");
let mob = document.getElementById("mob");
let lab = document.getElementById("lab");
let Acc = document.getElementById("Acc");

//image input
let picInput = document.querySelector(".pic-input");
let imageAdd = document.querySelector("#image-add");
let imgLink;

let arr = createFrom.elements
console.log(createFrom)
console.log(arr)

console.log("ahmed")
function changeValue() {
    if (this.value == 1) {
        // show lab-brand
        mob.classList.add("d-none");
        Acc.classList.add("d-none");
        lab.classList.remove("d-none");

    } else if (this.value == 2) {
        // show mob-brand
        lab.classList.add("d-none");
        Acc.classList.add("d-none");
        mob.classList.remove("d-none");
    } else if (this.value == 3) {
        // show Acc-Kind
        lab.classList.add("d-none");
        mob.classList.add("d-none");
        Acc.classList.remove("d-none");
    } else {
        // show nothing
        mob.classList.add("d-none");
        lab.classList.add("d-none");
        Acc.classList.add("d-none");
    }
}

categ.addEventListener("change", changeValue);

// on onload window automaticlly show the right category
window.onload = changeValue

console.log("ahmed")
 //on submit for remove the other categry(temperory sol for category valdation issue)
let catgoreyNum= 0
createFrom.addEventListener("submit" ,() => {
    if (categ.value == 1) {
        mob.remove();
        Acc.remove();
    } else if (categ.value == 2){
        lab.remove();
        Acc.remove();
    } else if (categ.value == 3){
        lab.remove();
        mob.remove();
    }
    catgoreyNum = categ.value;
})

// restore the removed inputs if the user intends to change the value
categ.addEventListener("change", () => {
    console.log(catgoreyNum)
    if (catgoreyNum != 0) {
        categ.after(mob)
        categ.after(lab)
        categ.after(Acc)
    }
})

imageAdd.onclick = () => {
    imgLink = prompt("enter the link of image here");
    if (imgLink !== "" && imgLink !== null) {
        if (picInput.value === '')
            picInput.value += `${imgLink}`;
        else if (picInput.value !== '')
            picInput.value += `,\n${imgLink}`;
    }
}
