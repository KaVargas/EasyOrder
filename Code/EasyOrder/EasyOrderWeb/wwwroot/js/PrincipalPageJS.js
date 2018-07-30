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