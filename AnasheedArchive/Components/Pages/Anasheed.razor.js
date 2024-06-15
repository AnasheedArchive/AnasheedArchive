const breakPoint = 576;
const ruler = document.getElementById("lyrics-ruler");

window.onresize = HideRuler;
window.onpageshow = HideRuler;

function HideRuler() {
    let width = window.innerWidth;
    if (width <= breakPoint) {
        ruler.classList.add("invisible");
    }
    if (width > breakPoint) {
        ruler.classList.remove("invisible");
    }
}