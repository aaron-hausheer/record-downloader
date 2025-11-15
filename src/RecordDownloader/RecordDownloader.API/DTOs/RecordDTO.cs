using System;

namespace RecordDownloader.DTOs;

public class RecordDto
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string? TextContent { get; set; }
}
