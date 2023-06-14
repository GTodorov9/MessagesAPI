using MessageHistoryAPI.DataModels;
using Microsoft.EntityFrameworkCore;

namespace MessageHistoryAPI
{
    public class MessageDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MessageDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["ConnectionString"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.UseSerialColumns();

            base.OnModelCreating(modelBuilder);
        }
    }
}
