// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var possuiEmpresaAtiva = false;
var urlBase = window.location.origin.concat('/');
var modalInstance;

if (window.location.href.replace("https://", "").split("/")[1] !== "") {
    pathRoot = pathRoot.concat(window.location.href.replace("https://", "").split("/")[1], "/")
}

// Write your Javascript code.
const montarDatatable = () => {
    //Pega a referência do gridResultados
    gridResultados = $("#gridResultados");

    if (!!gridResultados) {
        gridResultados.DataTable({
            "ordering": true,
            "pageLength": 20,
            "lengthChange": false,
            "searching": false,
            "language": {
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "_MENU_ resultados por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "",
                "sSearch": "Filtrar registros",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                },
                "select": {
                    "rows": {
                        "_": "Selecionado %d linhas",
                        "0": "Nenhuma linha selecionada",
                        "1": "Selecionado 1 linha"
                    }
                }
            },
            bottomEnd: {
                paging: {
                    numbers: 3
                }
            }
        });
    }
}

var inputsEmpresa = document.querySelectorAll('#autocompleteempresamodal, #autocompleteempresa');

inputsEmpresa.forEach(input => {
    input.addEventListener('input', function () {
        var query = this.value;
        var winUrl = window.location;

        const urlGatewayAPI = obterUrlGateway();
        
        if (query.length < 3) {
            document.getElementById("sugestaoEmpresas").innerHTML = "";
            document.getElementById("sugestaoEmpresasModal").innerHTML = "";
            return;
        } else {
            var url = `${urlGatewayAPI}/empresas/autocomplete/${encodeURIComponent(query)}`;

            fetch(url, { method: "GET" })
                .then(res => res.json())
                .then(data => {
                    var sugestoes = input.nextElementSibling;

                    sugestoes.innerHTML = "";

                    data.forEach(empresa => {
                        var li = document.createElement('li');
                        var tipoDocumento = empresa.idTipoInscricao == 1 ? "CNPJ" : "CPF";
                        let dados = `<strong>${empresa.nomeFantasia.toUpperCase()}</strong><br />
                                     <strong>Grupo:</strong> ${empresa.nomeGrupo} | <strong>Região:</strong> ${empresa.nomeRegiao}<br /> 
                                     <strong>${tipoDocumento}:</strong> ${empresa.cnpjFormatado}`;

                        li.innerHTML = dados;
                        li.classList.id = empresa.id
                        li.classList.add('list-group-item');

                        li.addEventListener('click', function () {
                            document.getElementById('IdEmpresaAtiva').value = empresa.id;

                            const empresaAtiva = {
                                id: empresa.id,
                                razaoSocial: empresa.razaoSocial,
                                nomeFantasia: empresa.nomeFantasia,
                                cnpj: empresa.cnpj,
                                nomeGrupo: empresa.nomeGrupo,
                                nomeRegiao: empresa.nomeRegiao
                            };

                            salvarDadosCookie(empresaAtiva);
                            
                            carregarEmpresaAtiva();
                            possuiEmpresaAtiva = true;

                            fecharModal();

                            sugestoes.innerHTML = "";

                            document.getElementById("divNomeEmpresaAtiva").style.display = 'block';
                            document.getElementById("divAutocompleteEmpresa").style.display = 'none';

                            document.getElementById('autocompleteempresa').value = "";

                            window.location.href = "/Empresa/Central";
                        });

                        sugestoes.appendChild(li);
                    });
                });
        }
    })
});

const obterUrlGateway = () => {
    var url = urlBase.concat("gateway/ObterUrlGateway");
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, false);
    xhr.send();

    if (xhr.status === 200) {
        return xhr.responseText;
    }
};

const carregarEmpresaAtiva = () => {
    var url = urlBase.concat("empresaativa/ObterEmpresaAtiva");
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, false); 
    xhr.send();

    if (xhr.status === 200) {
        const empresaAtiva = JSON.parse(xhr.responseText);

        if (empresaAtiva && empresaAtiva.nomeFantasia != null) {
            document.getElementById("nomeEmpresaAtiva").textContent = empresaAtiva.nomeFantasia.toUpperCase();
            document.getElementById("nomeGrupoRegiaoEmpresaAtiva").textContent = `${empresaAtiva.nomeGrupo.toUpperCase()} (${empresaAtiva.nomeRegiao.toUpperCase()})`;
            document.getElementById('mensagemSemEmpresaAtiva').style.display = 'none';
            possuiEmpresaAtiva = true;
        } else {
            possuiEmpresaAtiva = false;
            abrirModal();
        }
    } else {
        console.error("Erro ao carregar a empresa ativa.");
    }

    controlarVisibilidadeItensMenu();
};

const fecharModal = () => {
    const modalElement = document.getElementById('modalTrocarEmpresa');
    let modal = bootstrap.Modal.getInstance(modalElement);

    if (!modal) {
        modal = new bootstrap.Modal(modalElement);
    }
    modal.hide();
}

const abrirModal = () => {
    const modalElement = document.getElementById('modalTrocarEmpresa');

    if (!modalInstance) {
        modalInstance = new bootstrap.Modal(modalElement);
    }

    modalInstance.show();
}

const controlarVisibilidadeItensMenu = () => {
    let menus = document.getElementsByName("menu-permissao-empresa-ativa")

    for (let i = 0; i < menus.length; i++) {
        menus[i].style.display = possuiEmpresaAtiva ? 'block' : 'none';
    }
}

const exibirAutoComplete = () => {
    document.getElementById("divNomeEmpresaAtiva").style.display = 'none';
    document.getElementById("divAutocompleteEmpresa").style.display = 'block';
}

const salvarDadosCookie = (dados) => {
    var url = urlBase.concat("empresaativa/GravarEmpresaAtiva"); 
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, false); 
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    xhr.send(JSON.stringify(dados));
};

$(document).ready(function () {
    montarDatatable();
    carregarEmpresaAtiva();
});