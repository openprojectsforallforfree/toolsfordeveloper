using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class QuestionTypeMap : EntityTypeConfiguration<QuestionType>
    {
        public QuestionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionTypeId);

            // Properties
            // Table & Column Mappings
            this.ToTable("QuestionType");
            this.Property(t => t.QuestionTypeId).HasColumnName("QuestionTypeId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
