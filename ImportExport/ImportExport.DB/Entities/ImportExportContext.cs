using Microsoft.EntityFrameworkCore;

namespace ImportExport.DB.Entities
{
    public class ImportExportContext : DbContext
    {

        public DbSet<Site> Site { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<Charger> Charger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB; Initial Catalog = ImportExport; Integrated Security = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Site Configration
            modelBuilder.Entity<Site>().ToTable("site");
            modelBuilder.Entity<Site>().HasKey(n => n.siteId);
            modelBuilder.Entity<Site>().Property(n => n.siteId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Site>().Property(n => n.siteName).IsRequired().HasColumnType("nvarchar(100)");

            //Station Configration

            modelBuilder.Entity<Station>().ToTable("station");
            modelBuilder.Entity<Station>().HasKey(n => n.stationId);
            modelBuilder.Entity<Station>().Property(n => n.stationId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Station>().Property(n => n.stationName).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Station>().Property(n => n.stationBoxId).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Station>().Property(n => n.stationUuId).IsRequired().HasColumnType("nvarchar(100)");

            //Charger Configration
            modelBuilder.Entity<Charger>().ToTable("charger");
            modelBuilder.Entity<Charger>().HasKey(n => n.chargerId);
            modelBuilder.Entity<Charger>().Property(n => n.chargerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Charger>().Property(n => n.chargerUuid).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Charger>().Property(n => n.chargerFriendlyName).IsRequired().HasColumnType("nvarchar(100)");

            //Station Constraints
            modelBuilder.Entity<Station>().HasOne(s => s.Site).WithMany(st => st.stations)
                .HasForeignKey(si => si.siteId).HasConstraintName("ForeignKey_Station_Site");

            modelBuilder.Entity<Station>()
            .HasIndex(p => new { p.stationName, p.stationBoxId, p.stationUuId })
            .IsUnique(true);

            //Charger Constraints
            modelBuilder.Entity<Charger>().HasOne(st => st.Station).WithMany(c => c.chargers)
                .HasForeignKey(si => si.stationId).HasConstraintName("ForeignKey_Charger_Station");
            modelBuilder.Entity<Charger>()
            .HasIndex(c => new { c.chargerUuid, c.chargerFriendlyName })
            .IsUnique(true);

        }
    }
}
