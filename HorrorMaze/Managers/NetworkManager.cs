using System.Net.Http;
using System.Diagnostics;

namespace HorrorMaze
{
    public static class NetworkManager
    {

        public static readonly HttpClient client = new HttpClient();

        public static async void AddTimeScore(string name, string score, TextRenderer scoreBoardText)
        {
            var values = new Dictionary<string, string>
              {
                  { "scoreName", name },
                  { "scoreScore", score }
              };

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("web");
            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/AddTimeScore.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                GetTimeScores(scoreBoardText);
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static async void GetTimeScores(TextRenderer textRend)
        {
            var values = new Dictionary<string, string>
              {
              };

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("web");
            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/GetTimeScores.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                if(textRend != null)
                    textRend.SetText(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static async void AddFloorScore(string name, string score, string time, TextRenderer scoreBoardText)
        {
            var values = new Dictionary<string, string>
              {
                  { "scoreName", name },
                  { "scoreScore", score },
                  { "scoreTime", time }
              };

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("web");
            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/AddFloorScore.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                GetFloorScores(scoreBoardText);
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static async void GetFloorScores(TextRenderer textRend)
        {
            var values = new Dictionary<string, string>
            {
            };

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("web");
            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/GetFloorScores.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                if (textRend != null)
                    textRend.SetText(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
