using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FestiApp.persistence;
using FestiApp.ViewModel.Questionnaires;
using FestiDB.Domain;
using GalaSoft.MvvmLight;

namespace FestiApp.ViewModel.Questions
{
    public abstract class QuestionViewModel : ViewModelBase
    {
        private int _order;
        public abstract bool IsValid();


        public string Id { get; set; }

        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }

        public string Description { get; set; }

        public QuestionnaireViewModel Questionaire { get; set; }

        public bool IsNew => string.IsNullOrEmpty(Id);

        public abstract Task Save();

        protected QuestionViewModel(Question question)
        {
            Id = question.Id;
            Order = question.Order;
            Description = question.Description;
        }


        public abstract void Delete();

    }
}
