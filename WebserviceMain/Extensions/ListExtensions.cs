using System.Collections.Generic;
using WebserviceMain.Extensions.Helper;

namespace WebserviceMain.Extensions
{
	public static class ListExtensions
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			{
				var n = list.Count;
				while (n > 1)
				{
					n--;
					var k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
					var value = list[k];
					list[k] = list[n];
					list[n] = value;
				}
			}
		}
	}
}
