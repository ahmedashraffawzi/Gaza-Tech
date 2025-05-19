
let caTitles = document.querySelectorAll("#ca-title span")
let caBody = document.querySelectorAll("#ca-body div")

console.log(caTitles)
console.log(caBody)

for (let index in caTitles) {
        caTitles[0].style.backgroundColor = "var(--third-color)"
        caTitles[index].onclick = () => {

                for (let i in caBody) {
                        caBody[i].setAttribute("hidden", "")
                        caTitles[i].style.backgroundColor = ""
                        caBody[index].removeAttribute("hidden")
                        caTitles[index].style.backgroundColor = "var(--third-color)"
                }

        }
}