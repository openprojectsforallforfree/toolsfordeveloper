using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class PredefinedDropdownValueMap : EntityTypeConfiguration<PredefinedDropdownValue>
    {
        public PredefinedDropdownValueMap()
        {
            // Primary Key
            this.HasKey(t => t.PredefinedDropdownValueId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PredefinedDropdownValue");
            this.Property(t => t.PredefinedDropdownValueId).HasColumnName("PredefinedDropdownValueId");
            this.Property(t => t.PredefinedDropdownId).HasColumnName("PredefinedDropdownId");
            this.Property(t => t.ParentValueId).HasColumnName("ParentValueId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.PDDValue).HasColumnName("PDDValue");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.DeletedOn).HasColumnName("DeletedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            this.HasRequired(t => t.PredefinedDropdown)
                .WithMany(t => t.PredefinedDropdownValues)
                .HasForeignKey(d => d.PredefinedDropdownId);

        }
    }
}
