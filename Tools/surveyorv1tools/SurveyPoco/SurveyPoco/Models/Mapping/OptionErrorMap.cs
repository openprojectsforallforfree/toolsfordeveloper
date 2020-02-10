using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SurveyCore.Models;
namespace SurveyRepository.Mapping
{
    public class OptionErrorMap : EntityTypeConfiguration<OptionError>
    {
        public OptionErrorMap()
        {
            // Primary Key
            this.HasKey(t => t.OptionErrorId);

            // Properties
            // Table & Column Mappings
            this.ToTable("OptionError");
            this.Property(t => t.OptionErrorId).HasColumnName("OptionErrorId");
            this.Property(t => t.QuestionOptionId).HasColumnName("QuestionOptionId");
            this.Property(t => t.ErrorText).HasColumnName("ErrorText");
        }
    }
}
