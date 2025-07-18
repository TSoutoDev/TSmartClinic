document.addEventListener('DOMContentLoaded', () => {
    const incluirBtn = document.getElementById('incluirGrupoExame');

    incluirBtn.addEventListener('click', (event) => {
        event.preventDefault();

        // Obtém os valores dos inputs e remove espaços em branco
        const nomeParametro = document.getElementById('NomeParametro').value.trim();
        const rawLimiteInferior = document.getElementById('LimiteInferior').value.trim();
        const rawLimiteSuperior = document.getElementById('LimiteSuperior').value.trim();
        const rawValorPadrao = document.getElementById('ValorPadrao').value.trim();

        // Obtém os valores dos selects
        const padrao = document.querySelector('select[name="Padrao"]').value;
        const masculinoFeminino = document.querySelector('select[name="MasculinoFeminino"]').value;
        const flagNormal = document.querySelector('select[name="FlagNormal"]').value;
        const pontoRanking = document.querySelector('select[name="PontoRanking"]').value;
        const ativoParametro = document.querySelector('select[name="novoParametroAtivo"]').value;

        clearErrorMessages();

        // Recupera o valor do campo TipoResultado ou assume "N" se não existir
        const tipoResultadoElem = document.querySelector('[name="TipoResultado"]');
        const tipoResultado = tipoResultadoElem ? tipoResultadoElem.value.trim() : "N";

        // Validação dos campos obrigatórios gerais
        if (!nomeParametro || !masculinoFeminino || !flagNormal || !ativoParametro) {
            if (!nomeParametro) {
                const nomeErrorElem = document.getElementById('NomeParametroError');
                if (nomeErrorElem) nomeErrorElem.innerText = "Nome é obrigatório.";
            }
            if (!masculinoFeminino) {
                const mfErrorElem = document.getElementById('MasculinoFemininoError');
                if (mfErrorElem) mfErrorElem.innerText = "Selecione Masculino ou Feminino.";
            }
            if (!flagNormal) {
                const flagErrorElem = document.getElementById('FlagNormalError');
                if (flagErrorElem) flagErrorElem.innerText = "Selecione Flag Normal.";
            }
            if (!ativoParametro) {
                const ativoErrorElem = document.getElementById('AtivoParametroError');
                if (ativoErrorElem) ativoErrorElem.innerText = "Selecione o status do parâmetro.";
            }
            return;
        }

        // Exibe valores para debug
        console.log("tipoResultado:", tipoResultado,
            "rawLimiteInferior:", rawLimiteInferior,
            "rawLimiteSuperior:", rawLimiteSuperior,
            "rawValorPadrao:", rawValorPadrao);

        // Se o tipo de resultado for "N", os campos devem estar preenchidos
        if (tipoResultado === "N" && (rawLimiteInferior === "" || rawLimiteSuperior === "" || rawValorPadrao === "")) {
            if (rawLimiteInferior === "") {
                const liErrorElem = document.getElementById('LimiteInferiorError');
                if (liErrorElem) liErrorElem.innerText = "Limite Inferior é obrigatório.";
            }
            if (rawLimiteSuperior === "") {
                const lsErrorElem = document.getElementById('LimiteSuperiorError');
                if (lsErrorElem) lsErrorElem.innerText = "Limite Superior é obrigatório.";
            }
            if (rawValorPadrao === "") {
                const vpErrorElem = document.getElementById('ValorPadraoError');
                if (vpErrorElem) vpErrorElem.innerText = "Valor Padrão é obrigatório.";
            }
            return;
        }

        // Converte os valores para números (ou null se estiverem vazios)
        const limiteInferior = parseBRL(rawLimiteInferior);
        const limiteSuperior = parseBRL(rawLimiteSuperior);
        const valorPadrao = parseBRL(rawValorPadrao);

        // Validação: Limite Inferior não pode ser maior que Limite Superior (quando informados)
        if (limiteInferior !== null && limiteSuperior !== null && limiteInferior > limiteSuperior) {
            const liErrorElem = document.getElementById('LimiteInferiorError');
            if (liErrorElem) liErrorElem.innerText = "O Limite Inferior não pode ser maior que o Limite Superior.";
            return;
        }

        // Verifica se a nova faixa cruza com alguma já existente na tabela
        const tableBody = document.getElementById('ExameParametroTabelaBody');
        const rows = tableBody ? tableBody.querySelectorAll('tr') : [];
        for (const row of rows) {
            const existingLowerElem = row.querySelector('input[name*="LimiteInferior"]');
            const existingUpperElem = row.querySelector('input[name*="LimiteSuperior"]');
            if (existingLowerElem && existingUpperElem) {
                const existingLowerNum = parseBRL(existingLowerElem.value);
                const existingUpperNum = parseBRL(existingUpperElem.value);
                if (tipoResultado === "N" && (limiteInferior <= existingUpperNum && limiteSuperior >= existingLowerNum)) {
                    alert("A faixa de valores inserida cruza com uma faixa já existente na lista.");
                    return;
                }
            }
        }

        // Adiciona a nova linha à tabela
        const index = tableBody ? tableBody.querySelectorAll('tr').length : 0;
        const newRow = document.createElement('tr');

        const masculinoFemininoFormatado = (masculinoFeminino === 'M' ? 'Masculino' : (masculinoFeminino === 'F' ? 'Feminino' : 'Ambos'));
        const flagNormalFormatado = (flagNormal === 'true' ? 'Normal' : 'Alterado');
        const padraoFormatado = (padrao === '1' ? 'Sim' : 'Não');
        const ativoParametroFormatado = (ativoParametro === 'true' ? 'Ativo' : 'Inativo');

        newRow.innerHTML = `
      <td>
        <input type="hidden" name="ListaExameParametro.Index" value="${index}" />
        <input type="hidden" name="ListaExameParametro[${index}].NomeParametro" value="${nomeParametro}" />
        ${nomeParametro}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].LimiteInferior" value="${formatBRL(limiteInferior)}" />
        ${formatBRL(limiteInferior)}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].LimiteSuperior" value="${formatBRL(limiteSuperior)}" />
        ${formatBRL(limiteSuperior)}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].ValorPadrao" value="${formatBRL(valorPadrao)}" />
        ${formatBRL(valorPadrao)}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].Padrao" value="${padrao === '' ? '0' : padrao}" />
        ${padraoFormatado}
     </td>

      <td>
        <input type="hidden" name="ListaExameParametro[${index}].MasculinoFeminino" value="${masculinoFeminino}" />
        ${masculinoFemininoFormatado}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].FlagNormal" value="${flagNormal}" />
        ${flagNormalFormatado}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].PontoRanking" value="${pontoRanking}" />
        ${pontoRanking}
      </td>
      <td>
        <input type="hidden" name="ListaExameParametro[${index}].ParametroAtivo" value="${ativoParametro === 'true' ? 'True' : 'False'}" />
        ${ativoParametroFormatado}
      </td>
      <td class="text-center">
        <a class="btn btn-danger btn-sm delete-btn" onclick="deleteRow(this)">
          <i class="fas fa-trash"></i>
        </a>
      </td>
    `;

        if (tableBody) {
            tableBody.appendChild(newRow);
        }

        // Limpa os campos de input e reseta os selects
        document.getElementById('NomeParametro').value = '';
        document.getElementById('LimiteInferior').value = '';
        document.getElementById('LimiteSuperior').value = '';
        document.getElementById('ValorPadrao').value = '';
        document.querySelector('select[name="Padrao"]').selectedIndex = 0;
        document.querySelector('select[name="MasculinoFeminino"]').selectedIndex = 0;
        document.querySelector('select[name="FlagNormal"]').selectedIndex = 0;
        document.querySelector('select[name="PontoRanking"]').selectedIndex = 0;
        document.querySelector('select[name="novoParametroAtivo"]').selectedIndex = 0;
    });

    // Função para excluir uma linha da tabela
    window.deleteRow = (button) => {
        const row = button.closest('tr');
        if (row) row.remove();
    };

    // Função para converter valor do formato BRL para número.
    // Se o valor estiver vazio, retorna null para que permaneça em branco.
    const parseBRL = (value) => {
        if (!value) return null;
        const normalized = value.replace(/\./g, '').replace(',', '.');
        const parsed = parseFloat(normalized);
        return isNaN(parsed) ? null : parsed;
    };

    // Função para formatar valor para o padrão brasileiro.
    // Se o valor for null ou inválido, retorna uma string vazia.
    const formatBRL = (value) => {
        if (value === null || isNaN(value)) return '';
        return value.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    };

    // Função para limpar as mensagens de erro
    const clearErrorMessages = () => {
        const errorIds = [
            'NomeParametroError',
            'LimiteInferiorError',
            'LimiteSuperiorError',
            'ValorPadraoError',
            'PadraoError',
            'MasculinoFemininoError',
            'FlagNormalError',
            'PontoRankingError',
            'AtivoParametroError'
        ];
        errorIds.forEach((id) => {
            const elem = document.getElementById(id);
            if (elem) elem.innerText = '';
        });
    };


});
