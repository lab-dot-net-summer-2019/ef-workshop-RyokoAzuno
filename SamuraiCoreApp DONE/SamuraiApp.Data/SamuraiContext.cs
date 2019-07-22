using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext()
        {
        }

        public SamuraiContext(DbContextOptions<SamuraiContext> options)
            : base(options)
        { }
        

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Katana> Katanas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Samurai entity
            modelBuilder.Entity<Samurai>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Samurai>()
                .HasOne(s => s.SecretIdentity)
                .WithOne(si => si.Samurai)
                .HasForeignKey<SecretIdentity>(si => si.SamuraiId);

            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Quotes)
                .WithOne(q => q.Samurai)
                .HasForeignKey(q => q.SamuraiId);

            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.SamuraiBattles)
                .WithOne(sb => sb.Samurai)
                .HasForeignKey(sb => sb.SamuraiId);

            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Katanas)
                .WithOne(k => k.Samurai)
                .HasForeignKey(k => k.SamuraiId);

            modelBuilder.Entity<Samurai>()
                .Property(s => s.Name).HasMaxLength(30).IsRequired();

            // Battle entity
            modelBuilder.Entity<Battle>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Battle>()
                .Property(s => s.Name).HasMaxLength(30);
            // Quote entity
            modelBuilder.Entity<Quote>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Quote>()
                .Property(q => q.Text).HasMaxLength(10);

            // SamuraiBattle entity
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleId, s.SamuraiId });

            //modelBuilder.Entity<SamuraiBattle>()
            //    .Property(sb => sb.KillStreak);

            modelBuilder.Entity<SamuraiBattle>()
                .HasOne(sb => sb.Battle)
                .WithMany(b => b.SamuraiBattles)
                .HasForeignKey(sb => new { sb.BattleId });

            modelBuilder.Entity<SamuraiBattle>()
                .HasOne(sb => sb.Samurai)
                .WithMany(s => s.SamuraiBattles)
                .HasForeignKey(sb => new { sb.SamuraiId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
                 "Server=(localdb)\\MSSQLLocalDB;Database=SamuraiAppDataCore;Trusted_Connection=True;");
        }
    }
}
