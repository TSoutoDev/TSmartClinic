"use strict";

function languagePtBr() {
    return {
        decimal: "",
        emptyTable: "Nenhum registro encontrado",
        info: "Mostrando _START_ até _END_ de _TOTAL_ registros",
        infoEmpty: "Mostrando 0 até 0 de 0 registros",
        infoFiltered: "(filtrado de _MAX_ registros no total)",
        infoPostFix: "",
        thousands: ".",
        lengthMenu: "Mostrar _MENU_ registros",
        loadingRecords: "Carregando...",
        processing: "Processando...",
        search: "Pesquisar:",
        zeroRecords: "Nenhum registro encontrado",
        paginate: {
            first: "Primeira",
            last: "Última",
            next: "Próxima",
            previous: "Anterior"
        }
    };
}

function dataTableInit(path, params) {
    var obj = {
        processing: true,
        serverSide: false,
        filter: true,
        orderMulti: true,
        pagingType: "full_numbers",
        language: languagePtBr(),

        ajax: {
            url: path,
            type: "POST",
            datatype: "json",
            data: {
                model: params
            },
            dataSrc: function (response) {
                if (!!response && response.length <= 1000) {
                    $('#alertExcessoLinhas').addClass('d-lg-none');
                    return response;
                } else {
                    $('#alertExcessoLinhas').removeClass('d-lg-none');
                    return response.slice(0, 1000);
                }
            },

            error: function (xhr, error, thrown) {
                console.log(error);
            }
        }
    };

    return obj;
}

function dataTableLocalInit() {
    return {
        processing: true,
        serverSide: false,
        paging: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        pagingType: "full_numbers",
        searching: false,
        ordering: true,
        orderMulti: true,
        language: languagePtBr()
    };
}