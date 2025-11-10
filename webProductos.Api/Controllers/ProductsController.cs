using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;

namespace webProductos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetById(int id)
    {
        return  Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
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
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}