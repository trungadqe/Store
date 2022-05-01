using StoreLibrary.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreLibrary.Models

{
    public class Cart
    {
        public string UId { get; set; }
        public string? BookIsbn { get; set; }
        public StoreLibraryUser? User { get; set; }
        public Book? Book { get; set; }
    }
}
