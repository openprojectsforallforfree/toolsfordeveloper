using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MatrixAnswerCommentMap : EntityTypeConfiguration<MatrixAnswerComment>
    {
        public MatrixAnswerCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.MatrixAnswerCommentId);

            // Properties
            this.Property(t => t.MatrixAnswerCommentId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.MatrixAnswerSetGuid)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("MatrixAnswerComment");
            this.Property(t => t.MatrixAnswerCommentId).HasColumnName("MatrixAnswerCommentId");
            this.Property(t => t.MatrixQuestionId).HasColumnName("MatrixQuestionId");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.MatrixAnswerSetGuid).HasColumnName("MatrixAnswerSetGuid");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasOptional(t => t.MatrixAnswerSet)
                .WithMany(t => t.MatrixAnswerComments)
                .HasForeignKey(d => d.MatrixAnswerSetGuid);

        }
    }
}
