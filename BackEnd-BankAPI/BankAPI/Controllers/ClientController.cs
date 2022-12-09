using BankAPI.Data.BankModels;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService service)
        {
            _service = service;
        }

        // devuelve una coleccion, GET CLIENTES
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _service.GetAll();
        }

        // devueve un objeto, GET CLIENTE por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _service.GetById(id);
            if (client is null)
            {
                return ClientNotFound(id);
            }
            return client;
        }
        // Guardar cliente, POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            var newClient = await _service.Create(client);
            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
        }

        // Actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Client client)
        {
            if (id != client.Id) return BadRequest( new { message = $"El ID {id} de la URL no coincide con el ID {client.Id} del cuerpo de la solicitud" });

            var clientToUpdate = await _service.GetById(id);
            if (clientToUpdate is not null)
            {
                await _service.Update(id, clientToUpdate);
                return NoContent();
            } else
            {
                return ClientNotFound(id);
            }
        }

        // Eliminar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDelete = await _service.GetById(id);
            if (clientToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return ClientNotFound(id);
            }
        }

        private NotFoundObjectResult ClientNotFound(int id)
        {
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }
    }
}
