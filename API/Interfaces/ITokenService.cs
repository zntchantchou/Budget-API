using System.Collections.Generic;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
       public string CreateToken(AppUser user);
       public Dictionary<string, string> ParseToken(string token);
    }
}