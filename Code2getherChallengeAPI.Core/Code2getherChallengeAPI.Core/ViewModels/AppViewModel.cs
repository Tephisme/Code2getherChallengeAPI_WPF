using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace Code2getherChallengeAPI.Core.ViewModels
{
    public class AppViewModel :MvxViewModel
    {
        private string _imageUrl = "";
        private readonly HttpClient _client = new HttpClient();

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, GetNewImageUrl());
        }

        private string GetNewImageUrl()
        {
            string url = "";

            string responseBody = ContactAPI().ToString();

            string[] responseBodies = responseBody?.Split('\"');

            foreach (string response in responseBodies)
            {
                if (response.StartsWith("http"))
                    url = response;
            }

            return url;
        }

        private async Task<string> ContactAPI()
        {
            try
            {
                HttpResponseMessage response = await _client
                    .GetAsync("https://api.thecatapi.com/v1/images/search");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                
                return responseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}