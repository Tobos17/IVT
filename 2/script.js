const form2 = document.getElementById("my-form2");
const a = document.getElementById("a");
const b = document.getElementById("b");
const c = document.getElementById("c");

const content2 = document.getElementById("content2");

form2.addEventListener("submit", (e) => {
  e.preventDefault();

  const a = e.target[0].value;
  const b = e.target[1].value;
  const c = e.target[2].value;
  // console.log(a, b, c);

  content2.innerHTML = "";

  if (
    a === null ||
    a === "" ||
    b === null ||
    b === "" ||
    c === null ||
    c === ""
  ) {
    alert("Enter all umbers");
    form2.reset();
    return;
  }

  D = b * b - 4 * a * c;

  const el = document.createElement("h1");

  if (D < 0) {
    // console.log("nejde")

    el.textContent = "koreny neexistuji";
  } else {
    let x1 = (-b + Math.sqrt(D)) / (2 * a);
    let x2 = (-b - Math.sqrt(D)) / (2 * a);

    x1 = x1.toFixed(2);
    x2 = x2.toFixed(2);

    el.textContent = x1 + ", " + x2;
    // console.log(x1, x2)
  }

  content2.appendChild(el);
  form2.reset();
});
