
function MostrarUno() {
    document.getElementById("uno").style.display = "block";
}
function OcultarUno() {
    document.getElementById("uno").style.display = "none";
}

function MostrarDos() {
    document.getElementById("dos").style.display = "block";
}
function OcultarDos() {
    document.getElementById("dos").style.display = "none";
}
function MostrarTres() {
    document.getElementById("tres").style.display = "block";
}
function OcultarTres() {
    document.getElementById("tres").style.display = "none";
}

function MostrarCuatro() {
    document.getElementById("cuatro").style.display = "block";
}
function OcultarCuatro() {
    document.getElementById("cuatro").style.display = "none";
}


function Mostrar_Ocultar_Uno() {
    var uno = document.getElementById("uno");

    if (uno.style.display == "none") {
        MostrarUno();
    } else {
        OcultarUno();
    }
}

function Mostrar_Ocultar_Dos() {
    var dos = document.getElementById("dos");

    if (dos.style.display == "none") {
        MostrarDos();
    } else {
        OcultarDos();
    }
}

function Mostrar_Ocultar_Tres() {
    var tres = document.getElementById("tres");

    if (tres.style.display == "none") {
        MostrarTres();
    } else {
        OcultarTres();
    }
}

function Mostrar_Ocultar_Cuatro() {
    var cuatro = document.getElementById("cuatro");

    if (cuatro.style.display == "none") {
        MostrarCuatro();
    } else {
        OcultarCuatro();
    }
}

function getFourOrders() {
    $.ajax({
        url: "/api/GetOrders",
        type: "get", //send it through get method
        data: {
            amount: 4
        },
        success: function (response) {
            //Do Something
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

window.onload = getFourOrders();