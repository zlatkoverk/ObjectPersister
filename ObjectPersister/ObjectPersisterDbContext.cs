using System;
using Microsoft.EntityFrameworkCore;

namespace ObjectPersister
{
    public class ObjectPersisterDbContext : DbContext
    {
        public DbSet<ObjectDefinition> ObjectDefinitions { get; set; }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Enum> Enums { get; set; }

        public ObjectPersisterDbContext(DbContextOptions<ObjectPersisterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectDefinition>()
                .HasKey(m => m.Name);
            modelBuilder.Entity<ObjectDefinition>()
                .HasMany(m => m.Properties).WithOne(m => m.ObjectDefinition);

            modelBuilder.Entity<PropertyDefinition>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<PropertyDefinition>()
                .Property(m => m.Name);
            modelBuilder.Entity<PropertyDefinition>()
                .Property(m => m.Nullable);
            modelBuilder.Entity<PropertyDefinition>()
                .Property(m => m.Type);
            // Legal Values and nullable

            modelBuilder.Entity<Object>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Object>()
                .HasOne(m => m.Definition).WithMany(m => m.Objects);
            modelBuilder.Entity<Object>()
                .HasMany(m => m.Properties);

            modelBuilder.Entity<Property>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Property>()
                .HasOne(m => m.Definition);
        }
    }
}