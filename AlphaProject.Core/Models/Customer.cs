using System.ComponentModel.DataAnnotations;

namespace AlphaProject.Core.Models;

public class Customer
{
    [Key]
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
