using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
  public class FullUserDTO : UserDTO
  {
    public int Id { get; set; }
  }
}