using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Expense
  {
    public int Id { get; set; }
    [Required]
    public AppUser Author { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public float Amount { get; set; }
    [Required]
    public AppUser PaidBy { get; set; }
    [Required]
    public int PaidById { get; set; }
    [Required]
    public Campaign Campaign { get; set; }
    [Required]
    public int CampaignId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime ExpenseDate { get; set; } = DateTime.Now;    
  }
}