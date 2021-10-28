using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class CampaignController : BaseApiController
{
  private readonly ITokenService _tokenService;
  private readonly DataContext _context;
  private readonly IUserRepository _userRepository;

  public CampaignController(DataContext context, ITokenService tokenService, IUserRepository userRepository)
  {
    _context = context;
    _tokenService = tokenService;
    _userRepository = userRepository;
  }

  // [Authorize]
  [HttpPost]
  public async Task<ActionResult<string>> CreateCampaign(CampaignDTO campaignDTO)
  {
    var tokenString = Request.Headers["Authorization"].ToString();
    var parsed = _tokenService.ParseToken(tokenString);
    Console.WriteLine("email");
    Console.WriteLine(parsed["email"]);
    var user = await _userRepository.GetFullUserByEmailAsync(parsed["email"]);
    var campaignUsers = await _userRepository.GetFullUsersByEmailAsync(campaignDTO.EmailAddresses);
    var campaign = new Campaign { AdminId = user.Id, Title = campaignDTO.Title, Users = campaignUsers };
    _context.Campaigns.Add(campaign);
    await _context.SaveChangesAsync();
    return Ok(campaign.Title);
  }
}