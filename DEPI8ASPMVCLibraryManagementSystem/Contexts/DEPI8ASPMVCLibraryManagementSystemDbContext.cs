using DEPI8ASPMVCLibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI8ASPMVCLibraryManagementSystem.Contexts
{
    public class DEPI8ASPMVCLibraryManagementSystemDbContext:IdentityDbContext<ApplicationUser> 
    {

        public DEPI8ASPMVCLibraryManagementSystemDbContext()
        {

        }
        public DEPI8ASPMVCLibraryManagementSystemDbContext(DbContextOptions<DEPI8ASPMVCLibraryManagementSystemDbContext>options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB; Database=LibraryManagementSystemDb;Trusted_Connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
