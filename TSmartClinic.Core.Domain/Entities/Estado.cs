namespace TSmartClinic.Core.Domain.Entities
{
    public class Estado : Base
    {
        public string? Uf { get; set; }
        public string? NomeEstado { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string? Regiao { get; set; }
        public virtual ICollection<Municipio>? Municipios { get; set; }
    }
}
