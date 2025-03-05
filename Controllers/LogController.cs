using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using whatsapp_api.models;
using System.Text.Json;

namespace whatsapp_api.Controllers;

[ApiController]
[Route("api/logs")]
public class LogController : ControllerBase
{
    private static readonly string LogFilePath = "logs.json";

    [HttpPost]
    [HttpPost]
    public IActionResult SaveLog([FromForm] LogEntry logEntry)
    {
        try
        {
       
            logEntry.Timestamp = DateTime.UtcNow.ToString("o");
           

            List<LogEntry> logs = new List<LogEntry>();
            if (System.IO.File.Exists(LogFilePath))
            {
                string existingLogs = System.IO.File.ReadAllText(LogFilePath);
                // بررسی اینکه محتوای فایل خالی نباشد
                if (!string.IsNullOrWhiteSpace(existingLogs))
                {
                    try
                    {
                        logs = JsonSerializer.Deserialize<List<LogEntry>>(existingLogs) ?? new List<LogEntry>();
                    }
                    catch (JsonException jsonEx)
                    {
                        // در صورت خطا در پردازش JSON، یک لیست خالی ایجاد می‌کنیم
                        Console.WriteLine($"خطا در پردازش JSON: {jsonEx.Message}");
                        logs = new List<LogEntry>();
                    }
                }
            }

            logs.Add(logEntry);
            System.IO.File.WriteAllText(LogFilePath, JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true }));

            return Ok(new { message = "Log saved successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
