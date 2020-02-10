using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class AnswerMap : EntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.AnswerId);

            // Properties
            this.Property(t => t.AnswerId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.SurveyeeGuid)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Answer");
            this.Property(t => t.AnswerId).HasColumnName("AnswerId");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.QuestionOptionId).HasColumnName("QuestionOptionId");
            this.Property(t => t.AnswerValue).HasColumnName("AnswerValue");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.SurveyeeGuid).HasColumnName("SurveyeeGuid");
            this.Property(t => t.MatrixColId).HasColumnName("MatrixColId");
            this.Property(t => t.MatrixRowId).HasColumnName("MatrixRowId");
            this.Property(t => t.MatrixQuestionId).HasColumnName("MatrixQuestionId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasOptional(t => t.Question)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.QuestionId);
            this.HasOptional(t => t.QuestionOption)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.QuestionOptionId);
            this.HasRequired(t => t.Surveyee)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.SurveyeeGuid);

        }
    }
}
