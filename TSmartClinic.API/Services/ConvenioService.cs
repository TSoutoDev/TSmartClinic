using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class ConvenioService : BaseService<Convenio>, IConvenioService
    {
        private readonly IConvenioRepository _convenioRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public ConvenioService(IUsuarioLogadoService usuarioLogadoService, IConvenioRepository convenioRepository) : base(convenioRepository)
        {
            _convenioRepository = convenioRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public override Convenio Inserir(Convenio entity)
        {
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                if (!_usuarioLogadoService.ClienteId.HasValue)
                    throw new UnauthorizedAccessException("Clínica do usuário não identificada.");

                entity.ClienteId = _usuarioLogadoService.ClienteId.Value;
            }

            entity.DataCadastro = DateTime.Today;

            return base.Inserir(entity);
        }

        public override Convenio Atualizar(Guid publicId, Convenio entity)
        {
            var convenioExistente = _convenioRepository.ObterPorPublicId(publicId);

            if (convenioExistente == null)
                throw new NotFoundException();

            if (!_usuarioLogadoService.UsuarioMaster)
            {
                if (!_usuarioLogadoService.ClienteId.HasValue)
                    throw new UnauthorizedAccessException("Clínica do usuário não identificada.");

                entity.ClienteId = _usuarioLogadoService.ClienteId.Value;
            }

            convenioExistente.Atualizar(entity);

            return _convenioRepository.Atualizar(convenioExistente);
        }
    }
}