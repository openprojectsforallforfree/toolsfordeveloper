using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class PredefinedDropdownMap : EntityTypeConfiguration<PredefinedDropdown>
    {
        public PredefinedDropdownMap()
        {
            // Primary Key
            this.HasKey(t => t.PredefinedDropdownId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PredefinedDropdown");
            this.Property(t => t.PredefinedDropdownId).HasColumnName("PredefinedDropdownId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.ParentTableId).HasColumnName("ParentTableId");
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
