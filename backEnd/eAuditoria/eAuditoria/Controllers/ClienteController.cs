using eAuditoria.Data;
using eAuditoria.Data.Repository.ModelEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eAuditoria.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eAuditoria.Controllers
{
    [Route("api/[controller]")]

    [ApiController]

    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetList([FromServices] DataContext context)
        {
            var usuarios = await context.Cliente
                                .AsNoTracking()
                                .ToListAsync();


            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModelView>> GetUsuarioById([FromServices] DataContext context, int id)
        {
            var cliente = await context.Cliente
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return new ClienteModelView()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                DataNascimento = cliente.DataNascimento,
                Nome = cliente.Nome
            };
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromServices] DataContext context, ClienteModelView clienteModel)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = clienteModel.Id;
                cliente.Cpf = clienteModel.Cpf;
                cliente.DataNascimento = clienteModel.DataNascimento;
                cliente.Nome = clienteModel.Nome;

                context.Cliente.Add(cliente);
                await context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromServices] DataContext context, ClienteModelView clienteModel)
        {
            try
            {

                var cliente = await context.Cliente
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == clienteModel.Id);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Cpf = clienteModel.Cpf;
                cliente.DataNascimento = clienteModel.DataNascimento;
                cliente.Nome = clienteModel.Nome;

                context.Cliente.Update(cliente);
                await context.SaveChangesAsync();
                return Ok(true);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromServices] DataContext context, int id)
        {
            try
            {
                var cliente = await context.Cliente
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null)
                {
                    return NotFound();
                }

                context.Cliente.Remove(cliente);
                await context.SaveChangesAsync();
                return Ok(true);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
