using System.Collections.Generic;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using AutoMapper;
using API.DTOs;
using System;

namespace API.Controllers
{

  [Authorize]
  public class UsersController : BaseApiController
  {
    private DataContext _context { get; set; }
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
      _mapper = mapper;
      _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
      var user = await _userRepository.GetUserByIdAsync(id);
      Console.WriteLine("user", user);
      var mapped = _mapper.Map<UserDTO>(user);
      return Ok(mapped);
    }
    
     [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
    {
      var user = await _userRepository.GetUserByEmailAsync(email);
      return Ok(user);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
      var users = await _userRepository.GetUsersAsync();
      return Ok(users);
    }
  }
}