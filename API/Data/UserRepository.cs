using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    public DataContext _context { get; }
    public IMapper _mapper { get; }
    public UserRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
      return await _context.Users
      .Where(u => u.AppUserId == id)
      .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
    }

    public async Task<UserDTO> GetUserByEmailAsync(String email)
    {
      return await _context.Users
      .Where(u => u.Email == email)
      .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
    }

    public async Task<AppUser> GetFullUserByEmailAsync(String email)
    {
      return await _context.Users
      .Where(u => u.Email == email)
      .SingleOrDefaultAsync();
    }
    public async Task<ICollection<AppUser>> GetFullUsersByEmailAsync(List<string> emails)
    {
      return await _context.Users
      .Where(u => emails.Contains(u.Email))
      .ToListAsync();
    }

    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
      return await _context.Users
      .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
      .ToListAsync();
    }

    public void Update(AppUser user)
    {
      // EF will add a modified flag to this entity
      _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}
