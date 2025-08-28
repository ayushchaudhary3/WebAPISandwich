using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPISandwich.Model;

namespace WebAPISandwich.Model
{
    public class SandwichContext : DbContext
    {
        public SandwichContext(DbContextOptions<SandwichContext> options) : base(options)
        {
        }
        //static string connectionstring = @"Data Source=IBM-D7M2SB4\SQLEXPRESS;Initial Catalog=AyushDB;TrustServerCertificate= True; Integrated Security=True";
        public DbSet<Sandwich> Sandwiches { get; set; }
    }
}