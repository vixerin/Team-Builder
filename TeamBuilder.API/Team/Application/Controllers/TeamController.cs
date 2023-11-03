﻿using Microsoft.AspNetCore.Mvc;
using TeamBuilder.API.Team.Infrastructure;
using TeamBuilder.Common.Functional;
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
    public async Task<ResultDto<List<TeamMemberDto>>> Get(Guid id)
    {
        try
        {
            var response = await _inMemoryTeamStorage.GetAllTeamMembersByTeamId(id);
            return response.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return Result.Fail<List<TeamMemberDto>>(ex.Message).ToDto();
        }
    }

    [HttpPost("{id:Guid}/AddMembers")]
    public async Task<ResultDto> AddTeamMembers(Guid teamId, [FromBody] List<TeamMemberDto> teamMemberDtos)
    {
        try
        {
            var addMembersResult = await _inMemoryTeamStorage.AddMembers(teamId, teamMemberDtos);
            return addMembersResult.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return Result.Fail(ex.Message).ToDto();
        }
    }
}