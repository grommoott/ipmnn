using System.Net.Http;
using System;

namespace Backend
{
    public static class Requests
    {
        public static string GameUuid = "50f4740f-e25e-4be6-81f8-850e9d4eeaa8";
        public static string BaseUrl = "/api/games/50f4740f-e25e-4be6-81f8-850e9d4eeaa8";

        public static HttpClient Client = new()
        {
            BaseAddress = new Uri("https://2025.nti-gamedev.ru")
        };
    }
}
