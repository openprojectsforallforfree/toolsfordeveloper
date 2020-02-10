using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class MenuItemMap : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("MenuItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Controller).HasColumnName("Controller");
            this.Property(t => t.LinkText).HasColumnName("LinkText");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
        }
    }
}
