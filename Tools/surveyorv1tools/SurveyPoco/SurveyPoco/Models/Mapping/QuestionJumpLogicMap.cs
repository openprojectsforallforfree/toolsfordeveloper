using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class QuestionJumpLogicMap : EntityTypeConfiguration<QuestionJumpLogic>
    {
        public QuestionJumpLogicMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionJumpLogicId);

            // Properties
            // Table & Column Mappings
            this.ToTable("QuestionJumpLogic");
            this.Property(t => t.QuestionJumpLogicId).HasColumnName("QuestionJumpLogicId");
            this.Property(t => t.JumpQuestionId).HasColumnName("JumpQuestionId");
            this.Property(t => t.JumpQuestionOptionId).HasColumnName("JumpQuestionOptionId");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.MatrixQuestion_MatrixQuestionId).HasColumnName("MatrixQuestion_MatrixQuestionId");

            // Relationships
            this.HasOptional(t => t.MatrixQuestion)
                .WithMany(t => t.QuestionJumpLogics)
                .HasForeignKey(d => d.MatrixQuestion_MatrixQuestionId);
            this.HasRequired(t => t.Question)
                .WithMany(t => t.QuestionJumpLogics)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
