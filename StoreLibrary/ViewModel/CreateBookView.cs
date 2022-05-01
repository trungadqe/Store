using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreLibrary.Models;

namespace StoreLibrary.ViewModel
{
    public class CreateBookView
    {
        public Book book { get; set; } = null;
        [ValidateNever]
        public SelectList Category { get; set; } = null!;
        [ValidateNever]
        public SelectList Store { get; set; } = null!;
    }
}
