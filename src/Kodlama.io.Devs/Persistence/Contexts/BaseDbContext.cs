using Application.Enums;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            IEnumerable<EntityEntry<Entity>> datas = ChangeTracker.Entries<Entity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasIndex(p => p.Name).IsUnique();
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasIndex(p => p.Name).IsUnique();
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasOne(p => p.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.HasIndex(p => p.Email).IsUnique();
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p => p.Status).HasColumnName("Status");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasMany(p => p.UserOperationClaims);
                a.HasMany(p => p.RefreshTokens);
            });

            modelBuilder.Entity<Applicant>(a =>
            {
                a.ToTable("Applicants");
                a.Property(p => p.GitHubAddress).HasColumnName("GitHubAddress");
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasIndex(p => p.Name).IsUnique();
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasIndex(p => new { p.UserId, p.OperationClaimId }).IsUnique();
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);
            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshTokens").HasKey(k => k.Id);
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.Token).HasColumnName("Token");
                a.Property(p => p.Expires).HasColumnName("Expires");
                a.Property(p => p.Created).HasColumnName("Created");
                a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(p => p.Revoked).HasColumnName("Revoked");
                a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                a.HasOne(p => p.User);
            });

            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "Python") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

            Technology[] technologySeeds = { new(1, 1, "ASP.NET"), new(2, 1, "WPF") };
            modelBuilder.Entity<Technology>().HasData(technologySeeds);

            OperationClaim[] operationClaimSeeds = { new(1, ClaimRoles.admin.ToString()) };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeeds);
        }
    }
}
