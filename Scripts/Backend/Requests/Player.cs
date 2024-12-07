using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using UnityEngine;
using System.Collections.Generic;

namespace Backend
{
    public static class Player
    {
        public static async Task Create(string username)
        {
            using StringContent jsonContent = new(Serialization.Json.Serialize(new { name = username }));
            using HttpResponseMessage response = await Requests.Client.PostAsync($"{Requests.BaseUrl}/players", jsonContent);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                Debug.LogError(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<List<Types.Player>> GetList()
        {
            using HttpResponseMessage response = await Requests.Client.GetAsync($"{Requests.BaseUrl}/players");

            string content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.LogError(content);
            }
            return Serialization.Json.Parse<List<Types.Player>>(content);
        }

        /*public static async Task<Serialization.PlayerInventory> GetPlayerResources(string username)*/
        /*{*/
        /**/
        /*}*/
    }
}
