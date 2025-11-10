using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;

namespace webProductos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    [Authorize(Policy = "ResourceOwnerOrAdmin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await _service.GetByIdAsync(id);
            return Ok(user);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpPut("{id}")]
    [Authorize(Policy = "ResourceOwnerOrAdmin")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
    {
        var r = await _service.UpdateAsync(id, dto);
        if (r.Type == ResultType.NotFound) return NotFound();
        if (r.Type == ResultType.Conflict) return Conflict(r.Error);
        if (!r.Success) return BadRequest(r.Error);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "ResourceOwnerOrAdmin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}