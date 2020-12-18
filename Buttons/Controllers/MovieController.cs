using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Prac4.Models;

namespace Prac4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController:Controller
    {
        private readonly DbMovieContext _context;
        public MoviesController(DbMovieContext dbMovieContext)
        {
            _context = dbMovieContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Movies>> Get()
        {
            return await _context.GetAllAsync();
        }
        
        [HttpGet("get/{id}")]
        public async Task<Movies> Get(string id)
        {
            var tmp = await _context.GetByIdAsync(int.Parse(id));
            return tmp.FirstOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string id, [FromForm] string name,[FromForm] string janr,[FromForm] string author,[FromForm] string reliseDate)
        {
            await _context.InsertAsync(new Movies()
            {
                Author = author,
                Genre = janr,
                Name = name,
                Relise_Date = reliseDate,
            });
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] string currentId,[FromForm] string changedId, [FromForm] string name,[FromForm] string janr,[FromForm] string author,[FromForm] string reliseDate)
        {
            await _context.UpdateAsync(int.Parse(currentId), new Movies()
            {
                Author = author,
                Genre = janr,
                Name = name,
                Relise_Date = reliseDate,
            });
            

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            await _context.DeleteAsync(int.Parse(id));
            return Ok();
        }
    }
}