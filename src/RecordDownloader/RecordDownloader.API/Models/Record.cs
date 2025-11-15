using System;

namespace RecordDownloader.Models;

public class RecordEntity
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public byte[] Content { get; set; }
    public string? TextContent { get; set; }
}
