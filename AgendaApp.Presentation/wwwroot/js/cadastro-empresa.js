$("#numeroInscricao").blur(function () {
    let numeroInscricao = $(this).val();
    let numeroDigitos = numeroInscricao.length;

    switch (numeroDigitos) {
        case 11:
            $("#idTipoInscricao").val('2');
            break;
        case 14:
            $("#idTipoInscricao").val('1');
            break;
        default:
            $("#idTipoInscricao").val(null);
            break;
        }
});