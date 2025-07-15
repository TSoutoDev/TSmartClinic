let lista_medicamentos = [];

const criar_lista_medicamentos = () => {
    let linhas = $("#gridMedicamentos>tbody>tr").toArray();

    linhas.forEach(function (item) {
        let indice = $(item).attr("data-id");
        let idMedicamento = $(`#ListaKitMedicamento_${indice}_IdMedicamento`).val();
        let nomeMedicamento = $(`#ListaKitMedicamento_${indice}_NomeMedicamento`).val();
        let quantidade = $(`#ListaKitMedicamento_${indice}_Quantidade`).val();

        adicionar_medicamento(indice, idMedicamento, nomeMedicamento, quantidade);
    });
}

const adicionar_medicamento = (indice, idMedicamento, nomeMedicamento, quantidade) => {
    let medicamento = {
        indice: parseInt(indice),
        idMedicamento,
        nomeMedicamento,
        quantidade
    };

    //Adiciona o medicamento
    lista_medicamentos.push(medicamento);
}

const carregar_grid_medicamentos = () => {
    $("#gridMedicamentos>tbody").html('');

    //ordena os dados
    let lista_ordenada = lista_medicamentos.slice().sort(function (a, b) {
        return (a.nomeMedicamento.localeCompare(b.nomeMedicamento));
    });

    lista_ordenada.forEach(function (item) {
        let row = `
            <tr data-id='${item.indice}'>
                <td>
                    <input type="hidden" name="ListaKitMedicamento.Index" value="${item.indice}" />
                    <input type="hidden" name="ListaKitMedicamento[${item.indice}].IdMedicamento" id='ListaKitMedicamento_${item.indice}_IdMedicamento' ${(!!item.idMedicamento) ? 'value="' + item.idMedicamento + '"' : ""} />
                    <input type="hidden" name="ListaKitMedicamento[${item.indice}].NomeMedicamento" id='@($"ListaKitMedicamento_${item.indice}_NomeMedicamento")' ${(!!item.nomeMedicamento) ? 'value="' + item.nomeMedicamento + '"' : ""} />
                    ${item.nomeMedicamento}
                </td>
                <td>
                    <input type="hidden" name="ListaKitMedicamento[${item.indice}].Quantidade" id='ListaKitMedicamento_${item.indice}_Quantidade' ${(!!item.quantidade) ? 'value="' + item.quantidade + '"' : ""} />
                    ${item.quantidade}
                </td>
                <td class="text-end">
                    <a href="javascript:excluir_medicamento(${item.indice})" title="Excluir medicamento"><i class="fas fa-trash icone-exclusao"></i></a>
                </td>
            </tr>
        `;

        $("#gridMedicamentos>tbody").append(row);
    });
}

function localizar_indice_array(indice) {
    let indice_array = lista_medicamentos.findIndex(function (medicamento) {
        return medicamento.indice === parseInt(indice);
    });

    return indice_array;
}

function localizar_indice_medicamento(idMedicamento) {
    let indice_array = lista_medicamentos.findIndex(function (medicamento) {
        return parseInt(medicamento.idMedicamento) === parseInt(idMedicamento);
    });

    return indice_array;
}

const excluir_medicamento = (indice) => {
    //Verifica se o índice já está no array
    let indice_array = localizar_indice_array(indice);
    if (indice_array >= 0) {
        lista_medicamentos.splice(indice_array, 1);
        carregar_grid_medicamentos();
    }
}

const limpar_formulario = () => {
    $("#medicamento").val(null);
    $("#quantidade").val(null);
}

$("#btnGravarMedicamento").on('click', function () {
    let idMedicamento = $("#medicamento").val();
    let nomeMedicamento = $("#medicamento option:selected").text();
    let quantidade = $("#quantidade").val();

    if (!idMedicamento) {
        alert('Favor selecionar o medicamento');
        return;
    } else {
        if (localizar_indice_medicamento(idMedicamento) >= 0) {
            alert('O medicamento já foi incluído.  Favor selecionar outro');
            return;
        }
    }

    if (!quantidade) {
        alert('Favor informar a quantidade');
        return;
    } else {
        if (quantidade <= 0) {
            alert('Favor informar uma quantidade maior que zero');
            return;
        }
    }

    adicionar_medicamento(
        Math.floor((Math.random() * 100000) + 1),
        idMedicamento,
        nomeMedicamento,
        quantidade
    );

    carregar_grid_medicamentos();
    limpar_formulario();
    $("#medicamento").focus();
});

$(document).ready(function () {
    criar_lista_medicamentos();
});