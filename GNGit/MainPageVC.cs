using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GNGit
{
    public class MainPageVC : INotifyPropertyChanged
    {
        public MainPageVC()
        {
            OpenBrowserCommand = new Command<string>(OpenBrowser);
        }

        private ObservableCollection<GitUserModel> gitUsers = new ObservableCollection<GitUserModel>();
        public ObservableCollection<GitUserModel> GitUsers
        {
            get
            { return gitUsers; }
            set
            {
                if (gitUsers != value)
                {
                    gitUsers = value;
                    OnPropertyChanged("GitUser");
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get
            { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                    if (!string.IsNullOrEmpty(searchText)) 
                    {
                        string url = "https://api.github.com/search/users?q=" + searchText;
                        UseWebServiceAsync(url);
                    }
                }
            }
        }

        async private void UseWebServiceAsync(string url)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri(url));
                string result = await response.Content.ReadAsStringAsync();

                RootObject convert = JsonConvert.DeserializeObject<RootObject>(result);
                GitUsers.Clear();
                // only to make sure we clear before adding
                Thread.Sleep(200); // TODO: Find a better way to do this

                foreach (GitUserModel user in convert.GitUsers)
                {
                    GitUsers.Add(user);
                }
            }
            catch (JsonSerializationException jsonerr)
            {
                Debug.WriteLine(jsonerr.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand OpenBrowserCommand { private set; get; }

        public async void OpenBrowser(string uri)
        {
            await Browser.OpenAsync(new Uri(uri), BrowserLaunchMode.SystemPreferred);
        }
    }
}
