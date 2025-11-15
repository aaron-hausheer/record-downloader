using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecordDownloader.DTOs;
using RecordDownloader.Web.Models;

namespace RecordDownloader.Services;

public interface IRecordService
{
    Task<List<RecordDto>> GetAllAsync();
    Task<RecordDto> GetByIdAsync(Guid id);
    Task<byte[]> GetContentAsync(Guid id);
    Task<Guid> CreateAsync(CreateRecordRequest request);
    Task DeleteAsync(Guid id);
}