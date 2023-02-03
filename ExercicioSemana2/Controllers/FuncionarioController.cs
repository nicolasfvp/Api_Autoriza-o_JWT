using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExercicioSemana2.Context;
using ExercicioSemana2.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Azure.Core;
using ExercicioSemana2.Dto;

namespace ExercicioSemana2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        private readonly RhContext _context;

        public FuncionarioController(RhContext context)
        {
            _context = context;
        }

        // GET: api/Funcionario
        [HttpGet]
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // PUT: api/Funcionario/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente, Administrador")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario request)
        {
            try
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }

                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<FuncionarioRequest, Funcionario>());
                var mapper = configuration.CreateMapper();
                var funcionario = mapper.Map<Funcionario>(request);

                funcionario.Id = id;

                _context.Entry(funcionario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Funcionario
        [HttpPost]
        [Authorize(Roles = "Gerente, Administrador")]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario request)
        {
            try
            {
                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<FuncionarioRequest, Funcionario>());
                var mapper = configuration.CreateMapper();
                var funcionario = mapper.Map<Funcionario>(request);
                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario == null)
                {
                    return NotFound();
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        private bool FuncionarioExists(int id)
        {
            return (_context.Funcionarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
