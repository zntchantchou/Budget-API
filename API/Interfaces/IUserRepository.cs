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
    Task<UserDTO> GetUserByEmailAsync(String email);
    Task<FullUserDTO> GetFullUserByEmailAsync(String email);
    Task<ICollection<AppUser>> GetFullUsersByEmailAsync(List<string> email);

  }
}