using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarvelDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarvelDemo.Services
{
    public class MarvelDataService : IMarvelDataService
    {
        // TODO: Add your Marvel Developer Account keys
        const string _API_PRIVATE_KEY = "07b1920db25c18f8efa5a657485eb2017aa47c00";
        const string _API_PUBLIC_KEY = "451e76b8d718f343195eb3505b3a4519";

        readonly IHashService _hashService;

        public MarvelDataService(IHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task<IEnumerable<Comic>> GetComicsBySeries(int seriesId, string orderBy = null)
        {
            var ts = Guid.NewGuid().ToString();
            var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);

            if (string.IsNullOrWhiteSpace(orderBy))
                orderBy = "issueNumber";

            var url =
                $@"http://gateway.marvel.com/v1/public/series/{seriesId}/comics?orderBy={orderBy}&apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}";
            
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            
            var responseObject = JObject.Parse(response);

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Comic>>(responseObject["data"]["results"].ToString()));
        }

        public async Task<IEnumerable<Character>> GetAllCharacters(string orderBy = null)
        {
            var ts = Guid.NewGuid().ToString();
            var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);

            var url =
                $@"http://gateway.marvel.com/v1/public/characters?apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}";
            
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            var responseObject = JObject.Parse(response);

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Character>>(responseObject["data"]["results"].ToString()));
        }

    }
}
