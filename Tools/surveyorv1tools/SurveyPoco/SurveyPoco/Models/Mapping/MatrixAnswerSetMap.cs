using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MatrixAnswerSetMap : EntityTypeConfiguration<MatrixAnswerSet>
    {
        public MatrixAnswerSetMap()
        {
            // Primary Key
            this.HasKey(t => t.MatrixAnswerSetGuid);

            // Properties
            this.Property(t => t.MatrixAnswerSetGuid)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.AnswerId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("MatrixAnswerSet");
            this.Property(t => t.MatrixAnswerSetGuid).HasColumnName("MatrixAnswerSetGuid");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AnswerId).HasColumnName("AnswerId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasOptional(t => t.Answer)
                .WithMany(t => t.MatrixAnswerSets)
                .HasForeignKey(d => d.AnswerId);

        }
    }
}
