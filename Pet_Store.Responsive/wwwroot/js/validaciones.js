function validar() {
    document.getElementById('email').value;
    campo = event.target;
    valido = document.getElementById('emailOK');

    emailRegex = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#botonEnviar').attr('disabled', false);
    } else {
        valido.innerText = "El correo debe tener el formato: abc@dominio.com";
        $('#botonEnviar').attr('disabled', true);
    }
}

function validarTelefono() {
    var tel = document.getElementById('telefono').value;
    valido = document.getElementById('telOK');

    var min = 20000000;
    var max = 89999999;

    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (tel < min || tel > max) {
        valido.innerText = "Número de teléfono incorrecto";
        $('#botonEnviar').attr('disabled', true);
    } else {
        valido.innerText = "";
        $('#botonEnviar').attr('disabled', false);
    }
}

function validarContrasena() {
    document.getElementById('contrasena').value;
    campo = event.target;
    valido = document.getElementById('passOK');

    contraRegex = /^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (contraRegex.test(campo.value)) {
        valido.innerText = "ak7";
        $('#botonEnviar').attr('disabled', false);
    } else {
        valido.innerText = "La contraseña debe tener al entre 8 y 16 caracteres \n Al menos un dígito \n Al menos una minúscula \n Al menos una mayúscula \n Al menos un caracter no alfanumérico.";
        $('#botonEnviar').attr('disabled', true);
    }
}

function validarContrasenaRepetida() {
    var contrasena1 = document.getElementById('contrasena').value;
    var contrasena2 = document.getElementById('contrasena2').value;
    valido = document.getElementById('pass2OK');

    if (contrasena1 != contrasena2) {
        valido.innerText = "Las contraseñas no coinciden";
        $('#botonEnviar').attr('disabled', true);
    } else {
        valido.innerText = "correctisimo";
        $('#botonEnviar').attr('disabled', false);
    }
}

function LimpiarCampo() {
    $("#email").val($("#email").val().trim());
    $("#nombre").val($("#nombre").val().trim());
    $("#direccion").val($("#direccion").val().trim());
}

function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.iKeyCode
    if (iKeyCode < 48 || iKeyCode > 57)
        return false;
    return true;
}

function soloLetras(e) {
    var key = e.keyCode || e.which,
        tecla = String.fromCharCode(key).toLowerCase(),
        letras = " áéíóúabcdefghijklmnñopqrstuvwxyz",
        especiales = [8, 37, 39, 46],
        tecla_especial = false;

    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
