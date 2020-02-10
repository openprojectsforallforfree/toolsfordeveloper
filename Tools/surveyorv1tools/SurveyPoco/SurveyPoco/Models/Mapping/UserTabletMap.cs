using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class UserTabletMap : EntityTypeConfiguration<UserTablet>
    {
        public UserTabletMap()
        {
            // Primary Key
            this.HasKey(t => t.UserTabletId);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserTablet");
            this.Property(t => t.UserTabletId).HasColumnName("UserTabletId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TabletId).HasColumnName("TabletId");
            this.Property(t => t.GroupSurveyId).HasColumnName("GroupSurveyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.GroupSurvey)
                .WithMany(t => t.UserTablets)
                .HasForeignKey(d => d.GroupSurveyId);
            this.HasRequired(t => t.Tablet)
                .WithMany(t => t.UserTablets)
                .HasForeignKey(d => d.TabletId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserTablets)
                .HasForeignKey(d => d.UserId);

        }
    }
}
