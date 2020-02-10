using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class SurveyMap : EntityTypeConfiguration<Survey>
    {
        public SurveyMap()
        {
            // Primary Key
            this.HasKey(t => t.SurveyId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Survey");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.UniqueId).HasColumnName("UniqueId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.OrganizationId).HasColumnName("OrganizationId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Surveys)
                .HasForeignKey(d => d.OrganizationId);

        }
    }
}
