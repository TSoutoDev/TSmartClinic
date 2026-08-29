document.addEventListener('DOMContentLoaded', function () {

    const cpf = document.getElementById('CPF');
    const cnpj = document.getElementById('CNPJ');

    if (cpf) {
        aplicarMascaraCpf(cpf);

        cpf.addEventListener('input', function () {
            aplicarMascaraCpf(this);
        });
    }

    if (cnpj) {
        aplicarMascaraCnpj(cnpj);

        cnpj.addEventListener('input', function () {
            aplicarMascaraCnpj(this);
        });
    }
});


function aplicarMascaraCpf(campo) {
    let valor = (campo.value || '')
        .replace(/\D/g, '')
        .substring(0, 11);

    if (valor.length > 3) {
        valor = valor.substring(0, 3) + '.' + valor.substring(3);
    }

    if (valor.length > 7) {
        valor = valor.substring(0, 7) + '.' + valor.substring(7);
    }

    if (valor.length > 11) {
        valor = valor.substring(0, 11) + '-' + valor.substring(11);
    }

    campo.value = valor;
}


function aplicarMascaraCnpj(campo) {
    let valor = (campo.value || '')
        .replace(/\D/g, '')
        .substring(0, 14);

    if (valor.length > 2) {
        valor = valor.substring(0, 2) + '.' + valor.substring(2);
    }

    if (valor.length > 6) {
        valor = valor.substring(0, 6) + '.' + valor.substring(6);
    }

    if (valor.length > 10) {
        valor = valor.substring(0, 10) + '/' + valor.substring(10);
    }

    if (valor.length > 15) {
        valor = valor.substring(0, 15) + '-' + valor.substring(15);
    }

    campo.value = valor;
}