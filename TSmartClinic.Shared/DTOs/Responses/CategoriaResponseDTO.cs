﻿namespace TSmartClinic.Shared.DTOs.Responses
{
    public class CategoriaResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public bool? FlagSituacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
    }
}


