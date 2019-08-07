using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiDB.Domain.Table;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Azure.Mobile.Server.Tables;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace FestiDB.Persistence
{

    public class FestiContext : IdentityDbContext<UserAccount>
    {
        public FestiContext(string getConnectionString) : base(getConnectionString)
        {

        }

        public DbSet<Advice> Advices { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DrawQuestion> DrawQuestions { get; set; }
        public DbSet<DrawQuestionAnswer> DrawQuestionAnswers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }
        public DbSet<MeasureQuestion> MeasureQuestions { get; set; }
        public DbSet<MeasureQuestionAnswer> MeasureQuestionAnswers { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<MultipleChoiceQuestionAnswer> MultipleChoiceQuestionAnswers { get; set; }
        public DbSet<OpenQuestion> OpenQuestions { get; set; }
        public DbSet<OpenQuestionAnswer> OpenQuestionAnswers { get; set; }
        public DbSet<PictureQuestion> PictureQuestions { get; set; }
        public DbSet<PictureQuestionAnswer> PictureQuestionAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questionnaire> Questionaires { get; set; }
        public DbSet<ScaleQuestion> ScaleQuestions { get; set; }
        public DbSet<ScaleQuestionAnswer> ScaleQuestionAnswers { get; set; }
        public DbSet<TableQuestion> TableQuestions { get; set; }
        public DbSet<TableQuestionAnswer> TableQuestionsAnswers { get; set; }
        public DbSet<TableQuestionAnswerEntry> TableQuestionAnswerEntries { get; set; }
        public DbSet<User> FestiUsers { get; set; }
        public DbSet<MultipleChoiceQuestionOption> MultipleChoiceQuestionOptions { get; set; }

        public DbSet<TableQuestionMultipleColumn> TableQuestionMultipleColumns { get; set; }

        public DbSet<TableQuestionNumberColumn> TableQuestionNumberColumns { get; set; }

        public System.Data.Entity.DbSet<FestiDB.Domain.Table.TableQuestionStringColumn> TableQuestionStringColumns { get; set; }
        public DbSet<TableQuestionColumn> TableQuestionColumns { get; set; }
        public System.Data.Entity.DbSet<FestiDB.Domain.Table.TableQuestionTimeColumn> TableQuestionTimeColumns { get; set; }
        public DbSet<QuestionnaireInspector> QuestionnaireInspectors { get; set; }
        public DbSet<CategorieQuestion> CategorieQuestions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<User>().HasOptional(elem => elem.UserAccount)
                .WithOptionalDependent(ad => ad.User);

            modelBuilder.Entity<Contact>()
                .HasRequired(elem => elem.Customer)
                .WithMany().HasForeignKey(el => el.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Questionnaire>()
                .HasOptional(elem => elem.Event)
                .WithMany().HasForeignKey(el => el.EventId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
                .HasOptional(elem => elem.Questionnaire)
                .WithMany().HasForeignKey(el => el.QuestionnaireId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Answer>()
                .HasOptional(elem => elem.Question)
                .WithMany().HasForeignKey(elem => elem.QuestionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Answer>()
                .HasOptional(elem => elem.Inspector)
                .WithMany().HasForeignKey(elem => elem.InspectorId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .HasMany(elem => elem.Options)
                .WithOptional(elem => elem.Question)
                .HasForeignKey(elem => elem.QuestionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TableQuestionAnswerEntry>()
                .HasOptional(elem => elem.Answer)
                .WithMany().HasForeignKey(elem => elem.AnswerId)
                .WillCascadeOnDelete(true);
        }
    }
}