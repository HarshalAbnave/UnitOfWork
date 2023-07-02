using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Core.IConfiguration;
using UnitOfWork.Models;

namespace UnitOfWork.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(
        ILogger<UsersController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost()]
    [ActionName("CreateUser")]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetUserById", new { user.Id}, user);
        }
        catch (Exception ex)
        {
            return Problem();
        }
    }

    [HttpGet()]
    [Route("{Id}")]
    [ActionName("GetUserById")]
    public async Task<IActionResult> GetById([FromRoute] Guid Id)
    {
        User user = await _unitOfWork.Users.GetById(Id);
        if (user is null) return NotFound();

        return Ok(user);
    }
}