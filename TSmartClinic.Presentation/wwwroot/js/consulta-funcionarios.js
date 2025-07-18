"use strict";
$(document).ready(async function () {
    //dataTable();

    try {
        BuscaSetores(1) // todo
        BuscaFuncoes(1) // todo
        BuscaCentrosCusto(1)  // todo
    } catch (e) {
        console.error('Erro ao carregar dados da empresa ativa:', error);
    }
});

$('#pesquisar').click(function () {
    dataTable();
});

$('#txtAmbienteTrabalho').autocomplete({
    source: function (request, response) {
        var param = { parteNome: request.term, idEmpresa: 2 }
        $.ajax({
            method: "POST",
            url: pathRoot.concat("AmbienteTrabalho/AutoComplete"),
            data: param,
            dataType: "json",
            success: function (data) {
                response($.map(data, function (item) {
                    return {
                        value: item.id,
                        label: item.nome
                    }
                }))
            },
            error: function (xhr, error, thrown) {
                console.log(error);
            },
        });
    },    
    autoFocus: false,
    minLength: 1,
    select: function (event, ui) {
        $("#idAmbienteTrabalho").val(ui.item.value);
        $("#txtAmbienteTrabalho").val(ui.item.label);
        event.preventDefault();
    }
});

function dataTable() {
    try {
        var dataTableObj = dataTableInit(pathRoot.concat('Funcionario/DataTable'), criaFiltroEvento());
        dataTableObj.serverSide = false;
        dataTableObj.searching = false;

        dataTableObj.order = [[5, "desc"]],

        dataTableObj.columns = montaColumns();

        dataTableObj.createdRow = function (row, data, dataIndex) {
            $(row).attr('id', data.id);
        }

        $('#gridResultados').DataTable().destroy();

        return $("#gridResultados").DataTable(dataTableObj);
    } catch (e) {
        console.log(e);
    }
}

function montaColumns() {
    return [
        { data: "matricula", name: "0", orderable: true },
        { data: "nome", name: "1", orderable: true },
        { data: "cpf", name: "2", orderable: true },
        { data: "nomeEmpresa", name: "3", orderable: true },
        { data: "nomeSituacao", name: "4", orderable: true },
        { data: "id", visible: false}
    ];
}

function criaFiltroEvento() {
    var filtro = {
        filtroCampo: $("#filtroCampo").val(),
        filtroValor: $("#filtroValor").val(),
        idSituacao: $("#cboSituacao").val(),
        idTipoEmpresa: $("#cboEmpresa").val(),
        idSetor: $("#cboSetor").val(),
        idFuncao: $("#cboFuncao").val(),
        idCentroCusto: $("#cboCentroCusto").val(),
        idAmbienteTrabalho: $("#idAmbienteTrabalho").val()
    };

    return filtro;
}

$("#gridResultados").on('click', 'tr', function () {
    let idFunc = $(this).closest('tr').attr('id');
    if (!!idFunc) {
        window.location.href = pathRoot.concat('Funcionario/Central/', idFunc);
    }
});

function BuscaSetores(id) {
    var param = { idEmpresa : id }
    $.ajax({
        method: "POST",
        url: pathRoot.concat("Setor/ListarPorEmpresa"),
        data: param,
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                $('#cboSetor').append($('<option>', {
                    value: item.codigo,
                    text: item.nome
                }));
            });
        }
    });
}

function BuscaFuncoes(id) {
    var param = { idEmpresa: id }
    $.ajax({
        method: "POST",
        url: pathRoot.concat("Funcao/ListarPorEmpresa"),
        data: param,
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                $('#cboFuncao').append($('<option>', {
                    value: item.codigo,
                    text: item.nome
                }));
            });
        },
        error: function (xhr, error, thrown) {
            console.log(error);
        },
    });
}

function BuscaCentrosCusto(id) {
    var param = { idEmpresa: id }
    $.ajax({
        method: "POST",
        url: pathRoot.concat("CentroCusto/ListarPorEmpresa"),
        data: param,
        dataType: "json",
        success: function (data) {           
            $.each(data, function (i, item) {
                $('#cboCentroCusto').append($('<option>', {
                    value: item.codigo,
                    text: item.nome
                }));
            });
        },
        error: function (xhr, error, thrown) {
            console.log(error);
        },
    });
}
