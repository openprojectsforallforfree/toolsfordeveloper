using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class DemoOrderMap : EntityTypeConfiguration<DemoOrder>
    {
        public DemoOrderMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.OrderNo, t.HeaderId });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.OrderNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HeaderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DemoOrder");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.HeaderId).HasColumnName("HeaderId");
        }
    }
}
