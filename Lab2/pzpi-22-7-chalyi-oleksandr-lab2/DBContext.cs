using Microsoft.EntityFrameworkCore;
using SmartLightSense.Models;

namespace SmartLightSense
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Streetlight> Streetlights { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<EnergyUsage> EnergyUsages { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.Password)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(100);

                entity.Property(u => u.PhoneNumber)
                    .HasMaxLength(15);

                entity.Property(u => u.LastLogin)
                    .IsRequired();
            });

            // Sensor configuration
            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.SensorType)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(s => s.InstallationDate)
                    .IsRequired();

                entity.Property(s => s.Status)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(s => s.LastUpdate)
                    .IsRequired();

                entity.HasOne(s => s.Streetlight)
                    .WithMany(st => st.Sensors)
                    .HasForeignKey(s => s.StreetlightId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Streetlight configuration
            modelBuilder.Entity<Streetlight>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.SectorId)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(s => s.InstallationDate)
                    .IsRequired();

                entity.Property(s => s.Status)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(s => s.BrightnessLevel)
                    .IsRequired();

                entity.Property(s => s.LastMaintenanceDate)
                    .IsRequired();
            });

            // MaintenanceLog configuration
            modelBuilder.Entity<MaintenanceLog>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Date)
                    .IsRequired();

                entity.Property(m => m.IssueReported)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(m => m.ActionTaken)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(m => m.Status)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(m => m.Streetlight)
                    .WithMany()
                    .HasForeignKey(m => m.StreetlightId)
                    .OnDelete(DeleteBehavior.Cascade); 

                entity.HasOne(m => m.Technician)
                    .WithMany()
                    .HasForeignKey(m => m.TechnicianId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // EnergyUsage configuration
            modelBuilder.Entity<EnergyUsage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.Property(e => e.EnergyConsumed)
                    .IsRequired();

                entity.HasOne(e => e.Streetlight)
                    .WithMany()
                    .HasForeignKey(e => e.StreetlightId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Alert configuration
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.AlertType)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(a => a.Message)
                    .HasMaxLength(1000)
                    .IsRequired(false);

                entity.Property(a => a.AlertDateTime)
                    .IsRequired();

                entity.Property(a => a.Resolved)
                    .IsRequired();

                entity.HasOne(a => a.Streetlight)
                    .WithMany()
                    .HasForeignKey(a => a.StreetlightId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(a => a.Sensor)
                    .WithMany()
                    .HasForeignKey(a => a.SensorId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            // WeatherData configuration
            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.HasKey(w => w.Id);

                entity.Property(w => w.Date)
                    .IsRequired();

                entity.Property(w => w.Visibility)
                    .IsRequired();
            });
        }
    }
}
