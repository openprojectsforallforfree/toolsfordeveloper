using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class SurveySectionMap : EntityTypeConfiguration<SurveySection>
    {
        public SurveySectionMap()
        {
            // Primary Key
            this.HasKey(t => t.SectionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("SurveySection");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Survey)
                .WithMany(t => t.SurveySections)
                .HasForeignKey(d => d.SurveyId);

        }
    }
}
