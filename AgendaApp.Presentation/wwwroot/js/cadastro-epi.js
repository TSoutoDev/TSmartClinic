document.addEventListener("DOMContentLoaded", function () {
    // --- Parte 1: Inclusão e Remoção de Linhas na Tabela ---

    // Array para armazenar os IDs das empresas já adicionadas (incluindo as carregadas do banco)
    let empresasNaTabela = Array.from(document.querySelectorAll('#empresasTabelaBody tr'))
        .map(row => row.id.replace('row-', ''));

    // Função que verifica se a empresa já foi adicionada
    function verificarDuplicacao(idEmpresa) {
        return empresasNaTabela.includes(idEmpresa);
    }

    // Evento para o botão de incluir empresa
    document.getElementById("incluirEmpresa").addEventListener("click", function () {
        let empresaInput = document.getElementById("EmpresaAutoComplete");
        let idEmpresa = document.getElementById("IdEmpresa").value;
        let empresaInfo = empresaInput.value.trim();

        if (!idEmpresa || !empresaInfo) {
            alert("Por favor, selecione uma empresa válida antes de incluir.");
            return;
        }

        // Verifica se a empresa já foi adicionada
        if (verificarDuplicacao(idEmpresa)) {
            alert("Esta empresa já foi adicionada.");
            return; // Não permite adicionar novamente
        }

        // Separando o CNPJ e o nome da empresa
        let [cnpjEmpresa, nomeEmpresa] = empresaInfo.split(" - ");
        if (!cnpjEmpresa || !nomeEmpresa) {
            alert("Informações inválidas da empresa.");
            return;
        }

        // Adiciona a empresa à lista de IDs já presentes na tabela
        empresasNaTabela.push(idEmpresa);

        // Cria uma nova linha para a tabela
        let tabelaBody = document.getElementById("empresasTabelaBody");
        let newRow = document.createElement("tr");
        newRow.id = `row-${idEmpresa}`;
        newRow.innerHTML = `
        <td>
            <input type="checkbox" class="select-row" data-empresa-id="${idEmpresa}" />
        </td>
            <td>
                <input type="hidden" name="EPIEmpresa.Index" value="${idEmpresa}" />
                <input type="hidden" name="EPIEmpresa[${idEmpresa}].IdEmpresa" value="${idEmpresa}" />
                <input type="hidden" name="EPIEmpresa[${idEmpresa}].Cnpj" value="${cnpjEmpresa}" />
                ${formatarCNPJ(cnpjEmpresa)}
            </td>
            <td>
                <input type="hidden" name="EPIEmpresa[${idEmpresa}].NomeFantasia" value="${nomeEmpresa}" />
                ${nomeEmpresa.toUpperCase()}
            </td>
            <td class="text-center">
                <a class="btn btn-danger btn-sm delete-btn" onclick="deleteRow(this)">
                    <i class="fas fa-trash"></i>
                </a>
            </td>
        `;
        tabelaBody.appendChild(newRow);

        // Limpa os campos após inclusão
        empresaInput.value = "";
        document.getElementById("IdEmpresa").value = "";
        document.getElementById("CnpjEmpresa").value = "";
    });

    // Função para formatar o CNPJ
    function formatarCNPJ(cnpj) {
        return cnpj?.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, "$1.$2.$3/$4-$5") ?? cnpj;
    }

    // Função global para remover uma linha (para que o onclick na linha funcione)
    window.deleteRow = function (element) {
        let row = element.closest("tr");
        let idEmpresa = row.id.replace('row-', '');

        // Remove o id da empresa da lista
        empresasNaTabela = empresasNaTabela.filter(id => id !== idEmpresa);

        // Remove a linha da tabela
        row.remove();
    };


    // --- Parte 2: Configuração do DataTable e Preparação dos Inputs Hidden para Submissão ---
    // Aqui usamos jQuery e DataTables (certifique-se de que essas bibliotecas estão carregadas)

    $(document).ready(function () {
        // Inicializa o DataTable
        var table = $('#gridResultadosEmpresa').DataTable({
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
        $('#gridResultadosEmpresa tbody').on('change', 'input[type="checkbox"]', function () {
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
