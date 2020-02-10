using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class GroupSurveyMap : EntityTypeConfiguration<GroupSurvey>
    {
        public GroupSurveyMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupSurveyId);

            // Properties
            // Table & Column Mappings
            this.ToTable("GroupSurvey");
            this.Property(t => t.GroupSurveyId).HasColumnName("GroupSurveyId");
            this.Property(t => t.GroupId).HasColumnName("GroupId");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.Group)
                .WithMany(t => t.GroupSurveys)
                .HasForeignKey(d => d.GroupId);
            this.HasRequired(t => t.Survey)
                .WithMany(t => t.GroupSurveys)
                .HasForeignKey(d => d.SurveyId);

        }
    }
}
