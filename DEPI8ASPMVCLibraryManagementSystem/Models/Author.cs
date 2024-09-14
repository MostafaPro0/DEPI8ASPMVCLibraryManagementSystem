using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI8ASPMVCLibraryManagementSystem.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}

