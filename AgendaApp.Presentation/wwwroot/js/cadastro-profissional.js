// Script para controlar a exibição com base no checkbox
document.addEventListener("DOMContentLoaded", function () {
    var switchExterno = document.getElementById("Externo");
    var ofensor = document.getElementById("ofensor");
    var campoClinica = document.getElementById("campoClinica");
    var checkboxOfensor = document.getElementById("Ofensor"); 
    var inputClinica = document.getElementById("Clinica");

    // Função para alternar a visibilidade dos campos com base no estado do switch
    function toggleCamposExtras() {
        if (switchExterno.checked) {
            ofensor.classList.remove("d-none");  
            campoClinica.classList.remove("d-none");  
        } else {
            ofensor.classList.add("d-none");  
            campoClinica.classList.add("d-none");  

            // Limpa os valores quando o switch for desativado
            checkboxOfensor.checked = false;
            inputClinica.value = "";
        }
    }

    // Verifica o estado inicial e aplica a visibilidade correta
    toggleCamposExtras();

    // Adiciona o evento para alternar a exibição ao clicar no switch
    switchExterno.addEventListener("change", toggleCamposExtras);
});


// Script para exibir um alerta na inclusão/alteração quando o CPF não for preenchido
function confirmarGravacaoSemCPF(event) {
    const cpfInput = document.getElementById('Cpf');

    // Verifica se o botão que acionou o submit é o botão de exclusão
    if (event.submitter && event.submitter.id === "btnExcluir") {
        return true; // Permite o envio sem validação
    }

    if (!cpfInput.value.trim()) {
        let confirmacaoSemCPF = confirm("Atenção, o CPF é enviado para o eSocial nos eventos S-2220 e S-2240. Deseja incluir o profissional sem CPF?");

        // Se o usuário clicar em "Cancelar", impede a submissão do formulário
        if (!confirmacaoSemCPF) {
            event.preventDefault();
            return false;
        }
    }
    return true;
}

// Adiciona o evento ao formulário ao carregar a página
document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    if (form) {
        form.addEventListener("submit", confirmarGravacaoSemCPF);
    }
});


//script para modal da certificado digital
$(document).ready(function () {
    // Ao clicar no botão "Incluir", verifica se um arquivo foi selecionado e abre o modal
    $("#incluirCertificado").on("click", function () {
        var fileInput = $("#certificadoFile");
        if (fileInput.get(0).files.length > 0) {
            $("#senhaModal").modal("show");
        } else {
            alert("Selecione um arquivo .pfx antes de continuar.");
        }
    });

    // Ao clicar em "Salvar" no modal, armazena a senha no campo hidden e envia o formulário
    $("#salvarSenha").on("click", function () {
        var senha = $("#certSenha").val();
        $("#CertificadoDigitalSenha").val(senha);
        $("#senhaModal").modal("hide");
        $("#formCertificado").submit();
    });
});


//Ao clicar em "Excluir" os dados do certificado serão removidos 
document.getElementById("excluirCertificado").addEventListener("click", function () {
    // Limpa o campo de arquivo
    document.getElementById("certificadoFile").value = "";

    // Limpa o valor do campo hidden
    var hiddenInput = document.getElementById("CertificadoDigitalHidden");
    if (hiddenInput) {
        hiddenInput.value = "";
    }

    // Oculta a informação do certificado
    var certificadoInfo = document.getElementById("certificadoInfo");
    if (certificadoInfo) {
        certificadoInfo.style.display = "none";
    }

    // Exibe uma mensagem ou atualiza a interface para indicar que foi excluído
    var mensagemErro = document.getElementById("mensagemErro");
    mensagemErro.textContent = "Certificado digital removido. É necessário gravar para validar a alteração.";
    mensagemErro.style.display = "block";
});


//Validar o certificado digital enviado por meio do formulário, utilizando a senha informada pelo usuário para a validação
document.getElementById("salvarSenha").addEventListener("click", async function () {
    const senhaDigitada = document.getElementById("certSenha").value;
    const arquivoCertificado = document.getElementById("certificadoFile").files[0];
    const mensagemErro = document.getElementById("mensagemErro");

    if (!arquivoCertificado) {
        mensagemErro.textContent = "Por favor, selecione um certificado.";
        mensagemErro.style.display = "block";
        return;
    }

    try {
        // Ler o arquivo do certificado
        const arrayBuffer = await arquivoCertificado.arrayBuffer();
        const certificado = new Uint8Array(arrayBuffer);

        // Tenta validar o certificado com a senha
        if (await validarCertificado(certificado, senhaDigitada)) {
           // alert("Certificado validado com sucesso!");
            // Limpar o campo de senha após validação bem-sucedida
            document.getElementById("certSenha").value = "";
            mensagemErro.style.display = "none";
            const modal = new bootstrap.Modal(document.getElementById("senhaModal"));
            modal.hide();
        } else {
            throw new Error("Senha incorreta");
        }
    } catch (e) {
        mensagemErro.textContent = "Erro ao validar o certificado: " + e.message;
        mensagemErro.style.display = "block";
        document.getElementById("certSenha").value = "";
        document.getElementById("certSenha").focus();
    }
});

// Função para validar o certificado através de senha
async function validarCertificado(certificado, senha) {
    try {
        // Converte o Uint8Array para um formato binário adequado
        const pem = forge.util.binary.raw.encode(certificado); 

        // Carrega o certificado PFX com a senha convertendo o formato PEM binário para um objeto ASN.1 (uma estrutura de dados usada para representar certificados X.509)
        const p12Asn1 = forge.asn1.fromDer(pem);
        const p12 = forge.pkcs12.pkcs12FromAsn1(p12Asn1, senha); //tenta decodificar o certificado usando a senha fornecida.

        if (p12) {
           // console.log("Certificado carregado com sucesso!");
            return true;
        } else {
           // console.error("Certificado inválido ou senha incorreta.");
            return false;
        }
    } catch (error) {
       // console.error("Erro ao validar o certificado:", error);
        return false;
    }
}




