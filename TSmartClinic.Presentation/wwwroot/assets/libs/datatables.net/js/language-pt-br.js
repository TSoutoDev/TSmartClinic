'use strict';
var pathRoot = window.location.origin.concat('/');

if (window.location.href.replace("https://", "").split("/")[1] !== "") {
    pathRoot = pathRoot.concat(window.location.href.replace("https://", "").split("/")[1], "/")
}

function languagePtBr() {
    return {
        "sEmptyTable": "Nenhum registro encontrado",
        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "_MENU_ resultados por página",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "<img style='width:150px; height:150px;' src='" + pathRoot + "images/loading.gif' />",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": ">",
            "sPrevious": "<",
            "sFirst": "<<",
            "sLast": ">>"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        }
    }
}