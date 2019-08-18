using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SampleApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            HttpClientInstance = new HttpClient();
            GetEnvironmentInfoCommand = new DelegateCommand(async () => await GetInfo());
        }

        private readonly HttpClient HttpClientInstance;

        private async Task GetInfo()
        {
            var url = Constants.EndPoint;
            EnvironmentConfig = url;
            await Task.Delay(10000);
        }

        public DelegateCommand GetEnvironmentInfoCommand { get; private set; }

        private string _environmentConfig;
        public string EnvironmentConfig
        {
            get { return _environmentConfig; }
            set { SetProperty(ref _environmentConfig, value); }
        }
    }
}
