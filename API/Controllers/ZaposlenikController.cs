using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaposlenikController(DataContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZaposlenikDto>>> GetZaposlenici()
        {
            try
            {
                var zaposlenici = await context.Zaposlenici.ToListAsync();

                if (zaposlenici == null) return NotFound();

                var zaposleniciToReturn = mapper.Map<IEnumerable<ZaposlenikDto>>(zaposlenici);

                return Ok(zaposleniciToReturn);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                 ex.Message);
            }
        }

        [HttpGet("{id}")] // /api/zaposlenik/1
        public async Task<ActionResult<ZaposlenikDto>> GetZaposlenik(int id)
        {
            var zaposlenik = await context.Zaposlenici.FindAsync(id);

            if (zaposlenik == null) return NotFound();

            return mapper.Map<ZaposlenikDto>(zaposlenik);
        }

        [HttpPost]
        public async Task<ActionResult<Zaposlenik>> Insert(ZaposlenikInsertDto zaposlenikInsertDto)
        {
            try
            {
                var zaposlenik = new Zaposlenik
                {
                   Ime = zaposlenikInsertDto.Ime,
                   Prezime = zaposlenikInsertDto.Prezime,
                   Struka = zaposlenikInsertDto.Struka
                };

                context.Zaposlenici.Add(zaposlenik);
                await context.SaveChangesAsync();

                return Ok(zaposlenik);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                         ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteZaposlenik(int id)
        {
            try
            {
                var zaposlenik = await context.Zaposlenici.FindAsync(id);

                if (zaposlenik != null)
                {
                    context.Zaposlenici.Remove(zaposlenik);
                    context.SaveChanges();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);

            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateZaposlenik(int id, ZaposlenikUpdateDto zaposlenikUpdateDto)
        {
            try
            {
                var zaposlenik = await context.Zaposlenici.FirstOrDefaultAsync(z => z.Id == id);

                if (zaposlenik == null) return BadRequest("Zaposlenik nije pronađen");

                mapper.Map(zaposlenikUpdateDto, zaposlenik);

                context.Zaposlenici.Update(zaposlenik);

                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }
    }
}
