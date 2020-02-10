using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SurveyCore.Models;
using SurveyRepository.Mapping;

namespace SurveyPoco.Models
{
    public partial class DB_9F3BD3_SURVEYContext : DbContext
    {
        static DB_9F3BD3_SURVEYContext()
        {
            Database.SetInitializer<DB_9F3BD3_SURVEYContext>(null);
        }

        public DB_9F3BD3_SURVEYContext()
            : base("Name=DB_9F3BD3_SURVEYContext")
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerComment> AnswerComments { get; set; }
        public DbSet<DemoOrder> DemoOrders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSurvey> GroupSurveys { get; set; }
        public DbSet<MatrixAnswer> MatrixAnswers { get; set; }
        public DbSet<MatrixAnswerComment> MatrixAnswerComments { get; set; }
        public DbSet<MatrixAnswerSet> MatrixAnswerSets { get; set; }
        public DbSet<MatrixCol> MatrixCols { get; set; }
        public DbSet<MatrixQuestion> MatrixQuestions { get; set; }
        public DbSet<MatrixRow> MatrixRows { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OptionChoice> OptionChoices { get; set; }
        public DbSet<OptionError> OptionErrors { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PredefinedDropdown> PredefinedDropdowns { get; set; }
        public DbSet<PredefinedDropdownValue> PredefinedDropdownValues { get; set; }
        public DbSet<PredefinedValue> PredefinedValues { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionJumpLogic> QuestionJumpLogics { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Surveyee> Surveyees { get; set; }
        public DbSet<SurveyeeSurveyComment> SurveyeeSurveyComments { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<Tablet> Tablets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserTablet> UserTablets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnswerMap());
            modelBuilder.Configurations.Add(new AnswerCommentMap());
            modelBuilder.Configurations.Add(new DemoOrderMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new GroupSurveyMap());
            modelBuilder.Configurations.Add(new MatrixAnswerMap());
            modelBuilder.Configurations.Add(new MatrixAnswerCommentMap());
            modelBuilder.Configurations.Add(new MatrixAnswerSetMap());
            modelBuilder.Configurations.Add(new MatrixColMap());
            modelBuilder.Configurations.Add(new MatrixQuestionMap());
            modelBuilder.Configurations.Add(new MatrixRowMap());
            modelBuilder.Configurations.Add(new MenuItemMap());
            modelBuilder.Configurations.Add(new OptionChoiceMap());
            modelBuilder.Configurations.Add(new OptionErrorMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new PredefinedDropdownMap());
            modelBuilder.Configurations.Add(new PredefinedDropdownValueMap());
            modelBuilder.Configurations.Add(new PredefinedValueMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new QuestionJumpLogicMap());
            modelBuilder.Configurations.Add(new QuestionOptionMap());
            modelBuilder.Configurations.Add(new QuestionTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RuleMap());
            modelBuilder.Configurations.Add(new SurveyMap());
            modelBuilder.Configurations.Add(new SurveyeeMap());
            modelBuilder.Configurations.Add(new SurveyeeSurveyCommentMap());
            modelBuilder.Configurations.Add(new SurveySectionMap());
            modelBuilder.Configurations.Add(new TabletMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new UserTabletMap());
        }
    }
}
