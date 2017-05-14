using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearchCommand { get; }
        public Command AboutCommand { get; }
        public Command<Tag> ShowCategoriaCommand { get; }

        private IMonkeyHubApiService _monkeyHumApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHumApiService)
        {
            _monkeyHumApiService = monkeyHumApiService;
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
            Resultados = new ObservableCollection<Tag>();
        }

        async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_monkeyHumApiService, tag);
        }

        async void ExecuteAboutCommand(object obj)
        {
            await PushAsync<AboutViewModel>();
        }

        async void ExecuteSearchCommand(object obj)
        {
            //Debug.WriteLine($"Clicou no botão! {DateTime.Now} : {SearchTerm}");
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                    $"Gostaria de adicionar?", "Sim", "Não");

            if (resposta)
            {
                List<Tag> tagsRetornadasDoServico = await _monkeyHumApiService.GetTagsAsync();

                Resultados.Clear();
                if (tagsRetornadasDoServico != null)
                {
                    foreach (Tag tag in tagsRetornadasDoServico)
                    {
                        Resultados.Add(tag);
                    }
                }
            }
        }

        bool CanExecuteSearchCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(SearchTerm);
        }
    }
}
