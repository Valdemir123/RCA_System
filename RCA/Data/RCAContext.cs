using Microsoft.EntityFrameworkCore;
using RCA.Models;

namespace RCA.Data
{
    public class RCAContext : DbContext
    {
        public RCAContext(DbContextOptions<RCAContext> options) : base(options) { }


        public DbSet<Class_Book> Class_Book { get; set; }
        public DbSet<Class_BookItem> Class_BookItem { get; set; }


        public DbSet<Class_Channel> Class_Channel { get; set; }


        public DbSet<Class_Company> Class_Company { get; set; }


        public DbSet<Class_User> Class_User { get; set; }

        
        public DbSet<Class_GroupLevel> Class_GroupLevel { get; set; }
        public DbSet<Class_GroupLevelItem> Class_GroupLevelItem { get; set; }


        public DbSet<Class_Season> Class_Season { get; set; }
        public DbSet<Class_SeasonItem> Class_SeasonItem { get; set; }
        

        public DbSet<Class_Guest> Class_Guest { get; set; }
    }
}
