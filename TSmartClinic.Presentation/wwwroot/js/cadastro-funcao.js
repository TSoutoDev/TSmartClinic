let lista_atividades = [];

const criar_lista_atividades = () => {
    let linhas = $("#gridAtividades>tbody>tr").toArray();

    linhas.forEach(function (item) {
        let indice = $(item).attr("data-id");
        let id = $(`#ListaFuncaoAtividade_${indice}_Id`).val();
        let codigo = $(`#ListaFuncaoAtividade_${indice}_Codigo`).val();
        let principal = $(`#ListaFuncaoAtividade_${indice}_Principal`).val();
        let nome = $(`#ListaFuncaoAtividade_${indice}_Nome`).val();
        let descricao = $(`#ListaFuncaoAtividade_${indice}_Descricao`).val();
        let ativo = $(`#ListaFuncaoAtividade_${indice}_Ativo`).val();
        let trabalhoAltura = $(`#ListaFuncaoAtividade_${indice}_TrabalhoAltura`).val();
        let espacoConfinado = $(`#ListaFuncaoAtividade_${indice}_EspacoConfinado`).val();
        let eletricidade = $(`#ListaFuncaoAtividade_${indice}_Eletricidade`).val();
        let movimentacaoCarga = $(`#ListaFuncaoAtividade_${indice}_MovimentacaoCarga`).val();
        let radiacaoIonizante = $(`#ListaFuncaoAtividade_${indice}_RadiacaoIonizante`).val();
        let maquinasEquipamentos = $(`#ListaFuncaoAtividade_${indice}_MaquinasEquipamentos`).val();
        let inflamaveisCombustiveis = $(`#ListaFuncaoAtividade_${indice}_InflamaveisCombustiveis`).val();
        let trabalhoAquaviario = $(`#ListaFuncaoAtividade_${indice}_TrabalhoAquaviario`).val();

        adicionar_atualizar_atividade(indice, id, codigo, principal, nome, descricao, ativo,
            trabalhoAltura, espacoConfinado, eletricidade, movimentacaoCarga,
            radiacaoIonizante, maquinasEquipamentos, inflamaveisCombustiveis, trabalhoAquaviario);
    });
}

const adicionar_atualizar_atividade = (indice, id, codigo, principal, nome, descricao, ativo,
    trabalhoAltura, espacoConfinado, eletricidade, movimentacaoCarga,
    radiacaoIonizante, maquinasEquipamentos, inflamaveisCombustiveis, trabalhoAquaviario) => {
    let atividade = {
        indice: parseInt(indice),
        id: (!!id ? parseInt(id) : null),
        codigo,
        principal: (!!principal ? principal == 'true' : false),
        nome,
        descricao,
        ativo: (!!ativo ? ativo == 'true' : false),
        trabalhoAltura: (!!trabalhoAltura ? trabalhoAltura == 'true' : false),
        espacoConfinado: (!!espacoConfinado ? espacoConfinado == 'true' : false),
        eletricidade: (!!eletricidade ? eletricidade == 'true' : false),
        movimentacaoCarga: (!!movimentacaoCarga ? movimentacaoCarga == 'true' : false),
        radiacaoIonizante: (!!radiacaoIonizante ? radiacaoIonizante == 'true' : false),
        maquinasEquipamentos: (!!maquinasEquipamentos ? maquinasEquipamentos == 'true' : false),
        inflamaveisCombustiveis: (!!inflamaveisCombustiveis ? inflamaveisCombustiveis == 'true' : false),
        trabalhoAquaviario: (!!trabalhoAquaviario ? trabalhoAquaviario == 'true' : false),
    };

    //Verifica se o índice já está no array
    let indice_array = localizar_indice_array(indice);
    if (indice_array < 0)
        lista_atividades.push(atividade);
    else
        lista_atividades[indice_array] = atividade;
}

const carregar_grid_atividades = () => {
    $("#gridAtividades>tbody").html('');

    //ordena os dados
    let lista_ordenada = lista_atividades.slice().sort(function (a, b) {
        return (a.nome.localeCompare(b.nome));
    });

    lista_ordenada.forEach(function (item) {
        let row = `
            <tr data-id='${item.indice}'>
                <td>
                    <input type="hidden" name="ListaFuncaoAtividade.Index" value="${item.indice}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Id' name="ListaFuncaoAtividade[${item.indice}].Id" value="${item.id}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Codigo' name="ListaFuncaoAtividade[${item.indice}].Codigo" ${(!!item.codigo) ? 'value="' + item.codigo + '"' : ""} />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Principal' name="ListaFuncaoAtividade[${item.indice}].Principal" value="${item.principal}" />
                    ${item.codigo}
                </td>
                <td>
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Nome' name="ListaFuncaoAtividade[${item.indice}].Nome" ${(!!item.nome) ? 'value="' + item.nome + '"' : ""} />
                    ${item.nome.toUpperCase()}
                </td>
                <td>
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Descricao' name="ListaFuncaoAtividade[${item.indice}].Descricao" ${(!!item.descricao) ? 'value="' + item.descricao + '"' : ""} />
                    ${item.descricao}
                </td>
                <td>
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_TrabalhoAltura")' name="ListaFuncaoAtividade[${item.indice}].TrabalhoAltura" value="${item.trabalhoAltura}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_EspacoConfinado")' name="ListaFuncaoAtividade[${item.indice}].EspacoConfinado" value="${item.espacoConfinado}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Eletricidade")' name="ListaFuncaoAtividade[${item.indice}].Eletricidade" value="${item.eletricidade}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_MovimentacaoCarga")' name="ListaFuncaoAtividade[${item.indice}].MovimentacaoCarga" value="${item.movimentacaoCarga}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_RadiacaoIonizante")' name="ListaFuncaoAtividade[${item.indice}].RadiacaoIonizante" value="${item.radiacaoIonizante}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_MaquinasEquipamentos")' name="ListaFuncaoAtividade[${item.indice}].MaquinasEquipamentos" value="${item.maquinasEquipamentos}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_InflamaveisCombustiveis")' name="ListaFuncaoAtividade[${item.indice}].InflamaveisCombustiveis" value="${item.inflamaveisCombustiveis}" />
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_TrabalhoAquaviario")' name="ListaFuncaoAtividade[${item.indice}].TrabalhoAquaviario" value="${item.trabalhoAquaviario}" />
                </td>
                <td>
                    <input type="hidden" id='ListaFuncaoAtividade_${item.indice}_Ativo' name="ListaFuncaoAtividade[${item.indice}].Ativo" value="${item.ativo}" />
                    ${item.ativo ? 'Ativo' : 'Inativo'}
                </td>
                <td class="text-end">
                    <a href="javascript:editar_atividade(${item.indice})" title="Editar atividade"><i class="fas fa-pencil-alt"></i></a>`;

        if (!item.principal)
            row = row + `<a href="javascript:excluir_atividade(${item.indice})" title="Excluir atividade"><i class="fas fa-trash icone-exclusao"></i></a>`;

        row = row + `
                </td>
            </tr>
        `;

        $("#gridAtividades>tbody").append(row);
    });
}

function localizar_indice_array(indice) {
    let indice_array = lista_atividades.findIndex(function (atividade) {
        return atividade.indice === parseInt(indice);
    });

    return indice_array;
}

const limpar_formulario = () => {
    $(`#hidIndiceModal`).val(null);
    $(`#hidIdModal`).val(null);
    $(`#txtCodigoModal`).val(null);
    $(`#hidPrincipalModal`).val(null);
    $(`#txtNomeModal`).val(null);
    $(`#txtDescricaoModal`).val(null);
    $(`#ddlSituacaoModal option[value='true'`).attr('selected', 'selected');

    $(`#chkTrabalhoAlturaModal`).prop('checked', false);
    $(`#chkEspacoConfinadoModal`).prop('checked', false);
    $(`#chkEletricidadeModal`).prop('checked', false);
    $(`#chkMovimentacaoCargaModal`).prop('checked', false);
    $(`#chkRadiacaoIonizanteModal`).prop('checked', false);
    $(`#chkMaquinasEquipamentosModal`).prop('checked', false);
    $(`#chkInflamaveisCombustiveisModal`).prop('checked', false);
    $(`#chkTrabalhoAquaviarioModal`).prop('checked', false);

    $('#txtNomeModal').attr('disabled', false);
    $('#ddlSituacaoModal').attr('disabled', false);
}

const nova_atividade = () => {
    limpar_formulario();

    indice = Math.floor((Math.random() * 100000) + 1);

    $(`#hidIndiceModal`).val(indice);
    $(`#hidIdModal`).val(0);
    $(`#hidPrincipalModal`).val('false');

    $("#modalAtividade").modal('show');
}

const editar_atividade = (indice) => {
    //Verifica se o índice já está no array
    let indice_array = localizar_indice_array(indice);
    if (indice_array >= 0) {
        let atividade = lista_atividades[indice_array];

        $(`#hidIndiceModal`).val(atividade.indice);
        $(`#hidIdModal`).val(atividade.id);
        $(`#txtCodigoModal`).val(atividade.codigo);
        $(`#hidPrincipalModal`).val(atividade.principal);
        $(`#txtNomeModal`).val(atividade.nome);
        $(`#txtDescricaoModal`).val(atividade.descricao);
        $(`#ddlSituacaoModal option[value='${atividade.ativo}'`).attr('selected', 'selected');

        $(`#chkTrabalhoAlturaModal`).prop('checked', atividade.trabalhoAltura);
        $(`#chkEspacoConfinadoModal`).prop('checked', atividade.espacoConfinado);
        $(`#chkEletricidadeModal`).prop('checked', atividade.eletricidade);
        $(`#chkMovimentacaoCargaModal`).prop('checked', atividade.movimentacaoCarga);
        $(`#chkRadiacaoIonizanteModal`).prop('checked', atividade.radiacaoIonizante);
        $(`#chkMaquinasEquipamentosModal`).prop('checked', atividade.maquinasEquipamentos);
        $(`#chkInflamaveisCombustiveisModal`).prop('checked', atividade.inflamaveisCombustiveis);
        $(`#chkTrabalhoAquaviarioModal`).prop('checked', atividade.trabalhoAquaviario);

        if (atividade.principal) {
            $('#txtNomeModal').attr('disabled', true);
            $('#ddlSituacaoModal').attr('disabled', true);
        } else {
            $('#txtNomeModal').attr('disabled', false);
            $('#ddlSituacaoModal').attr('disabled', false);
        }

        $("#modalAtividade").modal('show');
    }
}

const excluir_atividade = (indice) => {
    //Verifica se o índice já está no array
    let indice_array = localizar_indice_array(indice);
    if (indice_array >= 0) {
        lista_atividades.splice(indice_array, 1);
        carregar_grid_atividades();
    }
}

const salvar_atividade = () => {
    let indice = $('#hidIndiceModal').val();
    let id = $('#hidIdModal').val();
    let codigo = $('#txtCodigoModal').val();
    let principal = $('#hidPrincipalModal').val();
    let nome = $(`#txtNomeModal`).val();
    let descricao = $(`#txtDescricaoModal`).val();
    let situacao = $(`#ddlSituacaoModal`).val();

    let trabalhoAltura = $(`#chkTrabalhoAlturaModal`).is(":checked");
    let espacoConfinado = $(`#chkEspacoConfinadoModal`).is(":checked");
    let eletricidade = $(`#chkEletricidadeModal`).is(":checked");
    let movimentacaoCarga = $(`#chkMovimentacaoCargaModal`).is(":checked");
    let radiacaoIonizante = $(`#chkRadiacaoIonizanteModal`).is(":checked");
    let maquinasEquipamentos = $(`#chkMaquinasEquipamentosModal`).is(":checked");
    let inflamaveisCombustiveis = $(`#chkInflamaveisCombustiveisModal`).is(":checked");
    let trabalhoAquaviario = $(`#chkTrabalhoAquaviarioModal`).is(":checked");

    adicionar_atualizar_atividade(indice, id, codigo, principal, nome, descricao, situacao,
        trabalhoAltura.toString(), espacoConfinado.toString(), eletricidade.toString(), movimentacaoCarga.toString(),
        radiacaoIonizante.toString(), maquinasEquipamentos.toString(), inflamaveisCombustiveis.toString(), trabalhoAquaviario.toString());
    carregar_grid_atividades();

    $("#modalAtividade").modal('hide');
}

$("#txtBuscaCBO").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/CBO/Autocomplete",
            type: "GET",
            dataType: "json",
            data: { Prefix: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.descricao, value: item.id, atividade: item.atividade, requisitos: item.requisitos };
                }))
            }
        })
    },
    select: function (event, ui) {
        event.preventDefault();
        $("#IdCBO").val(ui.item.value);
        $("#txtBuscaCBO").val(ui.item.label);
        $("#txtRequisitos").val(ui.item.requisitos);
        $("#txtAtividades").val(ui.item.atividade);
    },
    messages: {
        noResults: "", results: ""
    }
});

$("#txtBuscaCBO").blur(function () {
    if (!$(this).val()) {
        $("#IdCBO").val(null);
        $("#txtBuscaCBO").val(null);
        $("#txtRequisitos").val(null);
        $("#txtAtividades").val(null);
    }
});

$(document).ready(function () {
    criar_lista_atividades();
});
