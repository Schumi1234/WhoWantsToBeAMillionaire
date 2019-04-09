using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebserviceMain.Extensions.Helper
{
	public static class ThreadSafeRandom
	{
		[ThreadStatic] private static Random _local;

		public static Random ThisThreadsRandom => _local ?? (_local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
	}
}
