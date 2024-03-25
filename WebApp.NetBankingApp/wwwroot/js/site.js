// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

$(document).ready(function () {
    // Función para agregar o quitar el nuevo campo de entrada
    $('#roles').change(function () {
        var selectedOption = $(this).val();
        if (selectedOption === "Customer") {
            $('#addFieldCustomer').html('<label asp-for="InitialAmount" class="form-label fw-bold">Initial Amount</label> <input asp-for="InitialAmount" class="form-control"><span asp-validation-for="InitialAmount" class="text-danger"></span>');
        } else {
            $('#addFieldCustomer').html('');
        }
    });
});

// <label for="nuevoInput">Nuevo campo:</label><input type="text" class="form-control" id="nuevoInput" name="nuevoInput">