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
using AutoMapper;

namespace Biblioteca.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BibliotecaDbContext _context;
    private readonly IMapper _mapper;
    public BookController(BibliotecaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        var newBook = _mapper.Map<Book>(newData);
        var author = _context.Authors.Find(newData.AuthorId);
        if (author == null)
            return BadRequest("Ingrese un autor valido");

        _context.Books.Add(newBook);
        _context.SaveChanges();
        return Ok(_mapper.Map<BookResponse>(newBook));
    }

    [HttpPut("{id}")]
    public IActionResult EditBook(int id, [FromBody] BookInput newData) //esto esta mal, para q se usa el id?
    {
       var editBook = _context.Books.Find(id);
        if (editBook == null)
            return NotFound("No se encontro el libro");
       editBook.Name = newData.Name;
       editBook.AuthorId = newData.AuthorId;
        _context.SaveChanges();
        return Ok(_mapper.Map<BookResponse>(editBook));
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