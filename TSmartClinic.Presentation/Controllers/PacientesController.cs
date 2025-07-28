using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.Presentation.Controllers
{
    public class PacientesController : Controller
    {
        //public IActionResult PacientesCards()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult PacientesCards()
        {
            var paciente = new PacienteViewModel
            {
                Nome = "ANDERSON TESTE 140622",
                CPF = "459.581.520-41",
                Matricula = "202021",
                DataNascimento = new DateTime(1990, 1, 14),
                Empresa = "PEOPLENET",
                Setor = "AUDITORIA 33",
                Funcao = "Supervisor de Suporte"
            };

            return View(paciente);
           // return View(); // View de busca limpa e central
        }

        [HttpPost]
        public IActionResult ResultadoBusca(string filtro)
        {
            // Aqui você faria a busca real no banco usando o filtro
            // Por enquanto, vamos redirecionar para a Central de Pacientes com filtro

            return RedirectToAction("Consulta", new { filtro });
        }

        public class PacienteViewModel
        {
            public string Nome { get; set; }
            public string CPF { get; set; }
            public string Matricula { get; set; }
            public DateTime DataNascimento { get; set; }
            public string Empresa { get; set; }
            public string Setor { get; set; }
            public string Funcao { get; set; }
        }

    }

}

