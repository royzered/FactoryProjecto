
// *   A s t e t h i c s   *
function clr() {
    let inputs = document.getElementsByTagName("input");
    for (let input of inputs) {
        let clearThat = input.value = "";
    }
    let spn = document.getElementsByTagName("span");
    for ( let span of spn) {
        let clearSpans = span.innerHTML = "";
    }
}

function loader(res) {

    let loader = document.getElementById("loader");
    if(res.ok) 
    {
        loader.style.display = "none";
    }
    
}


// let x = 1;
// let divs = [document.getElementById("empDiv"), document.getElementById("depDiv"), document.getElementById("shiftDiv")];

// function seeDiv(div) {
//     let fadeIn = "fadeIn 0.3s ease-out";
//     let fadeOut = "fadeOut 0.3s ease-out";
//     if(x == 0) {
//         div.style.display = "block";
//         div.style.animation = fadeIn;
//         x = 1;
//     }
//     else if (x == 1){
//         div.style.animation = fadeOut;
//         setTimeout(() => {
//             div.style.display = "none";
//             x = 0;
//         }, 250);
//     }
// }
