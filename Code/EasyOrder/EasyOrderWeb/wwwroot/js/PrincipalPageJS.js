function MostrarComidas() {
    document.getElementById("comidas").style.display = "block";
}

function OcultarComidas() {
    document.getElementById("comidas").style.display = "none";
}

function MostrarBebidas() {
    document.getElementById("bebidas").style.display = "block";
}

function OcultarBebidas() {
    document.getElementById("bebidas").style.display = "none";
}

function Mostrar_Comidas() {
    OcultarBebidas();
    MostrarComidas();
}

function Mostrar_Bebidas() {
    OcultarComidas();
    MostrarBebidas();
}

$(document).ready(function () {
    $("#siguienteBtn").click(function () {
        if ($('#Number1')[0].value > 0) {
            $('#pa1').css("display", "block");
            $('#num1')[0].innerText = $('#Number1')[0].value;
        }

        if ($('#Number2')[0].value > 0) {
            $('#pa2').css("display", "block");
            $('#num2')[0].innerText = $('#Number2')[0].value;
        }
        if ($('#Number3')[0].value > 0) {
            $('#pa3').css("display", "block");
            $('#num3')[0].innerText = $('#Number3')[0].value;
        }
        if ($('#Number4')[0].value > 0) {
            $('#pa4').css("display", "block");
            $('#num4')[0].innerText = $('#Number4')[0].value;
        }
        if ($('#Number5')[0].value > 0) {
            $('#pa5').css("display", "block");
            $('#num5')[0].innerText = $('#Number5')[0].value;
        }
        if ($('#Number6')[0].value > 0) {
            $('#pa6').css("display", "block");
            $('#num6')[0].innerText = $('#Number6')[0].value;
        }
        if ($('#Number7')[0].value > 0) {
            $('#pa7').css("display", "block");
            $('#num7')[0].innerText = $('#Number7')[0].value;
        }
        if ($('#Number8')[0].value > 0) {
            $('#pa8').css("display", "block");
            $('#num8')[0].innerText = $('#Number8')[0].value;
        }
        if ($('#Number9')[0].value > 0) {
            $('#pa9').css("display", "block");
            $('#num9')[0].innerText = $('#Number9')[0].value;
        }
    });
});

function encerar() {
    document.getElementById("pa1").style.display = "none";
    $('#pa2').css("display", "none");
    $('#pa3').css("display", "none");
    $('#pa4').css("display", "none");
    $('#pa5').css("display", "none");
    $('#pa6').css("display", "none");
    $('#pa7').css("display", "none");
    $('#pa8').css("display", "none");
    $('#pa9').css("display", "none");
}

function sendPedido() {
    $.ajax({
        url: "/api/order/add",
        method: "POST",
        data: JSON.stringify({
            NumeroMesa: 1,
            Platos: [
                { Nombre: "porcionYuca", Cantidad: $('#Number1')[0].value },
                { Nombre: "carneAhumada", Cantidad: $('#Number2')[0].value },
                { Nombre: "caldoCarachama", Cantidad: $('#Number3')[0].value },
                { Nombre: "tilapiaFrita", Cantidad: $('#Number4')[0].value },
                { Nombre: "maitoTilapia", Cantidad: $('#Number5')[0].value },
                { Nombre: "maitoPollo", Cantidad: $('#Number6')[0].value },
                { Nombre: "gaseosa", Cantidad: $('#Number7')[0].value },
                { Nombre: "guayusa", Cantidad: $('#Number8')[0].value },
                { Nombre: "agua", Cantidad: $('#Number9')[0].value }
            ],
            NombreEmpleado: "kgvargas",
            EstadoOrden:"Espera"
        }),
        dataType: 'json',
        contentType: "application/json",
        async: false,
        success: function (result, status, jqXHR) {
            if (result.allowed) {
                location.href = "https://easyorderweb.azurewebsites.net/PrincipalPage";
            }
            else {
                location.href = "https://easyorderweb.azurewebsites.net/Error";

            }
        },
        error(jqXHR, textStatus, errorThrown) {
            location.href = "https://easyorderweb.azurewebsites.net/Error";
        }
    });
}