using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class PacienteService : BaseService<Paciente>, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository) : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public override Paciente Atualizar(int id, Paciente paciente)
        {
            var pacienteBanco = _pacienteRepository.ObterPorId(id);

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
    }
}
