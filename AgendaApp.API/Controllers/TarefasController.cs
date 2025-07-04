using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Cadastro de tarefa!");
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Atualização de tarefa!");
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Exclusão de tarefa!");
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Consulta de tarefas!");
        }
    }
}
