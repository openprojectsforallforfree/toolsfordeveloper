using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MatrixAnswerMap : EntityTypeConfiguration<MatrixAnswer>
    {
        public MatrixAnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.MatrixAnswerId);

            // Properties
            this.Property(t => t.MatrixAnswerId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.AnswerId)
                .HasMaxLength(128);

            this.Property(t => t.MatrixAnswerSet_MatrixAnswerSetGuid)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("MatrixAnswer");
            this.Property(t => t.MatrixAnswerId).HasColumnName("MatrixAnswerId");
            this.Property(t => t.MatrixQuestionId).HasColumnName("MatrixQuestionId");
            this.Property(t => t.QuestionOptionId).HasColumnName("QuestionOptionId");
            this.Property(t => t.AnswerValue).HasColumnName("AnswerValue");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.AnswerId).HasColumnName("AnswerId");
            this.Property(t => t.AnswerSetId).HasColumnName("AnswerSetId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.MatrixAnswerSet_MatrixAnswerSetGuid).HasColumnName("MatrixAnswerSet_MatrixAnswerSetGuid");

            // Relationships
            this.HasOptional(t => t.Answer)
                .WithMany(t => t.MatrixAnswers)
                .HasForeignKey(d => d.AnswerId);
            this.HasOptional(t => t.MatrixAnswerSet)
                .WithMany(t => t.MatrixAnswers)
                .HasForeignKey(d => d.MatrixAnswerSet_MatrixAnswerSetGuid);
            this.HasOptional(t => t.MatrixQuestion)
                .WithMany(t => t.MatrixAnswers)
                .HasForeignKey(d => d.MatrixQuestionId);
            this.HasOptional(t => t.QuestionOption)
                .WithMany(t => t.MatrixAnswers)
                .HasForeignKey(d => d.QuestionOptionId);

        }
    }
}
