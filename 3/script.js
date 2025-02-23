const form3 = document.getElementById("my-form3");

const content3 = document.getElementById("content3");

form3.addEventListener("submit", (e) => {
  e.preventDefault();

  content3.innerHTML = "";

  const rand = Math.floor(Math.random() * 6 + 1);
  // console.log(rand);

  const el = document.createElement("h1");

  el.textContent = rand;

  content3.appendChild(el);
});
