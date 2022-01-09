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

    public class ClienteController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ClienteModelView>>> GetList([FromServices] DataContext context)
        {
            var cliente = await context.Cliente
                                .AsNoTracking()
                                .ToListAsync();




            return cliente.Select(cliente => new ClienteModelView()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                DataNascimento = cliente.DataNascimento.ToString("dd/MM/yyyy"),
                Nome = cliente.Nome
            }).ToList();
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
                DataNascimento = cliente.DataNascimento.ToString("dd/MM/yyyy"),
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

                var clienteSplit = clienteModel.DataNascimento.Split('/');
                cliente.DataNascimento = new DateTime(Convert.ToInt32(clienteSplit[2]), Convert.ToInt32(clienteSplit[1]), Convert.ToInt32(clienteSplit[0]));
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
                var clienteSplit = clienteModel.DataNascimento.Split('/');
                cliente.DataNascimento = new DateTime(Convert.ToInt32(clienteSplit[2]), Convert.ToInt32(clienteSplit[1]), Convert.ToInt32(clienteSplit[0]));
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
