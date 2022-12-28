using Final.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

public class Customer
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }
    [Required]
    public string? UserName { get; set; }

    public long ? MobileNumber { get; set; }
    public string? Address { get; set; }

    [ForeignKey("customer")]
    [Required]
    public Guid CustomerUserID { get; set; }
    public virtual Authentication? customer { get; set; }
    public string? ProfilePicture { get; set; }

    public virtual ICollection<OrderDetails> OrderDeatils { get; set; } = new List<OrderDetails>();
}