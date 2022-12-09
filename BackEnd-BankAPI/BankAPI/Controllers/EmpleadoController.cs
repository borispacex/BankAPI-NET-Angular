using AutoMapper;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using BankAPI.Services.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IMapper _mapper;

        public EmpleadoController(IEmpleadoService empleadoService, IMapper mapper)
        {
            _empleadoService = empleadoService;
            _mapper = mapper;
        }

        [HttpGet("lista")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await _empleadoService.GetList();
            var listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);

            if (listaEmpleadoDTO.Count > 0) return Ok(listaEmpleadoDTO);
            else return NotFound();
        }
        [HttpGet("{idEmpleado}")]
        public async Task<IActionResult> GetById(int idEmpleado)
        {
            var _encontrado = await _empleadoService.Get(idEmpleado);
            if (_encontrado is null) return NotFound();
            else return Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
        }
        [HttpPost("guardar")]
        public async Task<IActionResult> Save([FromBody] EmpleadoDTO modelo)
        {
            var _empleado = _mapper.Map<Empleado>(modelo);
            var _empleadoCreado = await _empleadoService.Add(_empleado);

            if (_empleadoCreado.IdEmpleado != 0) return Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpPut("actualizar/{idEmpleado}")]
        public async Task<IActionResult> Update(int idEmpleado, [FromBody] EmpleadoDTO modelo)
        {
            var _encontrado = await _empleadoService.Get(idEmpleado);
            if (_encontrado is null) return NotFound();

            var _empleado = _mapper.Map<Empleado>(modelo);

            _encontrado.NombreCompleto = _empleado.NombreCompleto;
            _encontrado.IdDepartamento = _empleado.IdDepartamento;
            _encontrado.Sueldo = _empleado.Sueldo;
            _encontrado.FechaContrato = _empleado.FechaContrato;

            var respuesta = await _empleadoService.Update(_encontrado);
            if (respuesta) return Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpDelete("eliminar/{idEmpleado}")]
        public async Task<IActionResult> Delete(int idEmpleado)
        {
            var _encontrado = await _empleadoService.Get(idEmpleado);
            if (_encontrado is null) return NotFound();

            var respuesta = await _empleadoService.Delete(_encontrado);
            if (respuesta) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
