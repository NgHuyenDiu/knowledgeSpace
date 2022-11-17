using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeSpace.BackendServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                if (item.Entity is IDateTracking changedOrAddedItem)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        changedOrAddedItem.LastModifiedDate = DateTime.Now;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<User>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<User>().Property(x => x.DeleteState).HasDefaultValue(false);
            builder.Entity<LabelInKnowledgeBase>()
                        .HasKey(c => new { c.LabelId, c.KnowledgeBaseId });

            builder.Entity<Permission>()
                       .HasKey(c => new { c.RoleId, c.FunctionId, c.CommandId });

            builder.Entity<Vote>()
                        .HasKey(c => new { c.KnowledgeBaseId, c.UserId });

            builder.Entity<CommandInFunction>()
                       .HasKey(c => new { c.CommandId, c.FunctionId });

            builder.HasSequence("KnowledgeBaseSequence");

            builder.Entity<Permission>(entity =>
            {
                entity.HasOne(s => s.role)
                      .WithMany(g => g.Permisstions)
                      .HasForeignKey(s => s.RoleId);
                entity.HasOne(s => s.command)
                      .WithMany(g => g.Permisstions)
                      .HasForeignKey(s => s.CommandId);
                entity.HasOne(s => s.function)
                      .WithMany(g => g.Permisstions)
                      .HasForeignKey(s => s.FunctionId);
            });

            builder.Entity<CommandInFunction>(entity =>
            {
               
                entity.HasOne(s => s.command)
                      .WithMany(g => g.commandInFunctions)
                      .HasForeignKey(s => s.CommandId);
                entity.HasOne(s => s.function)
                      .WithMany(g => g.commandInFunctions)
                      .HasForeignKey(s => s.FunctionId);
            });

            builder.Entity<ActivityLog>(entity =>
            {

                entity.HasOne(s => s.user)
                      .WithMany(g => g.ActivityLogs)
                      .HasForeignKey(s => s.UserId);
                
            });

            builder.Entity<Report>(entity =>
            {

                entity.HasOne(s => s.user)
                      .WithMany(g => g.Reports)
                      .HasForeignKey(s => s.ReportUserId);

                entity.HasOne(s => s.knowledgeBase)
                     .WithMany(g => g.Reports)
                     .HasForeignKey(s => s.KnowledgeBaseId);

            });

            builder.Entity<Comment>(entity =>
            {

                entity.HasOne(s => s.user)
                      .WithMany(g => g.Comments)
                      .HasForeignKey(s => s.OwnerUserId);

            });

            builder.Entity<Vote>(entity =>
            {

                entity.HasOne(s => s.user)
                      .WithMany(g => g.Votes)
                      .HasForeignKey(s => s.UserId);

                entity.HasOne(s => s.knowledgeBase)
                      .WithMany(g => g.Votes)
                      .HasForeignKey(s => s.KnowledgeBaseId);

            });

           
            builder.Entity<KnowledgeBase>(entity =>
            {

                entity.HasOne(s => s.category)
                      .WithMany(g => g.knowledgeBases)
                      .HasForeignKey(s => s.CategoryId);

            });


            builder.Entity<Comment>(entity =>
            {

                entity.HasOne(s => s.knowledgeBase)
                      .WithMany(g => g.Comments)
                      .HasForeignKey(s => s.KnowledgeBaseId);

            });

            builder.Entity<Attachment>(entity =>
            {

                entity.HasOne(s => s.knowledgeBase)
                      .WithMany(g => g.Attachments)
                      .HasForeignKey(s => s.KnowledgeBaseId);

            });


            builder.Entity<LabelInKnowledgeBase>(entity =>
            {

                entity.HasOne(s => s.knowledgeBase)
                      .WithMany(g => g.LabelInKnowledgeBases)
                      .HasForeignKey(s => s.KnowledgeBaseId);

                entity.HasOne(s => s.label)
                     .WithMany(g => g.LabelInKnowledgeBases)
                     .HasForeignKey(s => s.LabelId);

            });

        }

        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }

        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<KnowledgeBase> KnowledgeBases { set; get; }
        public DbSet<Label> Labels { set; get; }
        public DbSet<LabelInKnowledgeBase> LabelInKnowledgeBases { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Report> Reports { set; get; }
        public DbSet<Vote> Votes { set; get; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}