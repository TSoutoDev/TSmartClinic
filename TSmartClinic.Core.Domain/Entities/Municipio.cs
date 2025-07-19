namespace TSmartClinic.Core.Domain.Entities
{
    public class Municipio : Base
    {
        public string? NomeMunicipio { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public bool? Capital { get; set; }
        public int? Codigo_uf { get; set; }
        public string? Siafi_id { get; set; }
        public int? Ddd { get; set; }
        public string? Fuso_horario { get; set; }
    }
}
