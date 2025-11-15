using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecordDownloader.Models;

namespace RecordDownloader.Repositories;

public interface IRecordRepository
{
    Task<List<RecordEntity>> GetAllAsync();
    Task<RecordEntity> GetByIdAsync(Guid id);
    Task AddAsync(RecordEntity record);
    Task DeleteAsync(RecordEntity record);
    Task SaveChangesAsync();
}