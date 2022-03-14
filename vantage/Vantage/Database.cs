using Microsoft.EntityFrameworkCore;
using Vantage.Models;

namespace Vantage
{
    public class Database : DbContext
    {
        public Database(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<User>();
            user.ToTable(nameof(User));
            user.HasKey(u => u.Id);
            
            var comment = builder.Entity<Comment>();
            comment.ToTable(nameof(Comment));
            comment.HasKey(c => c.Id);

            comment.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            var replacement = builder.Entity<ReplacementLink>();
            replacement.ToTable(nameof(ReplacementLink));
            replacement.HasKey(r => r.Id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReplacementLink> ReplacementLinks { get; set; }
    }
}