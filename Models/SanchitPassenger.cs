using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flight.Models;

public partial class SanchitPassenger
{
    public int Cid { get; set; }

    [Required(ErrorMessage = "Customer Name is Mandatory")]
    public string? Cname { get; set; }

    public string? Location { get; set; }

    [DataType(DataType.EmailAddress,ErrorMessage = "Please enter a valid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Password is required")]
    public string? Password { get; set; }

    [NotMapped]
    [Compare("Password",ErrorMessage ="Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    public string? Ltype { get; set; }
}
