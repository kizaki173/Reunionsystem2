using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.Application.DTOs;
using Reunionsystem2API.Application.Contract;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalas()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return Ok(salas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSala(int id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null) return NotFound();
            return Ok(sala);
        }

        [HttpPost]
        public async Task<IActionResult> PostSala([FromBody] SalaCreateDto salaCreateDto)
        {
            try
            {
                var nuevaSala = await _salaService.CreateSalaAsync(salaCreateDto);
                return CreatedAtAction(nameof(GetSala), new { id = nuevaSala.Id }, nuevaSala);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}