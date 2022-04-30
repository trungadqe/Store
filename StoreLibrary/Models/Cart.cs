using StoreLibrary.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWEB.Models

{
    public class Cart
    {
        public int UId { get; set; }
        public string? BookIsbn { get; set; }
        public StoreLibraryUser? User { get; set; }
        public Book? Book { get; set; }

        public virtual ICollection<Book>? Books  { get; set; }


    }
}
