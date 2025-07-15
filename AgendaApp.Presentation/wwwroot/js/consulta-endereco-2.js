//const urlGatewayAPI = "http://localhost:5047";

buscarCep = (valor) => {
    var cep = valor.replace(/\D/g, '');

    if (cep !== "") {
        var validacep = /^[0-9]{8}$/;

        if (validacep.test(cep)) {
            axios.get(`https://viacep.com.br/ws/${cep}/json/`)
                .then(function (response) {
                    preencherCampos(response.data);
                })
                .catch(function (error) {
                    console.error(error);
                });
        } else {
            limparCampos();
            alert("Formato de CEP inválido.");
        }
    } else {
        limparCampos();
    }
};

preencherCampos = async (conteudo) => {
    if (!("erro" in conteudo)) {
        let ruaElement = document.getElementById('Endereco_Rua');
        let bairroElement = document.getElementById('Endereco_Bairro');
        let cidadeElement = document.getElementById('Endereco_Cidade');
        let ufElement = document.getElementById('Endereco_IdUf');
        let codigoLogradouroElement = document.getElementById('Endereco_CodigoLogradouro');
        let idMunicipioElement = document.getElementById('Endereco_IdMunicipio');

        if (ruaElement && bairroElement && cidadeElement && ufElement && codigoLogradouroElement && idMunicipioElement) {
            ruaElement.value = conteudo.logradouro;
            bairroElement.value = conteudo.bairro;
            cidadeElement.value = conteudo.localidade;
            ufElement.value = retornarIdUF(conteudo.uf);
            codigoLogradouroElement.value = retornarTipoLogradouro(conteudo.logradouro);
            idMunicipioElement.value = await retornarIdMunicipio(conteudo.localidade);
        } else {
            console.error('Alguns elementos não foram encontrados.');
        }
    } else {
        limparCampos();
        alert("CEP não encontrado.");
    }
};

retornarTipoLogradouro = (logradouro) => {
    let tipoLogradouro = logradouro.split(" ")[0];
    let tipoLogradouroElement = document.getElementById('Endereco_CodigoLogradouro');
    let tipoLogradouroSelecionado = Array.from(tipoLogradouroElement.options).find(option => option.text === tipoLogradouro);

    return tipoLogradouroSelecionado ? tipoLogradouroSelecionado.value : 'R';
};

retornarIdUF = (uf) => {
    let ufElement = document.getElementById('Endereco_IdUf');
    let ufSelecionado = Array.from(ufElement.options).find(option => option.text === uf);

    return ufSelecionado ? ufSelecionado.value : ' ';
};

retornarIdMunicipio = async (cidade) => {
    let idUf = document.getElementById('Endereco_IdUf').value;
    var municipios = await buscarMunicipios(idUf, cidade);

    var municipioSelecionado = municipios.find(m => m.nome === cidade);

    return municipioSelecionado ? municipioSelecionado.id : '';
};

limparCampos = () => {
    let ruaElement = document.getElementById('Endereco_Rua');
    let bairroElement = document.getElementById('Endereco_Bairro');
    let ufElement = document.getElementById('Endereco_IdUf');
    let codigoLogradouroElement = document.getElementById('Endereco_CodigoLogradouro');

    if (ruaElement && bairroElement && ufElement && codigoLogradouroElement) {
        ruaElement.value = "";
        bairroElement.value = "";
        ufElement.value = "";
        codigoLogradouroElement.value = "";
    } else {
        console.error('Alguns elementos não foram encontrados.');
    }
};

buscarMunicipios = async (idUf, query) => {
    const response = await fetch(`${urlGatewayAPI}/municipios?iduf=${idUf}&query=${encodeURIComponent(query)}`);
    const data = await response.json();
    return data;
};

var inputMunicipio = document.getElementById('Endereco_Cidade');
var autocompleteContainer = document.getElementById('sugestoes');

inputMunicipio.addEventListener('input', function () {
    var query = this.value;
    var idUf = document.getElementById('Endereco_IdUf').value;

    if (query.length < 1 || !idUf) {
        autocompleteContainer.innerHTML = "";
        return;
    } else {
        fetch(`${urlGatewayAPI}/municipios?iduf=${idUf}&query=${encodeURIComponent(query)}`)
            .then(res => res.json())
            .then(data => {
                autocompleteContainer.innerHTML = "";

                data.forEach(municipio => {
                    var li = document.createElement('li');
                    li.textContent = municipio.nome;
                    li.id = municipio.id;
                    li.classList.add('list-group-item');
                    li.setAttribute('tabindex', '0');

                    li.addEventListener('click', function () {
                        const idMunicipioElement = document.getElementById('Endereco_IdMunicipio');
                        const cidadeElement = document.getElementById('Endereco_Cidade');

                        if (idMunicipioElement && cidadeElement) {
                            idMunicipioElement.value = municipio.id;
                            cidadeElement.value = municipio.nome;
                            autocompleteContainer.innerHTML = "";
                        } else {
                            console.error('Elementos não encontrados. IDs podem estar incorretos.');
                        }
                    });

                    autocompleteContainer.appendChild(li);
                });
            });
    }
});
