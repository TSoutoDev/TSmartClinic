document.addEventListener('DOMContentLoaded', function () {

    const input = document.getElementById('pacienteSearch');
    const container = document.getElementById('pacienteSugestoes');

    if (!input || !container) {
        return;
    }

    let timeoutBusca = null;
    let indiceSelecionado = -1;

    input.addEventListener('input', function () {

        const termo = this.value.trim();

        clearTimeout(timeoutBusca);

        // Só começa a pesquisar com 2 caracteres
        if (termo.length < 2) {
            limparSugestoes();
            return;
        }

        // Aguarda 300ms para não chamar a API a cada tecla
        timeoutBusca = setTimeout(function () {
            buscarPacientes(termo);
        }, 300);
    });

    input.addEventListener('keydown', function (event) {

        const itens = container.querySelectorAll('.qp-result-item');

        if (!itens.length) {
            return;
        }

        // SETA PARA BAIXO
        if (event.key === 'ArrowDown') {

            event.preventDefault();

            indiceSelecionado++;

            if (indiceSelecionado >= itens.length) {
                indiceSelecionado = 0;
            }

            atualizarSelecao(itens);
        }

        // SETA PARA CIMA
        else if (event.key === 'ArrowUp') {

            event.preventDefault();

            indiceSelecionado--;

            if (indiceSelecionado < 0) {
                indiceSelecionado = itens.length - 1;
            }

            atualizarSelecao(itens);
        }

        // ENTER
        else if (event.key === 'Enter') {

            if (indiceSelecionado >= 0) {

                event.preventDefault();

                itens[indiceSelecionado].click();
            }
        }

        // ESC
        else if (event.key === 'Escape') {

            event.preventDefault();

            limparSugestoes();

            indiceSelecionado = -1;
        }
    });

    function buscarPacientes(termo) {

        $.ajax({
            url: '/Pacientes/BuscarHeader',
            type: 'GET',
            data: {
                termo: termo
            },
            success: function (resultado) {
                montarSugestoes(resultado);
            },
            error: function (xhr) {
                console.error(
                    'Erro ao buscar pacientes:',
                    xhr
                );

                limparSugestoes();
            }
        });
    }


    function montarSugestoes(pacientes) {

        indiceSelecionado = -1;
        container.innerHTML = '';

        if (!pacientes || pacientes.length === 0) {

            container.innerHTML = `
            <div class="qp-empty">
                <i class="bx bx-user-x"></i>
                <span>Nenhum paciente encontrado</span>
            </div>
        `;

            container.style.display = 'block';
            return;
        }

        pacientes.forEach(function (paciente) {

            const item = document.createElement('button');

            item.type = 'button';
            item.className = 'qp-result-item';

            const nome = paciente.nomePaciente || 'Paciente sem nome';
            const cpf = paciente.cpf || 'CPF não informado';
            const clinica = paciente.nomeClinica || 'Clínica não informada';

            item.innerHTML = `
            <div class="qp-result-avatar">
                ${obterIniciais(nome)}
            </div>

            <div class="qp-result-content">

                <div class="qp-result-name">
                    ${escaparHtml(nome)}
                </div>

                <div class="qp-result-info">
                    <span>
                        <i class="bx bx-id-card"></i>
                        ${escaparHtml(cpf)}
                    </span>

                    <span>
                        <i class="bx bx-building-house"></i>
                        ${escaparHtml(clinica)}
                    </span>
                </div>

            </div>

            <div class="qp-result-arrow">
                <i class="bx bx-chevron-right"></i>
            </div>
        `;

            item.addEventListener('click', function () {

                const clienteIdAtivo = parseInt(
                    document.getElementById('ClienteIdAtivo')?.value || '0'
                );

                if (paciente.clienteId === clienteIdAtivo) {
                    abrirPaciente(paciente.publicId);
                    return;
                }

                const modalElement = document.getElementById('modalConfirmarTrocaUnidade');

                if (!modalElement)
                    return;

                document.getElementById('trocaPacienteNome').textContent =
                    paciente.nomePaciente || 'Paciente';

                document.getElementById('trocaClinicaNome').textContent =
                    paciente.nomeClinica || 'Clínica';

                const modal = new bootstrap.Modal(modalElement);
                modal.show();

                const btnConfirmar = document.getElementById('btnConfirmarTrocaUnidade');

                btnConfirmar.onclick = function () {

                    const unidadesJson =
                        document.getElementById('UnidadesUsuario')?.textContent || '[]';

                    const unidadesUsuario = JSON.parse(unidadesJson);

                    const unidadeDestino = unidadesUsuario.find(function (unidade) {
                        return unidade.ClienteId === paciente.clienteId;
                    });

                    if (!unidadeDestino) {
                        modal.hide();
                        alert('Você não possui acesso a uma unidade vinculada a este paciente.');
                        return;
                    }

                    const returnUrl =
                        `/Pacientes/CentralPaciente?publicId=${encodeURIComponent(paciente.publicId)}`;

                    const form = document.createElement('form');

                    form.method = 'POST';
                    form.action = '/Account/TrocarUnidade';

                    const token =
                        document.querySelector('input[name="__RequestVerificationToken"]')?.value;

                    if (token) {
                        const antiForgery = document.createElement('input');

                        antiForgery.type = 'hidden';
                        antiForgery.name = '__RequestVerificationToken';
                        antiForgery.value = token;

                        form.appendChild(antiForgery);
                    }

                    const unidadeInput = document.createElement('input');

                    unidadeInput.type = 'hidden';
                    unidadeInput.name = 'unidadeId';
                    unidadeInput.value = unidadeDestino.Id;

                    form.appendChild(unidadeInput);

                    const returnUrlInput = document.createElement('input');

                    returnUrlInput.type = 'hidden';
                    returnUrlInput.name = 'returnUrl';
                    returnUrlInput.value = returnUrl;

                    form.appendChild(returnUrlInput);

                    document.body.appendChild(form);

                    modal.hide();
                    form.submit();
                };
            });

            container.appendChild(item);
        });

        container.style.display = 'block';
    }


    function abrirPaciente(publicId) {

        if (!publicId) {
            return;
        }

        window.location.href =
            `/Pacientes/CentralPaciente?publicId=${encodeURIComponent(publicId)}`;
    }


    function limparSugestoes() {

        indiceSelecionado = -1;
        container.innerHTML = '';
        container.style.display = 'none';
    }


    function escaparHtml(valor) {

        const div = document.createElement('div');

        div.textContent = valor || '';

        return div.innerHTML;
    }


    // Fecha as sugestões ao clicar fora
    document.addEventListener('click', function (event) {

        if (
            !input.contains(event.target) &&
            !container.contains(event.target)
        ) {
            limparSugestoes();
        }
    });

    function obterIniciais(nome) {

        return (nome || '')
            .trim()
            .split(/\s+/)
            .slice(0, 2)
            .map(function (parte) {
                return parte.charAt(0).toUpperCase();
            })
            .join('');
    }

    function atualizarSelecao(itens) {

        itens.forEach(function (item, index) {

            if (index === indiceSelecionado) {

                item.classList.add('qp-selected');

                item.scrollIntoView({
                    block: 'nearest'
                });

            } else {

                item.classList.remove('qp-selected');
            }
        });
    }

});