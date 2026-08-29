using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class PacienteService : BaseService<Paciente>, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly IUsuarioClientePerfilService _usuarioClientePerfilService;

        public PacienteService(IUsuarioLogadoService usuarioLogadoService, IUsuarioClientePerfilService usuarioClientePerfilService, IPacienteRepository pacienteRepository) : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
            _usuarioLogadoService = usuarioLogadoService;
            _usuarioClientePerfilService = usuarioClientePerfilService;
        }

        public override Paciente Atualizar(Guid publicId, Paciente paciente)
        {
            var pacienteBanco = _pacienteRepository.ObterPorPublicId(publicId);

            if (pacienteBanco == null)
                throw new NotFoundException();

            pacienteBanco.Atualizar(paciente);

            if (paciente.PacienteEnderecos != null && paciente.PacienteEnderecos.Any())
            {
                var enderecoRecebido = paciente.PacienteEnderecos.FirstOrDefault();

                if (enderecoRecebido?.Endereco != null)
                {
                    var vinculoBanco = pacienteBanco.PacienteEnderecos?.FirstOrDefault();

                    if (vinculoBanco?.Endereco != null)
                    {
                        vinculoBanco.Tipo = enderecoRecebido.Tipo;

                        //metodo da entidade
                        vinculoBanco.Endereco.Atualizar(enderecoRecebido.Endereco);
                    }
                    else
                    {
                        pacienteBanco.PacienteEnderecos ??= new List<PacienteEndereco>();

                        pacienteBanco.PacienteEnderecos.Add(new PacienteEndereco
                        {
                            Tipo = enderecoRecebido.Tipo,
                            Endereco = enderecoRecebido.Endereco
                        });
                    }
                }
            }
            _pacienteRepository.Atualizar(pacienteBanco);

            return pacienteBanco;
        }

        public override Paciente Inserir(Paciente entity)
        {
            entity.DataCadastro = DateTime.Today;

            return base.Inserir(entity);
        }

        public override void Excluir(Guid publicId)
        {
            var paciente = _pacienteRepository.ObterPorPublicId(publicId);

            if (paciente == null)
                throw new NotFoundException();

            _pacienteRepository.ExcluirComEnderecos(paciente);
        }

        public async Task<List<Paciente>> BuscarPacientesHeader(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return new List<Paciente>();

            // Master pode pesquisar em todas as clínicas
            if (_usuarioLogadoService.UsuarioMaster)
            {
                return await _pacienteRepository.BuscarPacientesHeader(termo);
            }

            if (!_usuarioLogadoService.UsuarioLogadoId.HasValue)
                return new List<Paciente>();

            var usuarioId = _usuarioLogadoService.UsuarioLogadoId.Value;

            var clinicas = _usuarioClientePerfilService.ObterClinicasDoUsuario(usuarioId);

            var clienteIds = clinicas
                .Select(x => x.Id)
                .Distinct()
                .ToList();

            if (!clienteIds.Any())
                return new List<Paciente>();

            return await _pacienteRepository.BuscarPacientesHeader(termo, clienteIds);
        }
    }
}
