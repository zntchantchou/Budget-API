using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Contributor
  {
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public virtual AppUser User { get; set; }

    [Required]
    public virtual Expense Expense { get; set; }

    [Required]
    public int ExpenseId { get; set; }
  }
}

