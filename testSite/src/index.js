const signalR = require('@aspnet/signalr');
const axios = require('axios');
const feather = require('feather-icons')
const join = require('url-join');

const messages = document.getElementById("messages");

const status = document.getElementById("status");
const connect = document.getElementById("connect");
const disconnect = document.getElementById("disconnect");

const connectError = document.getElementById("connect-error");
const sendError = document.getElementById("send-error");

const url = document.getElementById("url");
const method = document.getElementById("method");
const hub = document.getElementById("hub");

let connection;

document.getElementById("disconnect").addEventListener("click", event => {
  const source = event.target || event.srcElement;

  if (connection) {
    source.setAttribute("disabled", "");

    connection.stop().then(() => {
      console.log("Stopped!")
      source.classList.add("hidden");

      connect.removeAttribute("disabled");
      connect.classList.remove("hidden");

      status.innerHTML = "";
    });
  }
});

document.getElementById("connect").addEventListener("click", event => {
  const source = event.target || event.srcElement;

  source.setAttribute("disabled", "");

  connection = new signalR.HubConnectionBuilder()
    .withUrl(join(url.value, hub.value))
    .build();

  connection.on(method.value, data => {
    console.log("Recieved message", data);

    const card = document.createElement("div");
    card.className = "card fluid";
    card.innerHTML = `<div class="section"><h3>Message</h3><p><pre>${JSON.stringify(data, null, 4)}</pre></p></div>`

    messages.insertBefore(card, messages.childNodes[0])
  });

  connection.start()
    .then(() => {
      connect.classList.add("hidden");
      disconnect.classList.remove("hidden");

      status.innerHTML = feather.icons["check-circle"].toSvg({ color: "green" })
      connectError.classList.add("hidden");
    })
    .catch(reason => {
      connect.removeAttribute("disabled");
      connect.classList.remove("hidden");
      disconnect.classList.add("hidden");

      status.innerHTML = feather.icons.x.toSvg({ color: "red" })

      connectError.classList.remove("hidden");
      connectError.innerHTML = `<div class="section"><p class="doc">${reason}</p></div>`;
    });
});


document.getElementById("send").addEventListener("click", () => {
  const postUrl = join(url.value, "api/send", method.value);
  const message = document.getElementById("message").value;
  axios.post(postUrl, message, {
    mode: 'no-cors',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json',
    },
  })
    .then(() => {
      sendError.classList.add("hidden");
    })
    .catch(reason => {
      sendError.classList.remove("hidden");
      sendError.innerHTML = `<div class="section"><p class="doc">${reason}</p></div>`;
    });
});
