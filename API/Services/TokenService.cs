using API.Interfaces;
using API.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Text.Json;

namespace API.Services
{
  public class TokenService : ITokenService
  {
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(AppUser user)
    {
      var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds,
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    public bool DecodeToken(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      try
      {
        var securityToken = tokenHandler.ValidateToken(token.Substring(7), new TokenValidationParameters
        {
          IssuerSigningKey = _key,
          ValidateIssuerSigningKey = true,
        }, out SecurityToken validatedToken);
        Console.Write("BONJOUR");
      }
      catch
      {
        return false;
      }

      //   Console.Write("asString");
      //   Console.Write(asString);
      return true;
    }


    public Dictionary<string, string> ParseToken(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      try
      {
        var securityToken = tokenHandler.ReadToken(token.Substring(7)) as JwtSecurityToken;
        var claims = securityToken.Claims;
        // var otherToken = new JwtSecurityToken(token.Substring(7));
        // var asObj = otherToken.Subject.ToString();
        // Console.WriteLine("asObj");
        // Console.WriteLine(asObj);
        var parsed = new Dictionary<string, string>();
        foreach (Claim claim in claims)
        {
          if (claim.Type != null && claim.Value != null)
          {
            parsed.Add(claim.Type, claim.Value);
          }
        }
        // Console.WriteLine(parsed["email"]);
        // Console.WriteLine(parsed["nameid"]);
        return parsed;
      }
      catch
      {
        return new Dictionary<string, string>();
      }
    }
  }
}