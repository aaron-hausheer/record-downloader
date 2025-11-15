using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecordDownloader.DTOs;
using RecordDownloader.Models;
using RecordDownloader.Repositories;
using RecordDownloader.Web.Models;

namespace RecordDownloader.Services;

public class RecordService : IRecordService
    {
        private readonly IRecordRepository repository;

        public RecordService(IRecordRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<RecordDto>> GetAllAsync()
        {
            List<RecordEntity> entities = await repository.GetAllAsync();
            List<RecordDto> result = new List<RecordDto>();

            foreach (RecordEntity entity in entities)
            {
                RecordDto dto = new RecordDto
                {
                    Id = entity.Id,
                    Filename = entity.Filename,
                    TextContent = entity.TextContent
                };
                result.Add(dto);
            }

            return result;
        }

        public async Task<RecordDto> GetByIdAsync(Guid id)
        {
            RecordEntity entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            RecordDto dto = new RecordDto
            {
                Id = entity.Id,
                Filename = entity.Filename,
                TextContent = entity.TextContent
            };

            return dto;
        }

        public async Task<byte[]> GetContentAsync(Guid id)
        {
            RecordEntity entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            return entity.Content;
        }

        public async Task<Guid> CreateAsync(CreateRecordRequest request)
        {
            byte[] content = Convert.FromBase64String(request.Base64Content);

            RecordEntity entity = new RecordEntity
            {
                Id = Guid.NewGuid(),
                Filename = request.Filename,
                Content = content,
                TextContent = request.TextContent
            };

            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            RecordEntity entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            await repository.DeleteAsync(entity);
            await repository.SaveChangesAsync();
        }
    }