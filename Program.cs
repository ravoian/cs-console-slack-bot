using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace SlackMessageBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string token = "<slack_bot_token_here>";

            // Basic command-line interface
            Console.WriteLine("Enter your desired message to post to Slack:");
            string input = Console.ReadLine();

            // Parameters for API call
            var payload = new
            {
                channel = "<slack_channel_id_here>",
                text = input,
            };
            string serializedPayload = JsonSerializer.Serialize(payload);

            // Setup request client & header
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Post the request & display output
            StringContent body = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://slack.com/api/chat.postMessage", body);
            Console.WriteLine(response);

            // Display output for message status
            string responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument parsedContent = JsonDocument.Parse(responseContent);
            string contentFormatted = JsonSerializer.Serialize(parsedContent, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(contentFormatted);
        }


    }
}
