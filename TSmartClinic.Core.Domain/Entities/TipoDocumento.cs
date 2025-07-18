using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Entities
{
    public class TipoDocumento : Base
    {
        #region Propriedades
        public string? Nome { get; set; }
        public bool? FlagSituacao { get; set; }
        public string? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public string? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        #endregion

        #region Relacionamentos
        public List<Documento>? Documentos { get; set; }
        #endregion
    }
}
