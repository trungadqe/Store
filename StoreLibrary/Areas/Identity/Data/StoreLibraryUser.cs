using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StoreLibrary.Models;

namespace StoreLibrary.Areas.Identity.Data;

// Add profile data for application users by adding properties to the StoreLibraryUser class
public class StoreLibraryUser : IdentityUser
{
    public DateTime? DoB { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
    public Store? Store { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<Cart>? Carts { get; set; }
}

