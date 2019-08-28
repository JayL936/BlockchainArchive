using System;
using System.Collections.Generic;
using System.Text;
using BlockchainArchive.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlockchainArchive.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<File> Files { get; set; }
        public DbSet<BlockchainHistory> HistoryEntries { get; set; }
        public DbSet<StoredContract> StoredContracts { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<FileOwner> FileOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FileOwner>()
                .HasKey(fo => new { fo.FileGuid, fo.OwnerId });

            builder.Entity<FileOwner>()
                .HasOne(f => f.File)
                .WithMany(fo => fo.FileOwners)
                .HasForeignKey(fo => fo.FileGuid);

            builder.Entity<FileOwner>()
                .HasOne(fo => fo.Owner)
                .WithMany(f => f.OwnedFiles)
                .HasForeignKey(fo => fo.OwnerId);            
        }
    }
}
