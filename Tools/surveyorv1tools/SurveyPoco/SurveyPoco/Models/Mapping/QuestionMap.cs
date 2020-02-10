using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Question");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.PrecedingQuestionId).HasColumnName("PrecedingQuestionId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.QuestionGroup).HasColumnName("QuestionGroup");
            this.Property(t => t.QuestionText).HasColumnName("QuestionText");
            this.Property(t => t.Required).HasColumnName("Required");
            this.Property(t => t.OnlyNumericValue).HasColumnName("OnlyNumericValue");
            this.Property(t => t.IncludeComment).HasColumnName("IncludeComment");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.QuestionOrder).HasColumnName("QuestionOrder");
            this.Property(t => t.DependentQuestionId).HasColumnName("DependentQuestionId");
            this.Property(t => t.DependentQuestionOptionId).HasColumnName("DependentQuestionOptionId");
            this.Property(t => t.AllowMultipleChoice).HasColumnName("AllowMultipleChoice");
            this.Property(t => t.HasPredefinedDropdown).HasColumnName("HasPredefinedDropdown");
            this.Property(t => t.PredefinedDropdownId).HasColumnName("PredefinedDropdownId");
            this.Property(t => t.IsUpdated).HasColumnName("IsUpdated");
            this.Property(t => t.NoOfRows).HasColumnName("NoOfRows");
            this.Property(t => t.SectionId).HasColumnName("SectionId");
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
            this.HasRequired(t => t.PredefinedDropdown)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.PredefinedDropdownId);
            this.HasRequired(t => t.QuestionType)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.QuestionTypeId);
            this.HasRequired(t => t.SurveySection)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.SectionId);

        }
    }
}
