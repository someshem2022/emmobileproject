using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using EM.InsurePlus.Common;
using EM.InsurePlus.Services.Contacts;
using EM.InsurePlus.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EM.InsurePlus.Data;
using System.IO;
using AutoMapper;

namespace EM.InsurePlus
{
    public partial class App : Application
    {
        private static InsurePlusDatabase database;
        static App()
        {
            BuildDependencies();           
        }

        public App()
        {
            InitializeComponent();
            InitializeAutomapper();
            InitDatabase();
            InitNavigation();            
        }
        private static void InitDatabase()
        {
            if (database == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "insureplus.db3");
                Preferences.Set(PreferencesKey.DatabasePath, dbPath);                
                database = new InsurePlusDatabase(dbPath);
            }
        }

        public static void BuildDependencies()
        {
            Preferences.Set(AppContants.Url, "https://eminsureplus.azurewebsites.net/");
            Locator.Instance.Build();
        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        private void InitializeAutomapper()
        {
            Mapper.Initialize(c => c.AddProfile<RepositoryMapProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=0ab3fbb0-afd9-42ea-bf44-c553b14b2046;" +
                     "uwp={Your UWP App secret here};" +
                     "ios={Your iOS App secret here};" +
                     "macos={Your macOS App secret here};",
                     typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
