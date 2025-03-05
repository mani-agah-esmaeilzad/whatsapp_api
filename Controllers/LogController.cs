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
    public async Task<IActionResult> SaveLog([FromForm] LogEntry logEntry, IFormFile image)
    {
        try
        {
            logEntry.Timestamp = DateTime.UtcNow.ToString("o");
            logEntry.SystemIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            logEntry.Platform = "application";

            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    logEntry.ImageBase64 = Convert.ToBase64String(ms.ToArray());
                }
            }

            List<LogEntry> logs = new();
            if (System.IO.File.Exists(LogFilePath))
            {
                string existingLogs = System.IO.File.ReadAllText(LogFilePath);
                logs = JsonSerializer.Deserialize<List<LogEntry>>(existingLogs) ?? new List<LogEntry>();
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
