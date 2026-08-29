async function buscarCep(inputCep) {

    const cep = (inputCep.value || '').replace(/\D/g, '');

    if (!cep) {
        limparEndereco(inputCep);
        return;
    }

    if (cep.length !== 8) {
        erroCep(
            inputCep,
            'Informe um CEP válido com 8 dígitos.'
        );
        return;
    }

    try {

        const responseViaCep = await axios.get(
            `https://viacep.com.br/ws/${cep}/json/`
        );

        const dados = responseViaCep.data;

        if (dados.erro) {
            erroCep(
                inputCep,
                'CEP não encontrado.'
            );
            return;
        }

        const prefixo = obterPrefixoEndereco(inputCep);

        preencherCampo(prefixo, 'Logradouro', dados.logradouro);
        preencherCampo(prefixo, 'Bairro', dados.bairro);
        preencherCampo(prefixo, 'Cidade', dados.localidade);
        preencherCampo(prefixo, 'Estado', dados.uf);

        if (dados.ibge) {
            await preencherMunicipioEstado(
                prefixo,
                dados.ibge
            );
        }

        const numero = document.querySelector(
            `[name="${prefixo}.Numero"]`
        );

        if (numero) {
            numero.focus();
        }

    }
    catch (error) {

        console.error(
            'Erro ao consultar CEP:',
            error
        );

        erroCep(
            inputCep,
            'Não foi possível consultar o CEP.'
        );
    }
}


async function preencherMunicipioEstado(prefixo, codigoIbge) {

    try {

        const response = await axios.get(
            `/api/municipios/ibge/${codigoIbge}`
        );

        const municipio = response.data;

        preencherCampo(
            prefixo,
            'MunicipioId',
            municipio.municipioId
        );

        preencherCampo(
            prefixo,
            'EstadoId',
            municipio.estadoId
        );

    }
    catch (error) {

        console.error(
            'Município não encontrado na base do sistema:',
            error
        );

        preencherCampo(prefixo, 'MunicipioId', '');
        preencherCampo(prefixo, 'EstadoId', '');
    }
}


function preencherCampo(prefixo, campo, valor) {

    const elemento = document.querySelector(
        `[name="${prefixo}.${campo}"]`
    );

    if (elemento) {
        elemento.value = valor ?? '';
    }
}


function obterPrefixoEndereco(inputCep) {

    return inputCep.name.replace('.Cep', '');
}


function limparEndereco(inputCep) {

    const prefixo = obterPrefixoEndereco(inputCep);

    preencherCampo(prefixo, 'Logradouro', '');
    preencherCampo(prefixo, 'Bairro', '');
    preencherCampo(prefixo, 'Cidade', '');
    preencherCampo(prefixo, 'Estado', '');
    preencherCampo(prefixo, 'EstadoId', '');
    preencherCampo(prefixo, 'MunicipioId', '');
}


/* =========================================================
   ALTERAÇÃO MANUAL DE CIDADE / ESTADO
   ========================================================= */

function configurarValidacaoManualEndereco() {

    const camposCidade = document.querySelectorAll(
        'input[name$=".Endereco.Cidade"]'
    );

    const camposEstado = document.querySelectorAll(
        'input[name$=".Endereco.Estado"]'
    );

    camposCidade.forEach(function (campo) {

        campo.addEventListener('input', function () {

            const prefixo = this.name.replace('.Cidade', '');

            preencherCampo(
                prefixo,
                'MunicipioId',
                ''
            );
        });

    });

    camposEstado.forEach(function (campo) {

        campo.addEventListener('input', function () {

            const prefixo = this.name.replace('.Estado', '');

            preencherCampo(
                prefixo,
                'EstadoId',
                ''
            );

            preencherCampo(
                prefixo,
                'MunicipioId',
                ''
            );
        });

    });
}


/* =========================================================
   MÁSCARA DO CEP
   ========================================================= */

function configurarMascaraCep() {

    const camposCep = document.querySelectorAll(
        'input[name$=".Endereco.Cep"]'
    );

    camposCep.forEach(function (campo) {

        // Aplica máscara também para valor carregado do banco
        aplicarMascaraCep(campo);

        campo.addEventListener('input', function () {
            aplicarMascaraCep(this);
        });

    });
}


function aplicarMascaraCep(campo) {

    let valor = (campo.value || '').replace(/\D/g, '');

    valor = valor.substring(0, 8);

    if (valor.length > 5) {

        valor =
            valor.substring(0, 5) +
            '-' +
            valor.substring(5);
    }

    campo.value = valor;
}


/* =========================================================
   ERRO DO CEP
   ========================================================= */

function erroCep(inputCep, mensagem) {

    limparEndereco(inputCep);

    // Limpa também o CEP digitado
    inputCep.value = '';

    setTimeout(function () {

        alert(mensagem);

        inputCep.focus();

    }, 50);
}

/* =========================================================
   INICIALIZAÇÃO
   ========================================================= */

document.addEventListener('DOMContentLoaded', function () {

    configurarValidacaoManualEndereco();
    configurarMascaraCep();

});