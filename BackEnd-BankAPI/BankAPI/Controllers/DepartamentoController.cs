using AutoMapper;
using BankAPI.Data.DTOs;
using BankAPI.Services.Contrato;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly IDepartamentoService _departamentoService;
        private readonly IMapper _mapper;

        public DepartamentoController(IDepartamentoService departamentoService, IMapper mapper)
        {
            _departamentoService = departamentoService;
            _mapper = mapper;
        }

        [HttpGet("lista")]
        public async Task<IActionResult> Get()
        {
            var listaDepartamento = await _departamentoService.GetList();
            var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

            if (listaDepartamentoDTO.Count > 0) return Ok(listaDepartamentoDTO);
            else return NotFound();
        }
    }
}
