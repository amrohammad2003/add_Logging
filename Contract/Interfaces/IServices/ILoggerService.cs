using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces.IServices
{
    public interface ILoggerService
    {
        Task LogErr(Exception exception, string? message = null);
        public Task LogInfo(string message);

        public Task LogDbg(Exception exception, string? message = null);

        public Task LogWarn(string message)
;


    }
}
