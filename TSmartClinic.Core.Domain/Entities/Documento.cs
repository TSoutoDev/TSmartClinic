using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Entities
{
    public class Documento : Base
    {
        #region Propriedades
        public string? Nome { get; set; }
        public string? TextoHtml { get; set; }
        public bool? FlagSituacao { get; set; }
        public string? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public string? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        public Guid TipoDocumentoId { get; set; }
        #endregion

        #region Relacionamentos
        public TipoDocumento? TipoDocumento { get; set; }
        #endregion
    }
}
