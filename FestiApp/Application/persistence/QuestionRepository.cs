using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly FestiMSClient _festiMsClient;
        private readonly TableQuestionFactory _tableQuestionFactory;
        private readonly IPictureRepository _pictureRepository;

        public QuestionRepository(FestiMSClient festiMsClient, TableQuestionFactory tableQuestionFactory, IPictureRepository pictureRepository)
        {
            _festiMsClient = festiMsClient;
            _tableQuestionFactory = tableQuestionFactory;
            _pictureRepository = pictureRepository;
        }

        public async Task<List<QuestionViewModel>> GetAll(string id)
        {
            var list = new List<QuestionViewModel>();
            try
            {
                list.AddRange(await _festiMsClient.GetSyncTable<OpenQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new OpenQuestionViewModel(elem, this)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<DrawQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new DrawQuestionViewModel(elem, this, _pictureRepository)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<PictureQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new PictureQuestionViewModel(elem, this)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<MeasureQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new MeasureQuestionViewModel(elem, this)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<ScaleQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new ScaleQuestionViewModel(elem, this)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<TableQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new TableQuestionViewModel(elem, this, _tableQuestionFactory)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<MultipleChoiceQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new MultipleChoiceQuestionViewModel(elem, this)).ToListAsync());
                list.AddRange(await _festiMsClient.GetSyncTable<CategorieQuestion>().Where(elem => elem.QuestionnaireId == id).Select(elem => new CategorieQuestionViewModel(elem, this)).ToListAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return list;
        }

        public async Task InsertAsync<T>(T entity) where T : Question
        {
            await _festiMsClient.GetSyncTable<T>().InsertAsync(entity);
        }

        public async Task Delete<T>(string id) where T : Question
        {
            var table = _festiMsClient.GetSyncTable<T>();
            var elem = await table.LookupAsync(id);
            await table.DeleteAsync(elem);
        }
    }
}