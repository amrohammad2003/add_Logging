﻿public class LogEntry
{
    public int Id { get; set; }
    public string LogLevel { get; set; }
    public string? Message { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ExceptionSource { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
