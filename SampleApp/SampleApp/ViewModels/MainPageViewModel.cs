using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
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
            GetEnvironmentInfoCommand = new DelegateCommand(async () => await GetInfo());
        }

        private async Task GetInfo()
        {
            EnvironmentConfig = "Prod";
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
