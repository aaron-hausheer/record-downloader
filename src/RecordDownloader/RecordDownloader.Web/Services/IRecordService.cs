using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using RecordDownloader.Web.DTOs;

namespace RecordDownloader.Web.Services
{
    public interface IRecordApiClient
    {
        Task<List<RecordDto>> GetRecordsAsync();
        Task UploadRecordAsync(IBrowserFile file, string textContent);
        Task DeleteRecordAsync(Guid id);
    }
}