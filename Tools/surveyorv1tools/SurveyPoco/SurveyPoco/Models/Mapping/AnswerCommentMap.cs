using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class AnswerCommentMap : EntityTypeConfiguration<AnswerComment>
    {
        public AnswerCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionCommentId);

            // Properties
            this.Property(t => t.QuestionCommentId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.SurveyeeGuid)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("AnswerComment");
            this.Property(t => t.QuestionCommentId).HasColumnName("QuestionCommentId");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.SurveyeeGuid).HasColumnName("SurveyeeGuid");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Surveyee)
                .WithMany(t => t.AnswerComments)
                .HasForeignKey(d => d.SurveyeeGuid);

        }
    }
}
