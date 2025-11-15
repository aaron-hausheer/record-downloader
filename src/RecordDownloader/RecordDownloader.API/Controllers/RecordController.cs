using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecordDownloader.DTOs;
using RecordDownloader.Services;
using RecordDownloader.Web.Models;

namespace RecordDownloader.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
    private readonly IRecordService recordService;

    public RecordsController(IRecordService recordService)
    {
        this.recordService = recordService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RecordDto>>> GetAll()
    {
        List<RecordDto> records = await recordService.GetAllAsync();
        return Ok(records);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RecordDto>> GetById(Guid id)
    {
        RecordDto record = await recordService.GetByIdAsync(id);
        if (record == null)
        {
            return NotFound();
        }

        return Ok(record);
    }

    [HttpGet("{id:guid}/file")]
    public async Task<IActionResult> Download(Guid id)
    {
        RecordDto record = await recordService.GetByIdAsync(id);
        if (record == null)
        {
            return NotFound();
        }

        byte[] content = await recordService.GetContentAsync(id);
        if (content == null)
        {
            return NotFound();
        }

        string fileName = record.Filename;
        return File(content, "application/octet-stream", fileName);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRecordRequest request)
    {
        Guid id = await recordService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = id }, id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await recordService.DeleteAsync(id);
        return NoContent();
    }
}