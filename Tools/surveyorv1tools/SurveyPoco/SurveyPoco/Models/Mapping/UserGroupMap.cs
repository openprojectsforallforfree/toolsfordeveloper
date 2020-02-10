using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class UserGroupMap : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.UserGroupId);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserGroup");
            this.Property(t => t.UserGroupId).HasColumnName("UserGroupId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.GroupId).HasColumnName("GroupId");
            this.Property(t => t.IsSuperVisor).HasColumnName("IsSuperVisor");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Group)
                .WithMany(t => t.UserGroups)
                .HasForeignKey(d => d.GroupId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserGroups)
                .HasForeignKey(d => d.UserId);

        }
    }
}
