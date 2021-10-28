using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

  // [Authorize]
  [HttpPost]
  public async Task<ActionResult<string>> CreateCampaign(CampaignDTO campaignDTO)
  {
    var tokenString = Request.Headers["Authorization"].ToString();
    var userData = _tokenService.ParseToken(tokenString);
    var user = await _userRepository.GetFullUserByEmailAsync(userData["email"]);
    var campaign = new Campaign { 
      AdminId = user.Id,
      Title = campaignDTO.Title 
    };
    if (campaignDTO.UserEmails.Count > 0)
    {
      // make sure  a user was found for every user provided 
      var campaignUsers = await _userRepository.GetFullUsersByEmailAsync(campaignDTO.UserEmails);
      if (campaignUsers != null && campaignUsers.Count == campaignDTO.UserEmails.Count)
      {
        campaign.Users = campaignUsers;
      }
    }

    // right now if users are not retrieved by email, campaign is still created without users
    _context.Campaigns.Add(campaign);
    await _context.SaveChangesAsync();
    return Ok(campaignDTO);
  }
}