using Microsoft.AspNetCore.Mvc;
using TeamBuilder.API.Team.Infrastructure;
using TeamBuilder.DTO.Team.Infrastructure;

namespace TeamBuilder.API.Team.Application.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly InMemoryTeamStorage _inMemoryTeamStorage;

    public TeamController(ILogger<TeamController> logger)
    {
        _logger = logger;
        _inMemoryTeamStorage = new InMemoryTeamStorage();
    }

    [HttpGet("{id:Guid}/Members")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _inMemoryTeamStorage.GetAllTeamMembersByTeamId(id);
        return Ok(response);
    }

    [HttpPost("{id:Guid}/AddMember")]
    public async Task<IActionResult> AddTeamMember(Guid id, [FromBody] TeamMemberDto teamMember)
    {
        await _inMemoryTeamStorage.AddMembers(new List<TeamMemberDto> {teamMember});
        return Ok();
    }
}