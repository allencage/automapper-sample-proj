using Log.Contracts;
using System;
using Serilog;

namespace Log.Logic
{
	public class CustomLog : ICustomLog
	{
		private readonly ILogger _logger;
		public CustomLog()
		{
			_logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.LiterateConsole()
				.CreateLogger();
		}

		public void Log(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void LogInformation(string message)
		{
			_logger.Information(message);
		}

		public void LogError(string message)
		{
			_logger.Error(message);
		}

		public void LogError(Exception ex, string message)
		{
			_logger.Error(ex, message);
		}

		public void LogWarning(string message)
		{
			_logger.Warning(message);
		}

		public void LogWarning(Exception ex, string message)
		{
			_logger.Warning(ex, message);
		}
	}
}
