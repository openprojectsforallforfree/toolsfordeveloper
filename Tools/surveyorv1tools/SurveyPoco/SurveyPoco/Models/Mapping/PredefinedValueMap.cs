using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class PredefinedValueMap : EntityTypeConfiguration<PredefinedValue>
    {
        public PredefinedValueMap()
        {
            // Primary Key
            this.HasKey(t => t.PredefinedValueId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PredefinedValue");
            this.Property(t => t.PredefinedValueId).HasColumnName("PredefinedValueId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Value).HasColumnName("Value");
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
