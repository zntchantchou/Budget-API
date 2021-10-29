using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Contributor
  {
    public int ContributorId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public AppUser User { get; set; }

    [Required]
    public Expense Expense { get; set; }

    [Required]
    public int ExpenseId { get; set; }
  }
}

