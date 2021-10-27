using System;
using API.Controllers;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class CampaignController : BaseApiController
{
private readonly DataContext _context;
    public CampaignController(DataContext context)
    {
      _context = context;

    }

    [Authorize]
    [HttpPost]
    public void CreateCampaign(CampaignDto campaignDTO) {
        Console.Write("CreateCampaign");
        Console.Write(campaignDTO.Title.ToString());
    } 
}