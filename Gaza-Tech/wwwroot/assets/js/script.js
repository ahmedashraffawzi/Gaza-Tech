
// countdown timer
let daysDiv = document.querySelector(".days")
let hoursDiv = document.querySelector(".Hours")
let minutesDiv = document.querySelector(".Minutes")
let secondsDiv = document.querySelector(".Seconds")

let timer = new Date("Mar 7, 2022 12:00:00").getTime();

let counter = setInterval(() => {
    let datenow = new Date().getTime()
    let diff = timer - datenow;

    let days = Math.floor(diff / (1000 * 60 * 60 * 24));
    let hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60))
    let minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60))
    let seconds = Math.floor((diff % (1000 * 60)/ (1000)))
    
    // console.log(days)
    // console.log(hours);
    // console.log(minutes);
    // console.log(seconds);

    daysDiv.innerHTML = `<h2>${days}</h2><p>days</p>`
    hoursDiv.innerHTML = `<h2>${hours}</h2><p>hours</p>`
    minutesDiv.innerHTML = `<h2>${minutes}</h2><p>minutes</p>`
    secondsDiv.innerHTML = `<h2>${seconds}</h2><p>seconds</p>`

    if (diff <= 0){
        clearInterval(counter)
    }
}, 1000)

//type animation
var options = {
    strings: ['Any Tech you need', 'Any Laptops', 'Any Mobile phones', 'Any Accessories', 'All Tech ... in one place'],
    typeSpeed: 100,
    loop: true,
    loopCount: Infinity,
    showCursor: false,
    backSpeed: 100
};
var typed = new Typed('.element', options);

// Join us confirm
let jform = document.querySelector(".Join-us form")
let jInput= document.querySelector(".Join-us input")

jform.onsubmit = () => {
    if (jInput.value != "") {
        alert(`We will send to you thae lastest offers to: ${jInput.value}`);
    }
}
