using System;

namespace RecordDownload.Shared.Entities;

public class CreateRecordRequest
{
    public string Filename { get; set; }
    public string TextContent { get; set; }
    public string Base64Content { get; set; }
}
