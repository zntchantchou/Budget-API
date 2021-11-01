using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    void Update(AppUser user);

    Task<bool> SaveAllAsync();
    Task<IEnumerable<UserDTO>> GetUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByEmailAsync(String email);

    Task<ICollection<AppUser>> GetUsersByEmailAsync(List<string> email);
    Task<FullUserDTO> GetFullUserByEmailAsync(String email);
    Task<ICollection<FullUserDTO>> GetFullUsersByEmailAsync(List<string> email);

  }
}