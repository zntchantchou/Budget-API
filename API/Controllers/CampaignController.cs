using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
public class CampaignController : BaseApiController
{
  private readonly ITokenService _tokenService;
  private readonly DataContext _context;
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public CampaignController(DataContext context, ITokenService tokenService, IUserRepository userRepository, IMapper mapper)
  {
    _context = context;
    _tokenService = tokenService;
    _userRepository = userRepository;
    _mapper = mapper;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<CampaignDTO>> CreateCampaign(CampaignDTO campaignDTO)
  {
    var tokenString = Request.Headers["Authorization"].ToString();
    var userData = _tokenService.ParseToken(tokenString);
    var user = await _userRepository.GetUserByEmailAsync(userData["email"]);
    var campaignUsers =  new List<AppUser>();
    campaignUsers.Add(user);
    if (campaignDTO.UserEmails.Count > 0)
    {
      // make sure every user provided was found or return error 
      var additionalUsers = await _userRepository.GetUsersByEmailAsync(campaignDTO.UserEmails);
      if (additionalUsers == null || additionalUsers.Count != campaignDTO.UserEmails.Count)
      {
        return BadRequest("Some of the users that you tried to add to this campaign do not exist");
      }
      campaignUsers.AddRange(additionalUsers);
    }
    foreach(var addedUser in campaignUsers) {
      Console.WriteLine("User added: ");
      Console.WriteLine(addedUser.Email);
    }
    var campaign = new Campaign
      {
        Title = campaignDTO.Title,
        Description = campaignDTO.Description,
        Users = campaignUsers, 
        Admin = user,
      };
    _context.Campaigns.Add(campaign);
    await _context.SaveChangesAsync();
    return Ok(campaignDTO);
  }

  [Authorize]
  [HttpGet]
  public async Task<List<UserCampaignDTO>> GetUserCampaigns()
  {
    var tokenString = Request.Headers["Authorization"].ToString();
    var userData = _tokenService.ParseToken(tokenString);
    var email = userData["email"];
    var user = await _context.Users
    .Include(u => u.Campaigns)
    .FirstOrDefaultAsync(u => u.Email == email);   
    var userCampaigns = user.Campaigns.OrderByDescending(c => c.CreatedAt);
    var mapped = _mapper.Map<List<UserCampaignDTO>>(userCampaigns);
    return mapped;
  }

  [Authorize]
  [HttpGet("admin")]
  public async Task<List<UserCampaignDTO>> GetAdminCampaigns()
  {
    var tokenString = Request.Headers["Authorization"].ToString();
    var userData = _tokenService.ParseToken(tokenString);
    var email = userData["email"];
    var currentUser = await _userRepository.GetUserByEmailAsync(email);
    var userCampaigns = await _context.Campaigns.Where(c => c.Admin.AppUserId == currentUser.AppUserId).ToListAsync();
    var mapped = _mapper.Map<List<UserCampaignDTO>>(userCampaigns);
    return mapped;
  }

  [Authorize]
  [HttpGet("{id}")]
  public async Task<CampaignDetailsDTO> GetCampaign(int id) {
    var cpgn = await _context.Campaigns
    .Include(c => c.Users)
    .FirstOrDefaultAsync(elt => elt.CampaignId == id);
    return new CampaignDetailsDTO {
      Title = cpgn.Title, 
      Description = cpgn.Description, 
      Users = _mapper.Map<ICollection<UserDTO>>(cpgn.Users), 
    };
  }
}