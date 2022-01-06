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

    public class LocacaoController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Locacao>>> GetList([FromServices] DataContext context)
        {
            var usuarios = await context.Locacao
                                .AsNoTracking()
                                .ToListAsync();


            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocacaoModelView>> GetUsuarioById([FromServices] DataContext context, int id)
        {
            var locacao = await context.Locacao
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (locacao == null)
            {
                return NotFound();
            }

            return new LocacaoModelView()
            {
                Id = locacao.Id,
                Id_Cliente = locacao.Id_Cliente,
                Id_Filme = locacao.Id_Filme,
                DataDevolucao = locacao.DataDevolucao,
                DataLocacao = locacao.DataLocacao
            };
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromServices] DataContext context, LocacaoModelView locacaoModel)
        {
            try
            {
                Locacao locacao = new Locacao();
                locacao.Id = locacaoModel.Id;
                locacao.Id_Cliente = locacaoModel.Id_Cliente;
                locacao.Id_Filme = locacaoModel.Id_Filme;
                locacao.DataDevolucao = locacaoModel.DataDevolucao;
                locacao.DataLocacao = locacaoModel.DataLocacao;

                context.Locacao.Add(locacao);
                await context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromServices] DataContext context, LocacaoModelView locacaoModel)
        {
            try
            {

                var locacao = await context.Locacao
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == locacaoModel.Id);

                if (locacao == null)
                {
                    return NotFound();
                }

                locacao.Id_Cliente = locacaoModel.Id_Cliente;
                locacao.Id_Filme = locacaoModel.Id_Filme;
                locacao.DataDevolucao = locacaoModel.DataDevolucao;
                locacao.DataLocacao = locacaoModel.DataLocacao;

                context.Locacao.Update(locacao);
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

                var locacao = await context.Locacao
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                if (locacao == null)
                {
                    return NotFound();
                }

                context.Locacao.Remove(locacao);
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
