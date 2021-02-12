using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Code2getherChallengeAPI.Core.ViewModels
{
    public class AppViewModel :MvxViewModel
    {
        public IMvxCommand GetImageUrlCommand { get; set; }
        
        private string _imageUrl = "";
        private readonly HttpClient _client = new HttpClient();

        public AppViewModel()
        {
            GetImageUrlCommand = new MvxCommand(GetImageUrl);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                SetProperty(ref _imageUrl, value);
                RaisePropertyChanged(() => ImageUrl);
            }
        }

        public void GetImageUrl()
        {
            ContactAPI();
        }

        private async Task ContactAPI()
        {
            try
            {
                HttpResponseMessage response = await _client
                    .GetAsync("https://api.thecatapi.com/v1/images/search");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                string[] responseBodies = responseBody.Split('\"');

                string url = "";
                foreach (string body in responseBodies)
                {
                    if (body.StartsWith("http"))
                        url = body;
                }

                _imageUrl = url;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}