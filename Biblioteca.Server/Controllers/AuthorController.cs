using Biblioteca.Server.DTOs;
using Biblioteca.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthorController : Controller
    {
        private readonly BibliotecaDbContext _context;
        public AuthorController(BibliotecaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _context.Authors.ToList();
            return Ok(_context.Authors.ToList());
        }

        [HttpGet("{name}")]
        public IActionResult GetAuthorByName(string name)
        {
            if (!_context.Authors.Any(a => a.Name == name))
                return NotFound();
            return Ok(_context.Authors.FirstOrDefault(a => a.Name == name));
        }

        [HttpPost()]
        public IActionResult AddAuthor([FromBody] AuthorInput newData)
        {
            if (newData == null)
                return BadRequest("Asigne un nombre al nuevo autor porfavor");
            if (_context.Authors.Any(a => a.Name == newData.Name))
                return BadRequest("El autor ya existe");

            var author = new Author { Name = newData.Name };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return Ok(author);
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteAuthor(string name)
        {
            if (!_context.Authors.Any(a => a.Name == name)) 
                return NotFound("El autor no existe");

            var author = _context.Authors.FirstOrDefault(a =>a.Name == name);
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
