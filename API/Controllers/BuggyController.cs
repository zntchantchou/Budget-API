using System;
using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BuggyController : BaseApiController
{
  private readonly DataContext _context;
  public BuggyController(DataContext context)
  {
    _context = context;
  }

  [Authorize]
  [HttpGet("auth")]
  public string GetSecret()
  {
    Console.WriteLine("!!! Auth Request");
    return "secret text";
  }

  [HttpGet("not-found")]
  public ActionResult<AppUser> GetNotFound()
  {
    var thing = _context.Users.Find(-1);
    if (thing == null) return NotFound();
    return Ok(thing);
  }

  [HttpGet("server-error")]
  public ActionResult<string> GetServerError()
  {
    var thing = _context.Users.Find(-1);
    var thingToReturn = thing.ToString();
    return thingToReturn;
  }

  [HttpGet("bad-request")]
  public ActionResult<string> GetBadRequest()
  {
    return BadRequest("This was not a good request");
  }
}