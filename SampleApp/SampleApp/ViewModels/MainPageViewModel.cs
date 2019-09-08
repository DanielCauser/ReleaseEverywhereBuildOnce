using Newtonsoft.Json;
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
            var response = await HttpClientInstance.GetAsync(Constants.EndPoint)
                                                                   .ConfigureAwait(false);
    
            if (response.IsSuccessStatusCode)
            {
                string serialized = await response.Content.ReadAsStringAsync();
                Environment result = await Task.Run(() => JsonConvert.DeserializeObject<Environment>(serialized))
                                           .ConfigureAwait(false);

                EnvironmentConfig = result.Name;
                return;
            }

            EnvironmentConfig = "Failed";
        }

        public DelegateCommand GetEnvironmentInfoCommand { get; private set; }

        private string _environmentConfig;
        public string EnvironmentConfig
        {
            get { return _environmentConfig; }
            set { SetProperty(ref _environmentConfig, value); }
        }
    }

    public class Environment
    {
        public string Name { get; set; }
    }
}
