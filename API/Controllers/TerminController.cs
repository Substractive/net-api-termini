using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminController(DataContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerminDto>>> GetTermine()
        {
            try
            {
                var termini = await context.Termini.Include(t => t.Usluga).Include(t => t.Zaposlenik).ToListAsync();
                if (termini == null) return NotFound();

                var terminiToReturn = mapper.Map<IEnumerable<TerminDto>>(termini);

                return Ok(terminiToReturn);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                 ex.Message);
            }
        }

        [HttpGet("{id}")] // /api/termin/1
        public async Task<ActionResult<TerminDto>> GetTermin(int id)
        {
            var termin = await context.Termini.Include(t => t.Usluga).Include(t => t.Zaposlenik)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (termin == null) return NotFound();

            return mapper.Map<TerminDto>(termin);
        }

        [HttpPost]
        public async Task<ActionResult<Termin>> Insert(TerminInsertDto terminInsertDto)
        {
            try
            {
                var termin = mapper.Map<Termin>(terminInsertDto);

                if(terminInsertDto.ZaposlenikId != 0)
                {
                    var zaposlenik = await context.Zaposlenici.FindAsync(terminInsertDto.ZaposlenikId);
                    if (zaposlenik == null) return NotFound("Zaposlenik nije pronađen");
                    termin.Zaposlenik = zaposlenik;
                }

                if(terminInsertDto.UslugaId != 0)
                {
                    var usluga = await context.Usluge.FindAsync(terminInsertDto.UslugaId);
                    if (usluga == null) return NotFound("Usluga nije pronađena");
                    termin.Usluga = usluga;
                }

                context.Termini.Add(termin);
                await context.SaveChangesAsync();

                return Ok(termin);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                         ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTermin(int id)
        {
            try
            {
                var termin = await context.Termini.FindAsync(id);

                if (termin != null)
                {
                    context.Termini.Remove(termin);
                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);

            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTermin(int id, TerminUpdateDto terminUpdateDto)
        {
            try
            {
                var termin = await context.Termini.Include(t => t.Zaposlenik)
                    .Include(t => t.Usluga)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (termin == null)
                {
                    return NotFound();  // Return 404 if the entity is not found
                }

                mapper.Map(terminUpdateDto, termin);

                if(terminUpdateDto.ZaposlenikId != 0)
                {
                    termin.Zaposlenik = await context.Zaposlenici.FindAsync(terminUpdateDto.ZaposlenikId);
                    if(termin.Zaposlenik == null)
                    {
                        return NotFound("Zaposlenik nije pronađen");
                    }
                }

                if (terminUpdateDto.UslugaId != 0)
                {
                    termin.Usluga = await context.Usluge.FindAsync(terminUpdateDto.UslugaId);
                    if (termin.Usluga == null)
                    {
                        return NotFound("Usluga nije pronađen");
                    }
                }

                context.Termini.Update(termin);

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
