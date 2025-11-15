using System;

namespace RecordDownloader.Web.Models;

public class CreateRecordRequest
{
    public string Filename { get; set; }
    public string TextContent { get; set; }
    public string? Base64Content { get; set; }
}
