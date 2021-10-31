using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.DTOs;
using System.Linq;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    var user = await _userRepository.GetFullUserByEmailAsync(userData["email"]);
    var campaign = new Campaign
    {
      AdminId = user.AppUserId,
      Title = campaignDTO.Title,
      Description = campaignDTO.Description,
    };

    if (campaignDTO.UserEmails.Count > 0)
    {
    // right now if users are not retrieved by email, campaign is still created without users
      // make sure  a user was found for every user provided 
      var campaignUsers = await _userRepository.GetFullUsersByEmailAsync(campaignDTO.UserEmails);
      if (campaignUsers == null || campaignUsers.Count != campaignDTO.UserEmails.Count)
      {
        return BadRequest("Some of the users that you tried to add to this campaign do not exist");
      }
    }
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
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    var userCampaignRefs = await _context.UserCampaigns.Where(uc => uc.UserId == user.AppUserId).ToListAsync();
    var userCampaigns = new List<Campaign>();
    foreach(var campaignRef in userCampaignRefs) {
      var found = await _context.Campaigns.FindAsync(campaignRef.CampaignId);
      if(found != null) { 
        userCampaigns.Add(found);
      }
    }
    var mapped = _mapper.Map<List<UserCampaignDTO>>(userCampaigns);
    return mapped;
  }
}