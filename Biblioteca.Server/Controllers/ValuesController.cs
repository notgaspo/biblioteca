using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

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
    [HttpGet("search/{name}")]
    public IActionResult GetBookByName(string name)
    {
        return Ok(_context.Books.FirstOrDefault(b => b.Name == name));
    }

    //los post
    [HttpPost("add{name}&{author}")]



    [HttpPost]
    public ActionResult Get() { return Ok("BookController is working"); }
}