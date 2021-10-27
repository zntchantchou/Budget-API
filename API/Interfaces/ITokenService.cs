using System.Collections.Generic;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
       public string CreateToken(AppUser user);
<<<<<<< HEAD
=======
       public bool DecodeToken(string token);
>>>>>>> 6b1259f44cde8c8f7436747d31a2a96f1d723630
       public Dictionary<string, string> ParseToken(string token);
    }
}