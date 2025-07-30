using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Controllers
{
    public class PacientesController : Controller
    {
        //public IActionResult PacientesCards()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult CardsPacientes()
        {
            return View();
           // return View(); // View de busca limpa e central
        }

        [HttpGet]
        public IActionResult CentralPaciente()
        {

            //MOC de DADOS
            // Simulação de um paciente vindo do banco
            var paciente = new PacienteViewModel
            {
                NomePaciente = "João da Silva",
                CPF = "123.456.789-00",
                DataNascimento = new DateTime(1990, 5, 12),
                Convenio = new Convenio
                {
                    NomeConvenio = "Unimed",
                },
                Telefone = "(11) 98765-4321",
                Email = "joao@email.com"
            };

            // Envolvendo no ResponseViewModel
            ResponseViewModel<PacienteViewModel> response = new ResponseViewModel<PacienteViewModel>
            {
                Sucesso = true,
                StatusCode = 200,
                Mensagem = "Paciente encontrado com sucesso.",
                Itens = new List<PacienteViewModel> { paciente }
            };

            return View(response);
         //   return View();
            // return View(); // View de busca limpa e central
        }

        [HttpPost]
        public IActionResult ResultadoBusca(string filtro)
        {
            // Aqui você faria a busca real no banco usando o filtro
            // Por enquanto, vamos redirecionar para a Central de Pacientes com filtro

            return RedirectToAction("Consulta", new { filtro });
        }


    }

}

