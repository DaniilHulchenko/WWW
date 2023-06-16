using Microsoft.AspNetCore.SignalR;
using WWW.Domain.Enum;
using WWW.Service.Interfaces;

namespace WWW.Hubs
{
    public class CityHub : Hub
    {
        private readonly IArticleService _articleService;
        ILogger<CityHub> _logger;
        public CityHub(IArticleService articleService, ILogger<CityHub> logger)
        {
            _logger = logger;
            _articleService = articleService;
        }


        public async Task GetCitys(string city)
        {
            //List<string> cityList = new List<string>(Enum.GetValues(typeof(PopularCity)).Cast<string>().ToList());
            List<PopularCity> cityList = new List<PopularCity>(Enum.GetValues(typeof(PopularCity)) as PopularCity[]);
            List<string> cityNames = cityList.Select(city => city.ToString()).ToList();
            cityNames = cityNames.Select(x => x.Replace("_"," ")).ToList();

            var searchResults = cityNames.Where(с => с.StartsWith(city, StringComparison.OrdinalIgnoreCase)).ToList();

            await Clients.Caller.SendAsync("ReceiveCitys",searchResults.ToArray());
        }
    }
}

//https://metanit.com/sharp/mvc5/16.1.php#:~:text=SignalR%20%D0%BF%D1%80%D0%B5%D0%B4%D0%BE%D1%81%D1%82%D0%B0%D0%B2%D0%BB%D1%8F%D0%B5%D1%82%20%D0%BF%D1%80%D0%BE%D1%81%D1%82%D0%BE%D0%B9%20API%20%D0%B4%D0%BB%D1%8F,%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D1%83%20%D1%81%20%D0%BA%D0%BE%D0%BC%D0%BC%D1%83%D0%BD%D0%B8%D0%BA%D0%B0%D1%86%D0%B8%D1%8F%D0%BC%D0%B8%20%D1%80%D0%B5%D0%B0%D0%BB%D1%8C%D0%BD%D0%BE%D0%B3%D0%BE%20%D0%B2%D1%80%D0%B5%D0%BC%D0%B5%D0%BD%D0%B8.

