using eAuditoria.Data;
using eAuditoria.Data.Repository.ModelEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eAuditoria.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPontoVirgula.Controllers
{
    [Route("api/[controller]")]

    [ApiController]

    public class FilmeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Filme>>> GetList([FromServices] DataContext context)
        {
            var usuarios = await context.Filme
                                .AsNoTracking()
                                .ToListAsync();


            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeModelView>> GetUsuarioById([FromServices] DataContext context, int id)
        {
            var filme = await context.Filme
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            return new FilmeModelView()
            {
                Id = filme.Id,
                Tituto = filme.Tituto,
                ClassificacaoIndicativa = filme.ClassificacaoIndicativa,
                Lancamento = filme.Lancamento
            };
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromServices] DataContext context, FilmeModelView filmeModel)
        {
            try
            {
                Filme filme = new Filme();
                filme.Id = filmeModel.Id;
                filme.Tituto = filmeModel.Tituto;
                filme.Lancamento = filmeModel.Lancamento;
                filme.ClassificacaoIndicativa = filmeModel.ClassificacaoIndicativa;

                context.Filme.Add(filme);
                await context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromServices] DataContext context, FilmeModelView filmeModel)
        {
            try
            {

                var filme = await context.Filme
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == filmeModel.Id);

                if (filme == null)
                {
                    return NotFound();
                }

                filme.Tituto = filmeModel.Tituto;
                filme.Lancamento = filmeModel.Lancamento;
                filme.ClassificacaoIndicativa = filmeModel.ClassificacaoIndicativa;

                context.Filme.Update(filme);
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
                var filme = await context.Filme
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                if (filme == null)
                {
                    return NotFound();
                }

                context.Filme.Remove(filme);
                await context.SaveChangesAsync();
                return Ok(true);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromServices] DataContext context, IFormFile file)
        {
            try
            {

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        int colCount = worksheet.Dimension.End.Column;

                        int rowCount = worksheet.Dimension.End.Row;

                        for (int row = 1; row < rowCount; row++)
                        {

                            if (!int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out int id))
                            {
                                continue;
                            }


                            Filme filme = new Filme();
                            filme.Id = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                            filme.Tituto = worksheet.Cells[row, 2].Value.ToString();
                            filme.Lancamento = Convert.ToInt16(worksheet.Cells[row, 3].Value);
                            filme.ClassificacaoIndicativa = Convert.ToInt32(worksheet.Cells[row, 4].Value);

                            context.Filme.Add(filme);
                        }

                        await context.SaveChangesAsync();

                    }
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
