var fourOrder;
var id1;
var id2;
var id3;
var id4;

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
        dispatchOrder(id1);
        $('#uno')[0].innerText = "";
    }
}

function Mostrar_Ocultar_Dos() {
    var dos = document.getElementById("dos");

    if (dos.style.display == "none") {
        MostrarDos();
    } else {
        OcultarDos();
        dispatchOrder(id2);
        $('#dos')[0].innerText = "";
    }
}

function Mostrar_Ocultar_Tres() {
    var tres = document.getElementById("tres");

    if (tres.style.display == "none") {
        MostrarTres();
    } else {
        OcultarTres();
        dispatchOrder(id3);
        $('#tres')[0].innerText = ""
    }
}

function Mostrar_Ocultar_Cuatro() {
    var cuatro = document.getElementById("cuatro");

    if (cuatro.style.display == "none") {
        MostrarCuatro();
    } else {
        OcultarCuatro();
        dispatchOrder(id4);
        $('#cuatro')[0].innerText = ""
    }
}

function getFourOrders() {
    $.ajax({
        url: "/api/order/GetOrders",
        type: "get", //send it through get method
        data: {
            amount: 4
        },
        success: function (response) {
            fourOrder = response;
            fillEmpty();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function dispatchOrder(id) {
    $.ajax({
        url: "/api/order/DispatchOrder",
        type: "get", //send it through get method
        data: {
            orderId: id
        },
        success: function (response) {
            fourOrder = response;
            fillEmpty();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}


window.onload = getFourOrders();

function fillEmpty() {
    var i = 0;
    var detalle;
    for (i = 0; i < fourOrder.length; i++) {
        switch (i) {
            case 0:
                detalle = "Detalle de Orden\n";
                for (var j = 0; j < fourOrder[i].platos.length; j++) {
                    detalle += ("" + fourOrder[i].platos[j].nombre + ":" + fourOrder[i].platos[j].cantidad + "\n");
                }
                id1 = fourOrder[i].orderNumber;
                $('#uno')[0].innerText = "" + detalle;
                Mostrar_Ocultar_Uno();
                break;
            case 1:
                detalle = "Detalle de Orden\n";
                for (var j = 0; j < fourOrder[i].platos.length; j++) {
                    detalle += ("" + fourOrder[i].platos[j].nombre + ":" + fourOrder[i].platos[j].cantidad + "\n");
                }
                id2 = fourOrder[i].orderNumber;
                $('#dos')[0].innerText = "" + detalle;
                Mostrar_Ocultar_Dos();
                break;
            case 2:
                detalle = "Detalle de Orden\n";
                for (var j = 0; j < fourOrder[i].platos.length; j++) {
                    detalle += ("" + fourOrder[i].platos[j].nombre + ":" + fourOrder[i].platos[j].cantidad + "\n");
                }
                id3 = fourOrder[i].orderNumber;
                $('#tres')[0].innerText = "" + detalle;
                Mostrar_Ocultar_Tres();
                break;
            case 3:
                detalle = "Detalle de Orden\n";
                for (var j = 0; j < fourOrder[i].platos.length; j++) {
                    detalle += ("" + fourOrder[i].platos[j].nombre + ":" + fourOrder[i].platos[j].cantidad + "\n");
                }
                id4 = fourOrder[i].orderNumber;
                $('#cuatro')[0].innerText = "" + detalle;
                Mostrar_Ocultar_Cuatro();
                break;
        }
    }
}

function fillOrder(order) {
    if (document.getElementById("uno").style.display == "none") {
        detalle = "Detalle de Orden\n";
        for (var j = 0; j < order.Platos.length; j++) {
            detalle += ("" + order.Platos[j].Nombre + ":" + order.Platos[j].Cantidad + "\n");
        }
        id1 = order.OrderNumber;
        $('#uno')[0].innerText = "" + detalle;
        Mostrar_Ocultar_Uno();
        return;
    }
    if (document.getElementById("dos").style.display == "none") {
        detalle = "Detalle de Orden\n";
        for (var j = 0; j < order.Platos.length; j++) {
            detalle += ("" + order.Platos[j].Nombre + ":" + order.Platos[j].Cantidad + "\n");
        }
        id2 = order.OrderNumber;
        $('#dos')[0].innerText = "" + detalle;
        Mostrar_Ocultar_Dos();
        return;
    }
    if (document.getElementById("tres").style.display == "none") {
        detalle = "Detalle de Orden\n";
        for (var j = 0; j < order.Platos.length; j++) {
            detalle += ("" + order.Platos[j].Nombre + ":" + order.Platos[j].Cantidad + "\n");
        }
        id3 = order.OrderNumber;
        $('#tres')[0].innerText = "" + detalle;
        Mostrar_Ocultar_Tres();
        return;
    }
    if (document.getElementById("cuatro").style.display == "none") {
        detalle = "Detalle de Orden\n";
        for (var j = 0; j < order.Platos.length; j++) {
            detalle += ("" + order.Platos[j].Nombre + ":" + order.Platos[j].Cantidad + "\n");
        }
        id4 = order.OrderNumber;
        $('#cuatro')[0].innerText = "" + detalle;
        Mostrar_Ocultar_Cuatro();
        return;
    }
}
