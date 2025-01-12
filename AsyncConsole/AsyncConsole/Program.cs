namespace AsyncConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("http://www.apple.com");
            Console.WriteLine("Apple's home page {0:0N} bytes", response.Content.Headers.ContentLength);
        }
    }
}
