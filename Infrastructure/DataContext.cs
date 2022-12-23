using Domain.Games.Elements;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Prompt> Prompts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Prompt>()
                .Property(p => p.WrongAnswers)
                .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions)null)
                );
        }
    }
}
