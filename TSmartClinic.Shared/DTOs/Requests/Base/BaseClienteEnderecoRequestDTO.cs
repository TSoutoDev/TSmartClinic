using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseClienteEnderecoRequestDTO
    {
        public string? Tipo { get; set; }
        public BaseEnderecoRequestDTO? Endereco { get; set; }
    }
}
