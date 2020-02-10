using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MatrixQuestionMap : EntityTypeConfiguration<MatrixQuestion>
    {
        public MatrixQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.MatrixQuestionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("MatrixQuestion");
            this.Property(t => t.MatrixQuestionId).HasColumnName("MatrixQuestionId");
            this.Property(t => t.PrecedingQuestionId).HasColumnName("PrecedingQuestionId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.QuestionGroup).HasColumnName("QuestionGroup");
            this.Property(t => t.QuestionText).HasColumnName("QuestionText");
            this.Property(t => t.Required).HasColumnName("Required");
            this.Property(t => t.OnlyNumericValue).HasColumnName("OnlyNumericValue");
            this.Property(t => t.IncludeComment).HasColumnName("IncludeComment");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.MatrixOrder).HasColumnName("MatrixOrder");
            this.Property(t => t.DependentQuestionId).HasColumnName("DependentQuestionId");
            this.Property(t => t.DependentQuestionOptionId).HasColumnName("DependentQuestionOptionId");
            this.Property(t => t.AllowMultipleChoice).HasColumnName("AllowMultipleChoice");
            this.Property(t => t.HasPredefinedDropdown).HasColumnName("HasPredefinedDropdown");
            this.Property(t => t.PredefinedDropdownId).HasColumnName("PredefinedDropdownId");
            this.Property(t => t.IsUpdated).HasColumnName("IsUpdated");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.QuestionTypeId).HasColumnName("QuestionTypeId");
            this.Property(t => t.RuleId).HasColumnName("RuleId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsOther).HasColumnName("IsOther");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.MatrixQuestions)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
