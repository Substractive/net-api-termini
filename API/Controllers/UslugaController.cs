using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")] // /api/usluga
    [ApiController]
    public class UslugaController(DataContext context, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UslugaDto>>> GetUsluge()
        {
            try
            {
                var usluge = await context.Usluge.ToListAsync();

                if(usluge == null) return NotFound();

                var uslugeToReturn = mapper.Map<IEnumerable<UslugaDto>> (usluge);

                return Ok(uslugeToReturn);

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                 ex.Message);
            }
        }

        [HttpGet("{id}")] // /api/usluga/1
        public async Task<ActionResult<UslugaDto>> GetUsluga(int id)
        {
            var usluga = await context.Usluge.FindAsync(id);

            if(usluga == null) return NotFound();

            return mapper.Map<UslugaDto>(usluga);
        }

        [HttpPost]
        public async Task<ActionResult<Usluga>> Insert(UslugaInsertDto uslugaInsertDto)
        {
            try
            {
                var usluga = new Usluga
                {
                    Naziv = uslugaInsertDto.Naziv,
                    Trajanje = uslugaInsertDto.Trajanje,
                    Vrsta = uslugaInsertDto.Vrsta,
                };

                context.Usluge.Add(usluga);
                await context.SaveChangesAsync();

                return Ok(usluga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                         ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsluga(int id)
        {
            try
            {
                var usluga = await context.Usluge.FindAsync(id);

                if(usluga != null)
                {
                    context.Usluge.Remove(usluga);
                    context.SaveChanges();
                }

                return Ok();

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);

            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUsluga(int id, UslugaUpdateDto uslugaUpdateDto)
        {
            try
            {
                var usluga = await context.Usluge.FirstOrDefaultAsync(u => u.Id == id);

                if (usluga == null) return BadRequest("Usluga nije pronađena");

                mapper.Map(uslugaUpdateDto, usluga); // uslugu iz baze će ažurirati sa podacima iz DTO objekta

                context.Usluge.Update(usluga);

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
