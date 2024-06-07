using Microsoft.EntityFrameworkCore;

namespace Animal_Rental.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Articals> Articals { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Complaints> Complaints { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LU175Q4\\SQLEXPRESS01;Initial Catalog=Animal_Rental;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=true");
                base.OnConfiguring(optionsBuilder);
        }
       

    }
}
