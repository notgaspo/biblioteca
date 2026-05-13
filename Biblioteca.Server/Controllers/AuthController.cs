using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Biblioteca.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase

{
    public AuthController()
    {

    }
    [HttpGet]
    [HttpPost]
    public ActionResult Get() { return Ok("AuthController is working"); }


}