namespace whatsapp_api.models;

public class LogEntry
{
    public string Timestamp { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string Platform { get; set; }
    public string Text { get; set; }
    public string SystemIp { get; set; }
    public string ImageBase64 { get; set; }

}
