using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class OptionChoiceMap : EntityTypeConfiguration<OptionChoice>
    {
        public OptionChoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.OptionChoiceId);

            // Properties
            // Table & Column Mappings
            this.ToTable("OptionChoice");
            this.Property(t => t.OptionChoiceId).HasColumnName("OptionChoiceId");
            this.Property(t => t.OptionChoiceLabel).HasColumnName("OptionChoiceLabel");
            this.Property(t => t.IsDropdown).HasColumnName("IsDropdown");
        }
    }
}
