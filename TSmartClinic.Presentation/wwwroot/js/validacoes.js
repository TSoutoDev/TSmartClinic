
document.addEventListener("DOMContentLoaded", function () {

    configurarValidacaoCPF();
    configurarValidacaoDataNascimento();

    if (typeof $ !== "undefined" && $.fn.mask) {
        configurarMascaras();
    }
});


// ============================================
// CPF
// ============================================

function validarCPF(cpf) {

    cpf = (cpf || "").replace(/\D/g, "");

    if (cpf.length !== 11) {
        return false;
    }

    // Impede CPFs com todos os números iguais
    if (/^(\d)\1{10}$/.test(cpf)) {
        return false;
    }

    let soma = 0;

    // Primeiro dígito verificador
    for (let i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }

    let resto = (soma * 10) % 11;

    if (resto === 10 || resto === 11) {
        resto = 0;
    }

    if (resto !== parseInt(cpf.charAt(9))) {
        return false;
    }

    soma = 0;

    // Segundo dígito verificador
    for (let i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }

    resto = (soma * 10) % 11;

    if (resto === 10 || resto === 11) {
        resto = 0;
    }

    return resto === parseInt(cpf.charAt(10));
}


function configurarValidacaoCPF() {

    const campoCPF = document.getElementById("CPF");

    if (!campoCPF) {
        return;
    }

    campoCPF.addEventListener("blur", function () {

        const cpf = campoCPF.value;

        // Deixa o FluentValidation tratar obrigatório
        if (!cpf) {
            limparValidacao(campoCPF);
            return;
        }

        if (!validarCPF(cpf)) {

            campoCPF.classList.add("is-invalid");
            campoCPF.classList.remove("is-valid");

        } else {

            campoCPF.classList.remove("is-invalid");
            campoCPF.classList.add("is-valid");
        }
    });

    campoCPF.addEventListener("input", function () {
        limparValidacao(campoCPF);
    });
}


// ============================================
// DATA DE NASCIMENTO
// ============================================

function configurarValidacaoDataNascimento() {

    const campoData = document.getElementById("DataNascimento");

    if (!campoData) {
        return;
    }

    // Impede selecionar data futura no próprio calendário
    const hoje = new Date();

    const hojeFormatado =
        hoje.getFullYear() + "-" +
        String(hoje.getMonth() + 1).padStart(2, "0") + "-" +
        String(hoje.getDate()).padStart(2, "0");

    campoData.setAttribute("max", hojeFormatado);


    campoData.addEventListener("change", function () {

        if (!campoData.value) {
            limparValidacao(campoData);
            return;
        }

        const nascimento = new Date(campoData.value + "T00:00:00");

        const dataAtual = new Date();
        dataAtual.setHours(0, 0, 0, 0);

        if (
            isNaN(nascimento.getTime()) ||
            nascimento > dataAtual
        ) {

            campoData.classList.add("is-invalid");
            campoData.classList.remove("is-valid");

        } else {

            campoData.classList.remove("is-invalid");
            campoData.classList.add("is-valid");
        }
    });
}


// ============================================
// MÁSCARAS
// ============================================

function configurarMascaras() {

    if ($("#CPF").length) {
        $("#CPF").mask("000.000.000-00");
    }

    if ($("#Telefone").length) {
        $("#Telefone").mask("(00) 00000-0000");
    }
}


// ============================================
// AUXILIAR
// ============================================

function limparValidacao(campo) {

    campo.classList.remove("is-invalid");
    campo.classList.remove("is-valid");
}
