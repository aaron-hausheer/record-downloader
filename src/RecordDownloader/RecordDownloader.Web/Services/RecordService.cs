using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using RecordDownloader.Web.DTOs;
using RecordDownloader.Web.Models;

namespace RecordDownloader.Web.Services;

public class RecordApiClient : IRecordApiClient
{
    private readonly HttpClient httpClient;

    public RecordApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<RecordDto>> GetRecordsAsync()
    {
        List<RecordDto> records = await httpClient.GetFromJsonAsync<List<RecordDto>>("api/records");
        if (records == null)
        {
            records = new List<RecordDto>();
        }

        return records;
    }

    public async Task UploadRecordAsync(IBrowserFile file, string textContent)
    {
        long maxFileSize = 1024 * 1024 * 15;
        MemoryStream memoryStream = new MemoryStream();
        await file.OpenReadStream(maxFileSize).CopyToAsync(memoryStream);
        byte[] bytes = memoryStream.ToArray();
        string base64 = Convert.ToBase64String(bytes);

        CreateRecordRequest request = new CreateRecordRequest
        {
            Filename = file.Name,
            TextContent = textContent,
            Base64Content = base64
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/records", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRecordAsync(Guid id)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync("api/records/" + id);
        response.EnsureSuccessStatusCode();
    }
}