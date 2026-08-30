public class LoginClienteDto
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public string? NomeCliente { get; set; }
    public string? RazaoSocial { get; set; }
    public string? Cnpj { get; set; }
    public int? NichoId { get; set; }
}