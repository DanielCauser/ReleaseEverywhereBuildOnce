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
            EnvironmentConfig = "Production";
            GetEnvironmentInfoCommand = new DelegateCommand(async () => await GetInfo());
        }

        private Task GetInfo()
        {
            EnvironmentConfig = "Prod";
            return Task.CompletedTask;
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
