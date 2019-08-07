using FestiDB.Domain;
using FestiDB.Domain.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.ViewModel.Questions
{
    public class TableQuestionFactory
    {
        private readonly IFestiClient _festiClient;
        public ICollection<string> KeyUnits => new List<string>() { "Text", "Nummer", "Tijd" };
        public ICollection<string> KeyValues => new List<string>() { "Text", "Nummer", "Multiple Choice" };


        public TableQuestionFactory(IFestiClient festiClient)
        {
            _festiClient = festiClient;
        }

        public async Task<TableQuestionColumn> CreateTableQuestionColumn(string keyUnit, string headerText,
            string id,
            ICollection<MultipleChoiceQuestionOption> multipleChoiceQuestionOptions = null)
        {
            TableQuestionColumn column;
            switch (keyUnit)
            {
                case "Text":
                    column = await _festiClient.InsertAsync(new TableQuestionStringColumn
                        {Header = headerText});
                    break;
                case "Nummer":
                    column = await _festiClient.InsertAsync(new TableQuestionNumberColumn
                        {Header = headerText});
                    break;
                case "Tijd":
                    column = await _festiClient.InsertAsync(new TableQuestionTimeColumn
                        {Header = headerText});
                    break;
                case "Multiple Choice":
                    column = await _festiClient.InsertAsync(new TableQuestionMultipleColumn
                    {
                        
                        Options = multipleChoiceQuestionOptions,

                        Header = headerText
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{headerText} not in range for key value");
            }

            column.Header = headerText;
            return column;
        }
    }
}