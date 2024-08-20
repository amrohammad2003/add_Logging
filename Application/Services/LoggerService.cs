using AutoMapper;
using Contract.Interfaces.IServices;
using Efcore.DBContext;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly AppDbContext _context;

        public LoggerService(AppDbContext context)
        {
            _context = context;
        }

        public async  Task LogErr(Exception exception, string? message = null)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.LogLevel = "Error";
            logEntry.ExceptionMessage = exception.Message;
            logEntry.ExceptionSource = exception.Source;
            await Log(logEntry);
        }
        public async Task LogInfo(string message)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.LogLevel = "Information";
            logEntry.Message = message;
            await Log(logEntry);
        }
        public async Task LogWarn(string message)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.LogLevel = "Warning";
            logEntry.Message = message;
             await Log(logEntry);
        }


        private  async Task Log(LogEntry logEntry)
        {
            _context.Errors.Add(logEntry);
            await _context.SaveChangesAsync();
        }

        public async Task LogDbg(Exception exception, string? message = null)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.LogLevel = "Debug";
            logEntry.ExceptionMessage = exception.Message;
            logEntry.ExceptionSource = exception.Source;
            await Log(logEntry);


        }

    }
}
