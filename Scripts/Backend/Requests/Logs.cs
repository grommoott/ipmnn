using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

namespace Backend
{
    public static class Logs
    {
        public static async Task PlayerCreate(string comment, string playerName, List<Serialization.Item> resourcesChanged)
        {
            using StringContent content = new
                (
                    Serialization.Json.Serialize
                    (
                        new { comment = comment, player_name = playerName, resources_changed = resourcesChanged }
                    )
                );

            using HttpResponseMessage response = await Requests.Client.PostAsync($"{Requests.BaseUrl}/logs", content);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                Debug.LogError(response.Content.ReadAsStringAsync());
            }
        }
    }
}
