const form4 = document.getElementById("my-form4");

const gesture = document.getElementById("gesture");
const content4 = document.getElementById("content4");

form4.addEventListener("submit", (e) => {
  e.preventDefault();

  const value = gesture.options[gesture.selectedIndex].value;

  content4.innerHTML = "";

  const i = Math.floor(Math.random() * 3);
  const generatedValue = gesture.options[i].value;
  // console.log(i)

  const el1 = document.createElement("h1");
  el1.textContent = "Zahral si " + value + " a on " + generatedValue;

  const el2 = document.createElement("h1");

  if (generatedValue == value) {
    content4.appendChild(el1);
    el2.textContent = "Remiza";
    content4.appendChild(el2);
    return;
  }

  if (
    (generatedValue === "nuzky" && value === "papir") ||
    (generatedValue === "papir" && value === "kamen") ||
    (generatedValue === "kamen" && value === "nuzky")
  ) {
    // console.log(generatedValue);
    el2.textContent = "Pohral si";
  } else {
    el2.textContent = "Vyhral si";
  }

  content4.appendChild(el1);
  content4.appendChild(el2);
});
