using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class TabletMap : EntityTypeConfiguration<Tablet>
    {
        public TabletMap()
        {
            // Primary Key
            this.HasKey(t => t.TabletId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Tablet");
            this.Property(t => t.TabletId).HasColumnName("TabletId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Mac).HasColumnName("Mac");
            this.Property(t => t.OperatingSystem).HasColumnName("OperatingSystem");
            this.Property(t => t.Description).HasColumnName("Description");
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
