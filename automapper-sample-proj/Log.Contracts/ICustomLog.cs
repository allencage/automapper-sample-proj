using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Contracts
{
    public interface ICustomLog
    {
        void Log(Exception ex);
		void LogInformation(string message);
		void LogError(string message);
		void LogError(Exception ex, string message);
		void LogWarning(string message);
		void LogWarning(Exception ex, string message);
    }
}
