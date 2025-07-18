document.addEventListener('DOMContentLoaded', function () {


    // Adicionar novo exame
    document.getElementById('incluirPreparatorio').addEventListener('click', function () {
        let preparatorioInput = document.getElementById('preparatorioAutoComplete');
        let idPreparatorioHidden = document.getElementById('IdPreparatorio');


        if (idPreparatorioHidden.value) {
            let preparatorioNome = preparatorioInput.value.trim();

            addToTable(preparatorioNome, idPreparatorioHidden.value, false);

            preparatorioInput.value = '';
            idPreparatorioHidden.value = '';
        }
        else {
            showAlert("Por favor, selecione um exame.");
        }
    });
});

// Função para adicionar um novo exame na tabela
function addToTable(preparatorioNome, id, fromDb) {
    let tabelaBody = document.getElementById('preparatorioTabelaBody');

    // Verificar se o preparatorio já foi adicionado
    let rows = tabelaBody.getElementsByTagName('tr');
    for (let i = 0; i < rows.length; i++) {
        let cells = rows[i].getElementsByTagName('td');
        if (cells[0].textContent.trim() === preparatorioNome) {
            showAlert("Este preparatorio já foi adicionado.");
            return;
        }
    }

    let tr = document.createElement('tr');
    tr.id = `row-${id}`;
    tr.innerHTML = `
    <td>
        <input type="hidden" name="PreparatorioGrupoExame.Index" value="${id}" />
        <input type='hidden' name='PreparatorioGrupoExame[${id}].IdPreparatorio' value='${id}'>
        <input type='hidden' name='PreparatorioGrupoExame[${id}].Descricao' value='${preparatorioNome}'>${preparatorioNome}
    </td>
    <td width="10%" class="text-center">
        <a class="btn btn-danger btn-sm delete-btn" onclick="deleteRow(this)">
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
    let nome = row.find('td').eq(0).text().trim();


    // Preenche os campos do formulário com os dados da linha
    $('#IdPreparatorio').val(id);
    $('#preparatorioAutoComplete').val(nome);

    // Remove a linha da tabela para permitir edição
    row.remove();

    // Desativa os botões de edição e exclusão para evitar múltiplas edições
    $('.btn-primary, .btn-danger').addClass('disabled');

    // Atualiza o botão de inclusão para "Concluir Edição"
    $('#incluirPreparatorio').text('Concluir Edição').off('click').on('click', completeEditBd);
}


// Função para concluir a edição dos dados carregados do banco
function completeEditBd() {
    // Lógica para concluir a edição e reativar os botões. E define o texto do botão de ação de volta para "Inserir"
    reenableButtons();
    $('#incluirPreparatorio').text('Inserir');
    $('#incluirPreparatorio').attr('onclick', 'handleAction()');
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
