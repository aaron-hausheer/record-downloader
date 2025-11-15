using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecordDownloader.Data;
using RecordDownloader.Models;

namespace RecordDownloader.Repositories;

public class RecordRepository : IRecordRepository
{
    private readonly AppDbContext context;

    public RecordRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<RecordEntity>> GetAllAsync()
    {
        List<RecordEntity> records = await context.Records.AsNoTracking().ToListAsync();
        return records;
    }

    public async Task<RecordEntity> GetByIdAsync(Guid id)
    {
        RecordEntity record = await context.Records.FirstOrDefaultAsync(x => x.Id == id);
        return record;
    }

    public async Task AddAsync(RecordEntity record)
    {
        await context.Records.AddAsync(record);
    }

    public async Task DeleteAsync(RecordEntity record)
    {
        await Task.Run(() => context.Records.Remove(record));
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}