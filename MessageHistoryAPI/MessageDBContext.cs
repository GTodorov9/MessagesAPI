using MessageHistoryAPI.DataModels;
using Microsoft.EntityFrameworkCore;

namespace MessageHistoryAPI
{
    public class MessageDBContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ChatDatabase;Username=postgres;Password=123456;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.UseSerialColumns();

            base.OnModelCreating(modelBuilder);
        }
    }
}
