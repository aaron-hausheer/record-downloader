using System;

namespace Record.Shared.Models;

public class RecordDto
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string? TextContent { get; set; }
}
