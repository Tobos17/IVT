const form = document.getElementById("my-form");
const text = document.getElementById("Text");

const content1 = document.getElementById("content1");

form.addEventListener("submit", (e) => {
  e.preventDefault();

  // console.log(e.target[0].value);
  const val = e.target[0].value;

  content1.innerHTML = "";

  if (val === null || val === "" || val.length > 10) {
    alert("Wrong number");
    form.reset();
    return;
  }

  for (let i = 1; i <= 20; i++) {
    // console.log(val * i)
    const el = document.createElement("h1");
    el.textContent = val * i;

    content1.appendChild(el);
  }
});
