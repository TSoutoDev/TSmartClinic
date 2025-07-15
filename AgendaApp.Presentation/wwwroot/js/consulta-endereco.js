/*const urlGatewayAPI = "http://localhost:5047";*/
 
buscarCep = (valor) => {
    var cep = valor.replace(/\D/g, '');

    if (cep != "") {
        var validacep = /^[0-9]{8}$/;

        if (validacep.test(cep)) {
            axios.get(`https://viacep.com.br/ws/${cep}/json/`)
                .then(function (response) {
                    preencherCampos(response.data);
                })
                .catch(function (error) {
                    //console.error(error);
                });
        }
        else {
            limparCampos();
            alert("Formato de CEP inválido.");
        }
    }
    else {
        limparCampos();
    }
};

preencherCampos = async (conteudo) => {
    if (!("erro" in conteudo)) {
        document.getElementById('Rua').value = conteudo.logradouro;
        document.getElementById('Bairro').value = conteudo.bairro;
        document.getElementById('Cidade').value = conteudo.localidade;
        document.getElementById('IdUf').value = retornarIdUF(conteudo.uf);
        document.getElementById('CodigoLogradouro').value = retornarTipoLogradouro(conteudo.logradouro);
        document.getElementById('IdMunicipio').value = await retornarIdMunicipio(conteudo.localidade);

    }
    else {
        limparCampos();
        alert("CEP não encontrado.");
    }
}

retornarTipoLogradouro = (logradouro) => {
    let tipoLogradouro = logradouro.split(" ")[0];
    let tipoLogradouroElement = document.getElementById('CodigoLogradouro');
    let tipoLogradouroSelecionado = Array.from(tipoLogradouroElement.options).find(option => option.text == tipoLogradouro);

    return tipoLogradouroSelecionado ? tipoLogradouroSelecionado.value : 'R';
}

retornarIdUF = (uf) => {
    let ufElement = document.getElementById('IdUf');
    let UfSelecionado = Array.from(ufElement.options).find(option => option.text == uf);

    return UfSelecionado ? UfSelecionado.value : ' ';
}

retornarIdMunicipio = async (cidade) => {
    let idUf = document.getElementById('IdUf').value;
    var municipios = await this.buscarMunicipios(idUf, cidade);

    var municipioSelecionado = Array.from(municipios).find(m => m.nome == cidade);

    return municipioSelecionado.id;
}

limparCampos = () => {
    document.getElementById('Rua').value = "";
    document.getElementById('Bairro').value = "";
    document.getElementById('IdUf').value = "";
    document.getElementById('CodigoLogradouro').value = "";
}

buscarMunicipios = async (idUf, query) => {
    const response = await fetch(`${urlGatewayAPI}/municipios?iduf=${idUf}&query=${encodeURIComponent(query)}`);
    const data = await response.json();
    return data.filter(m => m.nome == query);
}

var inputMunicipio = document.getElementById('empresa');
var autocompleteContainer = document.getElementById('sugestoes');
var idUf = document.getElementById('IdUf').value;

inputMunicipio.addEventListener('input', function () {
    var query = this.value;
    var idUf = document.getElementById('IdUf').value;

    if (query.length < 1 || !idUf) {
        document.getElementById("sugestoes").innerHTML = "";
        return;
    } else {
        fetch(`${urlGatewayAPI}/municipios?iduf=${idUf}&query=${encodeURIComponent(query)}`)
            .then(res => res.json())
            .then(data => {
                var sugestoes = document.getElementById("sugestoes");
                sugestoes.innerHTML = "";

                data.forEach(estado => {
                    var li = document.createElement('li');
                    li.textContent = estado.nome;
                    li.classList.id = estado.id;
                    li.classList.add('list-group-item');

                    li.addEventListener('click', function () {
                        document.getElementById('IdMunicipio').value = estado.id;
                        document.getElementById('Cidade').value = estado.nome;

                        sugestoes.innerHTML = "";
                    });
                    sugestoes.appendChild(li);
                });
            });
    }
})