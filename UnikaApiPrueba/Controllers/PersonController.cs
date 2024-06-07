using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnikaApiPrueba.Helper;
using UnikaApiPrueba.Helper.Dto;
using UnikaApiPrueba.Models;

namespace UnikaApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public PersonController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Person>> Add(Person person)
        {
            try
            {
                person.UniqueIdentifier = Guid.NewGuid();
                person.Cedula = CedulaGenerator();
                person.CreateDate = DateTime.UtcNow;
                person.UpdateDate = null;
                person.DeleteDate = null;
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message + " " + ex.StackTrace);
            }  

        }
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Person>>> List([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            try
            {
                var totalRecords = await _context.Persons.CountAsync();
                var persons = await _context.Persons
                                            .Skip(offset)
                                            .Take(limit)
                                            .ToListAsync();
                var result = new
                {
                    TotalRecords = totalRecords,
                    Limit = limit,
                    Offset = offset,
                    Data = persons
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + " " + ex.StackTrace);
            }
        }

  
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PersonUpdateDto person)
        {
            try
            {
                var personDb = await _context.Persons.FindAsync(person.Id);
                if (personDb == null)
                {
                    return NotFound();
                }


                personDb.FirstName = person.FirstName;
                personDb.LastName = person.LastName;
                personDb.Sex = person.Sex;
                personDb.BirthDate = person.BirthDate;
                personDb.UpdateDate = DateTime.UtcNow;

                _context.Entry(personDb).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + " " + ex.StackTrace);
            }

        }

        [HttpPut("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {

            try
            {
                var person = await _context.Persons.FindAsync(Id);
                person.DeleteDate = DateTime.UtcNow;
                _context.Entry(person).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + " " + ex.StackTrace);
            }

        }

        public static string CedulaGenerator()
        {
            Random random = new Random();
            int LastDigit = random.Next(0, 10);
            string CentralDigit = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                CentralDigit += random.Next(0, 10).ToString();
            }
            string InitDigit = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                InitDigit += random.Next(0, 10).ToString();
            }
            string Cedula = $"{InitDigit}-{CentralDigit}-{LastDigit}";
            return Cedula;
        }
    }
}

