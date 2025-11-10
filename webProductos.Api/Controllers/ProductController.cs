using Microsoft.AspNetCore.Mvc;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;

namespace webProductos.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return  Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductAddDto dto)
    {
        var id = await _service.AddProductAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        var r = await _service.UpdateAsync(id, dto);
        if (r.Type == ResultType.NotFound) return NotFound();
        if (r.Type == ResultType.Conflict) return Conflict(r.Error);
        if (!r.Success) return BadRequest(r.Error);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}