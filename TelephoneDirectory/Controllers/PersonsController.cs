using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonController( DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(await _context.Persons.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Person>>> Get(int id)
        {
            var pers = await _context.Persons.FindAsync(id);
            if (pers == null)
                return BadRequest("Person is not found");

            return Ok(pers);
        }
        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person pers)
        {
            _context.Persons.Add(pers);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var dbPers = await _context.Persons.FindAsync(request.Id);
            if (dbPers == null)
                return BadRequest("Person is not found");

            dbPers.Name = request.Name;
            dbPers.LastName = request.LastName;
            dbPers.Number = request.Number;

            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> DeletePerson(int id)
        {
            var dbPers = await _context.Persons.FindAsync(id);
            if (dbPers == null)
                return BadRequest("Person is not found");

            _context.Persons.Remove(dbPers);
            await _context.SaveChangesAsync();


            return Ok(await _context.Persons.ToListAsync());

        }

    }
}
