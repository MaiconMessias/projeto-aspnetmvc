// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function ()
{
    $("#telefone").inputmask("mask", { "mask": "(99) 9999-99999" });
    $("#cpf").inputmask("mask", { "mask": "999.999.999-99" }, {reverse:true});
    $("#cep").inputmask("mask", { "mask": "99999-999" });
    $("#data").inputmask("mask", { "mask": "99/99/9999" });
    $("#preco").inputmask("mask", { "mask": "999.999,99" }, { reverse: true });
    $("#valor").inputmask("mask", { "mask": "#.##9,99" }, { reverse: true });
    $("#ip").inputmask("mask", { "mask": "999.999.999.999" });
});