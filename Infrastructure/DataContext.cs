using Domain;
using Domain.Enums;
using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<PromptSet> PromptSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prompt>().HasKey(p => p.Id);
            modelBuilder.Entity<PromptSet>().HasKey(p => p.Id);

            modelBuilder
                .Entity<Prompt>()
                .Property(p => p.WrongAnswers)
                .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions)null)
                );

            modelBuilder
                .Entity<PromptSet>()
                .Property(p => p.Tags)
                .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions)null)
                );

            modelBuilder.Entity<PromptSet>()
                .HasMany(ps => ps.Prompts)
                .WithOne()
                .HasForeignKey(p => p.PromptSetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Property(u => u.Badges)
                .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<ICollection<Badge>>(v, (JsonSerializerOptions)null)
                );

            modelBuilder.Seed();
        }
    }
}
