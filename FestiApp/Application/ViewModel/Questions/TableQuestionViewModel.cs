using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FestiApp.persistence;
using FestiDB.Domain;
using System.Collections.Generic;
using System.Linq;
using FestiDB.Domain.Table;

namespace FestiApp.ViewModel.Questions
{
    public class TableQuestionViewModel : QuestionViewModel
    {
        private readonly TableQuestionFactory _tableQuestionFactory;
        private readonly TableQuestion _question;
        private readonly IQuestionRepository _questionRepository;
        private string _valueUnit;

        public TableQuestionViewModel(TableQuestion question, IQuestionRepository questionRepository,
            TableQuestionFactory tableQuestionFactory) :
            base(question)
        {
            _question = question;
            _questionRepository = questionRepository;
            _tableQuestionFactory = tableQuestionFactory;
            KeyUnit = KeyUnits.FirstOrDefault();
            ValueUnit = KeyValues.FirstOrDefault();
            Choices = new MultipleChoiceQuestionViewModel(new MultipleChoiceQuestion(), questionRepository)
            {
                Description = "TableMultipleChoice"
            };
        }

        public MultipleChoiceQuestionViewModel Choices { get; set; }


        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Description)) return false;
            if (string.IsNullOrEmpty(KeyUnit)) return false;
            if (string.IsNullOrEmpty(ValueUnit)) return false;
            if (string.IsNullOrEmpty(LeftHeader)) return false;
            if (string.IsNullOrEmpty(RightHeader)) return false;
            if (!Choices.IsValid() && IsMultipleChoice) return false;
            return true;
        }

        public override async Task Save()
        {
            _question.Order = Order;
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            var leftQuestionColumn = await _tableQuestionFactory.CreateTableQuestionColumn(KeyUnit, LeftHeader, _question.Id);
            var rightQuestionColumn= await _tableQuestionFactory.CreateTableQuestionColumn(ValueUnit, RightHeader, _question.Id, Choices.Options);
            _question.KeyColumnId = leftQuestionColumn.Id;
            _question.ValueColumnId = rightQuestionColumn.Id;
            await _questionRepository.InsertAsync(_question);

        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<TableQuestion>(Id);
        }

        public string LeftHeader { get; set; }

        public string RightHeader { get; set; }

        public ICollection<string> KeyUnits => new List<string>() { "Text", "Nummer", "Tijd" };
        public ICollection<string> KeyValues => new List<string>() { "Text", "Nummer", "Multiple Choice" };

        public string KeyUnit { get; set; }

        public string ValueUnit
        {
            get => _valueUnit;
            set
            {
                _valueUnit = value;
                RaisePropertyChanged("IsMultipleChoice");
            }
        }

        public bool IsMultipleChoice => ValueUnit == "Multiple Choice";
    }
}