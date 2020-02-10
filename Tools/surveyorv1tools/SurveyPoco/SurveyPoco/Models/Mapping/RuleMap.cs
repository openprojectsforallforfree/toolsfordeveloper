using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class RuleMap : EntityTypeConfiguration<Rule>
    {
        public RuleMap()
        {
            // Primary Key
            this.HasKey(t => t.RuleId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Rule");
            this.Property(t => t.RuleId).HasColumnName("RuleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
