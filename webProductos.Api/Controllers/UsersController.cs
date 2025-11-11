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
    private readonly IAuthorizationService _auth;

    public UsersController(IUserService service, IAuthorizationService auth)
    {
        _service = service;
        _auth = auth;
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var existingUser = await _service.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound();

        var authorizationResult = await _auth.AuthorizeAsync(
            User, existingUser.Id, "ResourceOwnerOrAdmin");

        if (!authorizationResult.Succeeded)
            return Forbid();
        
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
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
    {
        var existingUser = await _service.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound();

        var authorizationResult = await _auth.AuthorizeAsync(
            User, existingUser.Id, "ResourceOwnerOrAdmin");

        if (!authorizationResult.Succeeded)
            return Forbid();
        
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