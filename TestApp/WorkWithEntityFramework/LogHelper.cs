using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithEntityFramework
{

	public static class Logger
	{
		public static void Log(DbCommand command, long elapsedMilliseconds, Exception exception)
		{
			Trace.WriteLine(string.Format("Command:{0}, Elapsed Milliseconds:{1}", command.CommandText, elapsedMilliseconds));
		}
	}

	/// <summary>
	/// Logger for Entity Framework
	/// </summary>
	public class LogFormatter : IDbCommandInterceptor
	{
		private readonly Stopwatch _stopwatch = new Stopwatch();
		public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			// Перезапускам таймер
			_stopwatch.Restart();
		}
		public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			// Останавливаем таймер
			_stopwatch.Stop();
			// Логируем команду
			Log(command, interceptionContext);
		}
		public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			_stopwatch.Restart();
		}
		public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			_stopwatch.Stop();
			Log(command, interceptionContext);
		}
		public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			_stopwatch.Restart();
		}
		public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			_stopwatch.Stop();
			Log(command, interceptionContext);
		}
		private void Log<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Logger.Log(command, _stopwatch.ElapsedMilliseconds, interceptionContext.Exception);
		}
	}
}
