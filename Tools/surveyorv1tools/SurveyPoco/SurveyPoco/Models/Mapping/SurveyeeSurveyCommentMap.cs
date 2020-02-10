using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class SurveyeeSurveyCommentMap : EntityTypeConfiguration<SurveyeeSurveyComment>
    {
        public SurveyeeSurveyCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.SurveyCommentsId);

            // Properties
            this.Property(t => t.SurveyCommentsId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.SurveyeeGuid)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("SurveyeeSurveyComment");
            this.Property(t => t.SurveyCommentsId).HasColumnName("SurveyCommentsId");
            this.Property(t => t.SurveyeeGuid).HasColumnName("SurveyeeGuid");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasOptional(t => t.Surveyee)
                .WithMany(t => t.SurveyeeSurveyComments)
                .HasForeignKey(d => d.SurveyeeGuid);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SurveyeeSurveyComments)
                .HasForeignKey(d => d.UserId);

        }
    }
}
