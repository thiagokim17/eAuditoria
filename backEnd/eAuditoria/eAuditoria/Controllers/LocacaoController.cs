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
        public async Task<ActionResult<List<LocacaoModelView>>> GetList([FromServices] DataContext context)
        {
            var locacoes = await context.Locacao
                                .AsNoTracking()
                                .ToListAsync();


            return locacoes.Select(locacao => new LocacaoModelView()
            {
                Id = locacao.Id,
                IdCliente = context.Cliente
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Cliente).Id,

                NomeCliente = context.Cliente
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Cliente).Nome,

                IdFilme = context.Filme
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Filme).Id,

                TituloFilme = context.Filme
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Filme).Tituto,

                DataDevolucao = locacao.DataDevolucao.HasValue ? locacao.DataDevolucao.Value.ToString("dd/MM/yyyy") : String.Empty,
                DataLocacao = locacao.DataLocacao.ToString("dd/MM/yyyy"),
            }).ToList();
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

                IdCliente = context.Cliente
                        .AsNoTracking().FirstOrDefault(x => x.Id == locacao.Id_Cliente).Id,

                NomeCliente = context.Cliente
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Cliente).Nome,

                IdFilme = context.Filme
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Filme).Id,

                TituloFilme = context.Filme
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == locacao.Id_Filme).Tituto,

                DataDevolucao = locacao.DataDevolucao.HasValue ? locacao.DataDevolucao.Value.ToString("dd/MM/yyyy") : String.Empty,
                DataLocacao = locacao.DataLocacao.ToString("dd/MM/yyyy"),
            };
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromServices] DataContext context, LocacaoModelView locacaoModel)
        {
            try
            {
                Locacao locacao = new Locacao();
                locacao.Id = locacaoModel.Id;
                locacao.Id_Cliente = locacaoModel.IdCliente;
                locacao.Id_Filme = locacaoModel.IdFilme;


                if (!string.IsNullOrEmpty(locacaoModel.DataLocacao))
                {
                    var dataLocacaoSplit = locacaoModel.DataLocacao.Split('/');
                    locacao.DataLocacao = new DateTime(Convert.ToInt32(dataLocacaoSplit[2]), Convert.ToInt32(dataLocacaoSplit[1]), Convert.ToInt32(dataLocacaoSplit[0]));
                }
                else
                {
                    locacao.DataLocacao = System.DateTime.Now;
                }

                if (!string.IsNullOrEmpty(locacaoModel.DataDevolucao))
                {
                    var dataDevolucaoSplit = locacaoModel.DataDevolucao.Split('/');
                    locacao.DataDevolucao = new DateTime(Convert.ToInt32(dataDevolucaoSplit[2]), Convert.ToInt32(dataDevolucaoSplit[1]), Convert.ToInt32(dataDevolucaoSplit[0]));
                }
                else
                {
                    locacao.DataDevolucao = null;
                }


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

                locacao.Id_Cliente = locacaoModel.IdCliente;
                locacao.Id_Filme = locacaoModel.IdFilme;

                if (!string.IsNullOrEmpty(locacaoModel.DataLocacao))
                {
                    var dataLocacaoSplit = locacaoModel.DataLocacao.Split('/');
                    locacao.DataLocacao = new DateTime(Convert.ToInt32(dataLocacaoSplit[2]), Convert.ToInt32(dataLocacaoSplit[1]), Convert.ToInt32(dataLocacaoSplit[0]));
                }
                else
                {
                    locacao.DataLocacao = System.DateTime.Now;
                }

                if (!string.IsNullOrEmpty(locacaoModel.DataDevolucao))
                {
                    var dataDevolucaoSplit = locacaoModel.DataDevolucao.Split('/');
                    locacao.DataDevolucao = new DateTime(Convert.ToInt32(dataDevolucaoSplit[2]), Convert.ToInt32(dataDevolucaoSplit[1]), Convert.ToInt32(dataDevolucaoSplit[0]));
                }
                else
                {
                    locacao.DataDevolucao = null;
                }

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
