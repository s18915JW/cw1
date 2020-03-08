using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			if (args[0] == null)
				throw new ArgumentNullException();
			else if (CheckURL(args[0]))
				throw new ArgumentException();
			else
			{

				var url = args[0];
				var client = new HttpClient();
				var res = await client.GetAsync(url);

				client.Dispose();

				if (res.IsSuccessStatusCode)
				{
					var content = await res.Content.ReadAsStringAsync();
					var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

					var matches = regex.Matches(content);
					HashSet<string> set = new HashSet<string>();

					if (matches.Count > 0)
					{
						foreach (var match in matches)
							set.Add(match.ToString());

						foreach (var uniqueAddress in set)
							Console.WriteLine(uniqueAddress);
					}
					else Console.WriteLine("Nie znaleziono adresów email");
				}
				else Console.WriteLine("Błąd w czasie pobierania strony");
			}
		}
		public static bool CheckURL(string url)
		{
			Uri uriResult;
			return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
		}
	}

}
