import {
  AbstractTableQuestionAnswerValue, TableQuestion, TableQuestionAnswerEntry, TableQuestionAnswerMultipleValue,
  TableQuestionAnswerNumberValue, TableQuestionAnswerStringValue,
  TableQuestionAnswerTimeValue, TableQuestionColumn,
  TableQuestionMultipleColumn
} from '@/services/ApiService'

export class AnswerEntryFactory {
  static getAnswerEntry (question: TableQuestion) {
    const answerEntry = new TableQuestionAnswerEntry()
    answerEntry.key = AnswerEntryFactory.getValueColumn(question.keyColumn)
    answerEntry.value = AnswerEntryFactory.getValueColumn(question.valueColumn)
    return answerEntry
  }

  private static getValueColumn (questionColumn : TableQuestionColumn | undefined): AbstractTableQuestionAnswerValue {
    switch (questionColumn!._discriminator) {
      case 'TableQuestionStringColumn':
        return new TableQuestionAnswerStringValue();
      case 'TableQuestionMultipleColumn':
        return new TableQuestionAnswerMultipleValue();
      case 'TableQuestionNumberColumn':
        return new TableQuestionAnswerNumberValue();
      case 'TableQuestionTimeColumn':
        return new TableQuestionAnswerTimeValue();
      default:
        throw new Error()
    }
  }
}
