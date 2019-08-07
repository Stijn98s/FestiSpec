using FestiApp.persistence;
using FestiApp.ViewModel.Events;
using FestiApp.ViewModel.Questionnaires;
using FestiAppViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAzure.MobileServices;

namespace FestiApp.ViewModel
{
    public class PlanInspectorViewModel : ViewModelBase
    {

        private readonly FestiMSClient _festiMsClient;
        private readonly IEditViewModel<QuestionnaireViewModel> _questionnaire;

        public ICollection<InspectorDistanceViewModel> Inspectors { get; private set; }

        public ICommand AssignCommand { get; set; }

        private InspectorDistanceViewModel _selectedInspector;

        public InspectorDistanceViewModel SelectedInspector
        {
            get => _selectedInspector;
            set
            {
                _selectedInspector = value;
                base.RaisePropertyChanged("SelectedInspector");
            }
        }

        public ICommand ChangeAssignCommand { get; set; }

        public PlanInspectorViewModel(FestiMSClient festiMsClient, IEditViewModel<QuestionnaireViewModel> questionnaire, IEditViewModel<EventViewModel> inspectionEvent)
        {
            ChangeAssignCommand = new RelayCommand(ChangeAssign);
            _festiMsClient = festiMsClient;
            _questionnaire = questionnaire;

            Task.Run(async () =>
            {
                try
                {

                    Inspectors = await _festiMsClient.Inspectors.GetAllAvailableInspectors(inspectionEvent.Entity.Id);
                    RaisePropertyChanged("Inspectors");
                }
                catch (MobileServiceInvalidOperationException e)
                {
                    
                }

            });

            AssignCommand = new RelayCommand(ScheduleInspectorIn);
        }

        public void ScheduleInspectorIn()
        {
            if (SelectedInspector != null)
            {
                Task.Run(async () =>
                {
                    RaisePropertyChanged("SelectedInspector");
                    await _festiMsClient.Inspectors.AssignInspector(SelectedInspector.Inspector, _questionnaire.Entity.Id);
                    RaisePropertyChanged("Inspectors");
                    MessageBox.Show("De inspecteur heeft de vragenlijst ontvangen in zijn takenlijst");
                });
            }
        }

        public void ChangeAssign()
        {
            if (SelectedInspector != null)
            {
                Task.Run(async () =>
                {
                    RaisePropertyChanged("SelectedInspector");
                    await _festiMsClient.Inspectors.DespatchInspector(SelectedInspector.Inspector, _questionnaire.Entity.Id);
                    RaisePropertyChanged("Inspectors");
                    MessageBox.Show("De vragenlijst is verwijdert uit de takenlijst van de inspecteur");
                });
            }
        }
    }
}