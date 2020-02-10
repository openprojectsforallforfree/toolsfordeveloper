using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MatrixRowMap : EntityTypeConfiguration<MatrixRow>
    {
        public MatrixRowMap()
        {
            // Primary Key
            this.HasKey(t => t.RowId);

            // Properties
            // Table & Column Mappings
            this.ToTable("MatrixRow");
            this.Property(t => t.RowId).HasColumnName("RowId");
            this.Property(t => t.RowLabel).HasColumnName("RowLabel");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.MatrixRows)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
