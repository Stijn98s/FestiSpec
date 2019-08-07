using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using FestiApp.persistence;
using FestiDB.Domain;
using System.Linq;
using FestiApp.NinjectModules;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Owin.Security.Provider;
using Microsoft.Win32;

namespace FestiApp.ViewModel.Questions
{
    public class DrawQuestionViewModel : QuestionViewModel
    {
        private readonly DrawQuestion _question;
        private readonly IQuestionRepository _questionRepository;
        private readonly IPictureRepository _pictureRepository;
        private string _fileName;
        public Unit SelectedUnit { get; set; }

        public DrawQuestionViewModel(DrawQuestion question, IQuestionRepository questionRepository, IPictureRepository pictureRepository) : base(question)
        {
            _question = question;
            _questionRepository = questionRepository;
            _pictureRepository = pictureRepository;
            OpenFileDialogCommand = new RelayCommand(OpenFileDialogAction);
        }

        private void OpenFileDialogAction()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
                FileName = openFileDialog.FileName;
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public override bool IsValid()
        {
            if(string.IsNullOrEmpty(Description)) return  false;
            return !string.IsNullOrEmpty(FileName);
        }

        public override async Task Save()
        {
            _question.Order = Order;
            await _pictureRepository.UploadFileAsync(FileName, _question);
            _question.Description = Description;
            _question.QuestionnaireId = _question.Questionnaire.Id;
            _question.Questionnaire = null;
            await _questionRepository.InsertAsync(_question);
        }

        public override void Delete()
        {
            if (!string.IsNullOrEmpty(Id))
                _questionRepository.Delete<DrawQuestion>(Id);
        }

        public RelayCommand OpenFileDialogCommand { get; set; }
    }
}
