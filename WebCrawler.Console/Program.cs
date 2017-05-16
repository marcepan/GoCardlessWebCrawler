using Newtonsoft.Json;
using WebCrawler.Logic;

namespace WebCrawler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Crawler();
            bot.Run();
            var json = JsonConvert.SerializeObject(bot.Results);
            System.Console.Write(json);
            System.Console.ReadKey();
        }
    }
}
