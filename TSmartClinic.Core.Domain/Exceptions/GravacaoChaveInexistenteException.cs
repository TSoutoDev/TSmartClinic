namespace TSmartClinic.Core.Domain.Exceptions
{
    public class GravacaoChaveInexistenteException : TSmartClinicException
    {
        public GravacaoChaveInexistenteException() : base(400, "Não é possível gravar o registro pois está sendo associado a um registro (chave)")
        {
        }
        /// <summary>
        /// Tentativa de inserir/atualizar um registro que viola uma constraint UNIQUE.
        /// </summary>
        public class RegistroDuplicadoException : TSmartClinicException
        {
            public RegistroDuplicadoException()
                : base(400, "Não é possível gravar: já existe um registro com esses dados.") { }
        }

        /// <summary>
        /// Tentativa de gravar sem preencher um campo obrigatório (NOT NULL).
        /// </summary>
        public class CampoObrigatorioException : TSmartClinicException
        {
            public CampoObrigatorioException()
                : base(400, "Não é possível gravar: existe campo obrigatório não informado.") { }
        }
    }
}

