using FestiApp.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace FestiApp.NinjectModules
{
    public class NetStatusService : ViewModelBase, INetStatusService, IDisposable
    {
        private readonly Timer _timer;
        private readonly HttpClient _request;
        private bool _isActive = true;

        public NetStatusService()
        {
            _timer = new Timer(1000) { AutoReset = true, Enabled = true };

            _request = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("festiMSURL")),
                Timeout = TimeSpan.FromSeconds(1)
            };

            _request.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            _timer.Elapsed += OnTimedEvent;
        }

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (var mes = new HttpRequestMessage(HttpMethod.Head, "api/Status/Status"))
                {
                    var req = await _request.SendAsync(mes);
                    IsActive = req.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                IsActive = false;
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                RaisePropertyChanged();
                RaisePropertyChanged("NotIsActive");
            }
        }

        public bool NotIsActive => !IsActive;
        public void SubscribeWithAction(Action raiseCanExecuteChanged)
        {
            PropertyChanged += (ob, ob2) => { Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(raiseCanExecuteChanged)); };
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}