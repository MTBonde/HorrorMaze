using System.Net.Http;
using System.Diagnostics;

namespace HorrorMaze
{
    public static class NetworkManager
    {

        public static readonly HttpClient client = new HttpClient();

        public static async void AddScore(string name, string score, TextRenderer scoreBoardText)
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
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/AddScore.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                GetScores(scoreBoardText);
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static async void GetScores(TextRenderer textRend)
        {
            var values = new Dictionary<string, string>
              {
              };

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("web");
            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://dreamlikestudios.net/GameBackend/HorrorMaze/GetScores.php", content);
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
    }
}
