using System.Collections.Generic;

namespace HelloAspNetCoreMvc.Models
{
    public class Data
    {
        public string Text { get; set; }

        public static IEnumerable<Data> GetHelloWorldSet()
        {
            return new Data[] {
                new Data { Text = "Hello"},
                new Data { Text = "ASP.NET Core MVC" }
            };
        }
    }
}
