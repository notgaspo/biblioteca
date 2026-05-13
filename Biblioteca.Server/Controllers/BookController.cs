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

    //los get
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
    public IActionResult GetBookByName(string name)
    {
        return Ok(_context.Books.FirstOrDefault(b => b.Name == name));
    }

    //MOVER A UN CONTROLADOR DE AUTORES
    [HttpGet("author/search{name}")]
    public IActionResult GetAuthorByName(string name)
    {
        return Ok(_context.Authors.FirstOrDefault(a => a.Name == name));
    }

    //los post (IMPLEMENTAR DOTs)
    [HttpPost]
    public IActionResult AddBook([FromBody] BookInput newData)
    {
        if (newData.Name == null)
            return BadRequest("Asigne un nombre al nuevo libro");
        if (!_context.Authors.Any(b => b.Name == newData.AuthorName))
            return BadRequest("Ingrese un autor valido");
        var authorId = _context.Authors.FirstOrDefault(a => a.Name == newData.AuthorName).Id;
        var book = new Book { Name = newData.Name, AuthorId = authorId };
        _context.Books.Add(book);
        _context.SaveChanges();
        return Ok(book);
    }

    //MOVER A UN CONTROLADOR DE AUTORES
    [HttpPost("author")]
    public IActionResult AddAuthor([FromBody] AuthorInput newData)
    {
        if (newData == null)
            return BadRequest("Asigne un nombre al nuevo autor porfavor");
        var author = new Author { Name = newData.Name };
        _context.Authors.Add(author);
        _context.SaveChanges();
        return Ok(author);
    }

    //los put
    [HttpPut("{id}")]
    public IActionResult EditBook(int id, [FromBody] BookInput newData)
    {
        if (!_context.Books.Any(b => b.Id == id))
            return NotFound("El libro no esta registrado");

        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        book.Name = newData.Name;
        book.AuthorId = newData.AuthorId;

        _context.SaveChanges();
        return Ok(book);
    }

    //los delete
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();
        _context.Books.Remove(book);
        _context.SaveChanges();
        return NoContent();
    }

    //MOVER A UN CONTROLADOR DE AUTORES
    [HttpDelete("author/{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = _context.Authors.Find(id);
        if (author == null) return NotFound();
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return NoContent();
    }
}