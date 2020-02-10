using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class SurveyeeMap : EntityTypeConfiguration<Surveyee>
    {
        public SurveyeeMap()
        {
            // Primary Key
            this.HasKey(t => t.SurveyeeGuId);

            // Properties
            this.Property(t => t.SurveyeeGuId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Surveyee");
            this.Property(t => t.SurveyeeGuId).HasColumnName("SurveyeeGuId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
