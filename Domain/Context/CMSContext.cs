using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Context
{
    public partial class CMSContext : DbContext
    {
        public CMSContext()
        {
        }

        public CMSContext(DbContextOptions<CMSContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Cmsmodule> Cmsmodule { get; set; }
        public virtual DbSet<Component> Component { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageAction> PageAction { get; set; }
        public virtual DbSet<PageSection> PageSection { get; set; }
        public virtual DbSet<PageSetup> PageSetup { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<PolicyRole> PolicyRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TemplatePage> TemplatePage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Widget> Widget { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(new ConfigurationManger().SqlDataConnection);
            }

        }
        public override int SaveChanges()
        {
            try
            {
                var entities = (from entry in ChangeTracker.Entries()
                                where entry.State == EntityState.Modified || entry.State == EntityState.Added
                                select entry.Entity);

                var validationResults = new List<ValidationResult>();
                foreach (var entity in entities)
                {
                    if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                    {
                        throw new ValidationException();
                    }
                }
                try
                {
                    return base.SaveChanges();
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cmsmodule>(entity =>
            {
                entity.ToTable("CMSModule", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_cmsmoduleisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.ToTable("Component", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_componentisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_componentispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Html).IsRequired();

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Component)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ComponentModuleID_ModuleID");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.ToTable("Configuration", "Setup");

                entity.HasIndex(e => e.IsCached)
                    .HasName("idx_configurationiscached");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_configurationisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CmsmoduleId).HasColumnName("CMSModuleID");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("json");

                entity.HasOne(d => d.Cmsmodule)
                    .WithMany(p => p.Configuration)
                    .HasForeignKey(d => d.CmsmoduleId)
                    .HasConstraintName("FK_ConfigurationCMSModuleID_CMSModuleID");
            });

            modelBuilder.Entity<Lookup>(entity =>
            {
                entity.ToTable("Lookup", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_lookupisdeleted");

                entity.HasIndex(e => e.Order)
                    .HasName("idx_lookuporder");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Setup\".\"lookup_ID_seq\"'::regclass)");

                entity.Property(e => e.CmsmoduleId).HasColumnName("CMSModuleID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.MajorCode)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.MinorCode)
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.Property(e => e.Value).HasColumnType("character varying");

                entity.HasOne(d => d.Cmsmodule)
                    .WithMany(p => p.Lookup)
                    .HasForeignKey(d => d.CmsmoduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupCMSModuleID_CMSModuleID");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_menuisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_menuispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.RouteLink)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.MenuNavigation)
                    .WithMany(p => p.InverseMenuNavigation)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MenuMenuID_MenuID");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_moduleisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_moduleispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Page", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_pageisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.PageSetupId).HasColumnName("PageSetupID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.PageSetup)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.PageSetupId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PagePageSetupID_PageSetupID");
            });

            modelBuilder.Entity<PageAction>(entity =>
            {
                entity.ToTable("PageAction", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_pageactionisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.PageSectionId).HasColumnName("PageSectionID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageAction)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PageActionPageID_PageID");

                entity.HasOne(d => d.PageSection)
                    .WithMany(p => p.PageAction)
                    .HasForeignKey(d => d.PageSectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PageActionSectionID_PageSectionID");
            });

            modelBuilder.Entity<PageSection>(entity =>
            {
                entity.ToTable("PageSection", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_pagesectionisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.Property(e => e.WidgetId).HasColumnName("WidgetID");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.PageSection)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PageSectionModuleID_ModuleID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageSection)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PageSectionPageID_PageID");

                entity.HasOne(d => d.Widget)
                    .WithMany(p => p.PageSection)
                    .HasForeignKey(d => d.WidgetId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PageSectionWidgetID_WidgetID");
            });

            modelBuilder.Entity<PageSetup>(entity =>
            {
                entity.ToTable("PageSetup", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_pagesetupisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_pagesetupispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Header).HasColumnType("character varying");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Seo).HasColumnType("character varying");

                entity.Property(e => e.Slug).HasColumnType("character varying");

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.PageSetup)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PageSetupMenuID_MenuID");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "Permission");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_permissionisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PageActionId).HasColumnName("PageActionID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.PageAction)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.PageActionId)
                    .HasConstraintName("FK_PermissionPageActionID_PageActionID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_PermissionRoleID_RoleID");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("Policy", "Permission");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_policyisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<PolicyRole>(entity =>
            {
                entity.ToTable("PolicyRole", "Permission");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_policyroleisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyRole)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_PolicyRolePolicyID_PolicyID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PolicyRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_PolicyRoleRoleID_RoleID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Permission");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_roleisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_templateisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_templateispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TemplateCssPath)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");
            });

            modelBuilder.Entity<TemplatePage>(entity =>
            {
                entity.ToTable("TemplatePage", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_templatepageisdeleted");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Setup\".\"TemplatePageID\"'::regclass)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PageSetupId).HasColumnName("PageSetupID");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.PageSetup)
                    .WithMany(p => p.TemplatePage)
                    .HasForeignKey(d => d.PageSetupId)
                    .HasConstraintName("FK_TemplatePagePageSetupID_PageSetupID");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.TemplatePage)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TemplatePageTemplateID_TemplateID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Production");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_userisdeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("character varying");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PhoneNumber).HasColumnType("character varying");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.ProfilePicture).HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_UserPolicyID_PolicyID");
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.ToTable("Widget", "Setup");

                entity.HasIndex(e => e.IsDeleted)
                    .HasName("idx_widgetisdeleted");

                entity.HasIndex(e => e.IsPublished)
                    .HasName("idx_widgetispublished");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdatedBy).HasColumnType("character varying");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.Widget)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_WidgetComponentID_ComponentID");
            });

            modelBuilder.HasSequence("Permission_ID_seq", "Permission");

            modelBuilder.HasSequence("Policy_ID_seq", "Permission");

            modelBuilder.HasSequence("Role_ID_seq", "Permission");

            modelBuilder.HasSequence("User_ID_seq", "Production");

            modelBuilder.HasSequence("CMSModule_ID_seq", "Setup");

            modelBuilder.HasSequence("Configuration_ID_seq", "Setup");

            modelBuilder.HasSequence("lookup_ID_seq", "Setup");

            modelBuilder.HasSequence("PageAction_ID_seq", "Setup");

            modelBuilder.HasSequence("TemplatePageID", "Setup");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
