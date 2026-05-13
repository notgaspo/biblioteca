using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Biblioteca.Server.Models;
using System.Reflection.Metadata.Ecma335;
using Biblioteca.Server.DTOs;

namespace Biblioteca.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BibliotecaDbContext _context;
    public BookController(BibliotecaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _context.Books.ToList();
        return Ok(_context.Books.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        return Ok(_context.Books.Find(id));
    }

    [HttpGet("search{name}")]
    public IActionResult GetBookByName(string name) //aca hay que hacer el caso null
    {
        if (!_context.Books.Any(x => x.Name == name))
            return NotFound("No se encontro el libro");
        return Ok(_context.Books.FirstOrDefault(b => b.Name == name));
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] BookInput newData)
    {
        if (newData.NewName == null)
            return BadRequest("Asigne un nombre al nuevo libro");
        if (!_context.Authors.Any(b => b.Name == newData.AuthorName))
            return BadRequest("Ingrese un autor valido");

        var authorId = _context.Authors.FirstOrDefault(a => a.Name == newData.AuthorName).Id;
        var book = new Book { Name = newData.NewName, AuthorId = authorId };
        _context.Books.Add(book);
        _context.SaveChanges();
        return Ok(book);
    }

    [HttpPut("{id}")]
    public IActionResult EditBook([FromBody] BookInput newData)
    {
        if (!_context.Books.Any(b => b.Name == newData.OldName))
            return NotFound("El libro no esta registrado");

        var book = _context.Books.FirstOrDefault(b => b.Name == newData.OldName);
        book.Name = newData.NewName;

        if (!(newData.AuthorName == null))
        {
            var author = _context.Authors.FirstOrDefault(a => a.Name == newData.AuthorName);
            book.Author = author;
        }
        _context.SaveChanges();
        return Ok(book);

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();
        _context.Books.Remove(book);
        _context.SaveChanges();
        return NoContent();
    }

}