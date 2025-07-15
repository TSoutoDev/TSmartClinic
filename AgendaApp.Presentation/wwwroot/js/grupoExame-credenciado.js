document.addEventListener('DOMContentLoaded', function () {


    // Adicionar novo exame
    document.getElementById('incluirGrupoExame').addEventListener('click', function () {
        let grupoExameInput = document.getElementById('grupoExameAutoComplete');
        let idGrupoExameHidden = document.getElementById('IdGrupoExame');
        let valorInput = document.getElementById('Valor');
        let prazoInput = document.getElementById('Prazo');

        if (idGrupoExameHidden.value) {
            let grupoExameNome = grupoExameInput.value;
            let valor = valorInput.value.trim() || '0,00';
            let prazo = prazoInput.value.trim() || '0';

            let valorFloat = parseBRL(valor);
            let prazoFloat = parseFloat(prazo.replace(/\,/g, '.'));

            if (valorFloat < 0) {
                showAlert("O valor padrão não pode ser negativo.");
            } else if (prazoFloat < 0) {
                showAlert("O prazo não pode ser negativo.");
            } else {
                addToTable(grupoExameNome, formatBRL(valorFloat), prazo, idGrupoExameHidden.value, false);
                valorInput.value = '';
                prazoInput.value = '';
                grupoExameInput.value = '';
                idGrupoExameHidden.value = '';
            }
        } else {
            showAlert("Por favor, selecione um exame.");
        }
    });


    // --- Parte 2: Configuração do DataTable e Preparação dos Inputs Hidden para Submissão ---
    // Aqui usamos jQuery e DataTables (certifique-se de que essas bibliotecas estão carregadas)

    $(document).ready(function () {
        // Inicializa o DataTable
        var table = $('#grupoExameTabela').DataTable({
            "ordering": true,
            "paging": false, // Desativa a paginação
            "lengthChange": false,
            "searching": false,
            "language": {
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                }
            }
        });



        // Selecionar ou desmarcar todas as linhas
        $('#select-all').on('click', function () {
            var rows = table.rows({ 'search': 'applied' }).nodes();
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        // Se o checkbox de uma linha for desmarcado, desmarcar o 'select-all'
        $('#grupoExameTabela tbody').on('change', 'input[type="checkbox"]', function () {
            if (!this.checked) {
                var el = $('#select-all').get(0);
                if (el && el.checked && ('indeterminate' in el)) {
                    el.indeterminate = true;
                }
            }
        });

        // Excluir linhas selecionadas
        $('#delete-selected').on('click', function () {
            var ids = [];
            table.$('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    ids.push($(this).data('id'));
                }
            });

            if (ids.length === 0) {
                alert('Nenhuma empresa selecionada.');
                return;
            }

            if (confirm('Tem certeza de que deseja excluir as empresas selecionadas?')) {
                // Remover as linhas da tabela
                ids.forEach(function (id) {
                    table.row($('#row-' + id)).remove().draw();
                });
            }
        });
    });
});

// Função para adicionar um novo exame na tabela
function addToTable(grupoExameNome, valor, prazo, id, fromDb) {
    let tabelaBody = document.getElementById('grupoExameTabelaBody');

    // Verificar se o exame já foi adicionado
    let rows = tabelaBody.getElementsByTagName('tr');
    for (let i = 0; i < rows.length; i++) {
        let cells = rows[i].getElementsByTagName('td');
        if (cells[0].textContent.trim() === grupoExameNome) {
            showAlert("Este exame já foi adicionado.");
            return;
        }
    }

    let tr = document.createElement('tr');
    tr.id = `row-${id}`;

    tr.innerHTML = `
        <td>
            <input type="checkbox" class="select-row" data-empresa-id="${id}" />
        </td>
        <td>
            <input type="hidden" name="CredenciadoGrupoExame.Index" value="${id}" />
            <input type='hidden' name='CredenciadoGrupoExame[${id}].IdGrupoExame' value='${id}'>
            <input type='hidden' name='CredenciadoGrupoExame[${id}].Nome' value='${grupoExameNome}'>${grupoExameNome}
        </td>
       
        <td><input type='hidden' name='CredenciadoGrupoExame[${id}].Valor' value='${valor}'>${valor}</td>
        <td><input type='hidden' name='CredenciadoGrupoExame[${id}].Prazo' value='${prazo}'>${prazo}</td>
        <td>
            <a type="button" class="btn btn-primary btn-sm" onclick="${fromDb ? `editRowBd(event, ${id})` : `editRowInput('row-${id}', 'Valor-${id}', 'Prazo-${id}')`}">
                <i class="fas fa-edit"></i>
            </a>
            <a type="button" class="btn btn-danger btn-sm ml-2" onclick="deleteRow(this)">
                <i class="fas fa-trash"></i>
            </a>
        </td>
    `;

    tabelaBody.appendChild(tr);
}

// Função para editar os dados carregados do banco
function editRowBd(event, id) {
    event.preventDefault(); // Evita o comportamento padrão do clique

    // Usando jQuery para acessar os elementos da linha
    let row = $(`#row-${id}`);
    let check = row.find('td').eq(0).text().trim();
    let nome = row.find('td').eq(1).text().trim();
    let valor = row.find('td').eq(2).text().trim();
    let prazo = row.find('td').eq(3).text().trim();

    // Preenche os campos do formulário com os dados da linha
    $('#IdGrupoExame').val(id);
    $('#grupoExameAutoComplete').val(nome);
    $('#Valor').val(valor);
    $('#Prazo').val(prazo);

    // Remove a linha da tabela para permitir edição
    row.remove();

    // Desativa os botões de edição e exclusão para evitar múltiplas edições
    $('.btn-primary, .btn-danger').addClass('disabled');

    // Atualiza o botão de inclusão para "Concluir Edição"
    $('#incluirGrupoExame').text('Concluir Edição').off('click').on('click', completeEditBd);
}


// Função para concluir a edição dos dados carregados do banco
function completeEditBd() {
    // Lógica para concluir a edição e reativar os botões. E define o texto do botão de ação de volta para "Inserir"
    reenableButtons();
    $('#incluirGrupoExame').text('Inserir');
    $('#incluirGrupoExame').attr('onclick');
}

// Função para reativar botões após edição
function reenableButtons() {
    $('.btn-primary, .btn-danger').removeClass('disabled');
}

// Função para deletar uma linha
function deleteRow(button) {
    let row = button.closest('tr');
    row.remove();
}

// Função para mostrar um alerta 
function showAlert(message) {
    let alertaHTML = `
        <div class="alert alert-warning alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    let mensagemAlertaDiv = document.getElementById("mensagemAlerta");
    mensagemAlertaDiv.innerHTML = alertaHTML;
    mensagemAlertaDiv.classList.remove("d-none");
}

// Função para converter valor monetário brasileiro para float
function parseBRL(value) {
    value = value.replace(/\./g, '').replace(',', '.');
    return parseFloat(value);
}

function formatBRL(value) {
    // Verifica se o valor é uma string e converte para número se necessário
    if (typeof value === 'string') {
        value = parseFloat(value.replace(',', '.'));
    }
    return `${value.toFixed(2).replace('.', ',')}`;
}
