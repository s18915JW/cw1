using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			// Console.WriteLine("Hello World!");

			var url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl/";
			var client = new HttpClient();
			var res = await client.GetAsync(url);

			if (res.IsSuccessStatusCode)
			{
				var content = await res.Content.ReadAsStringAsync();
				var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+",RegexOptions.IgnoreCase);

				var matches = regex.Matches(content);

				foreach (var item in matches)
				{
					Console.WriteLine(item.ToString());
				}
			}
		}
	}
}
