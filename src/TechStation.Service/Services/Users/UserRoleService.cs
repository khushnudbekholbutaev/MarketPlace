using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Users;

namespace TechStation.Service.Services.Users;

public class UserRoleService : IUserRoleService
{
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IRepository<UserRole> repository;

    public UserRoleService(
        IMapper mapper,
        IUserService userService,
        IRepository<UserRole> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
    }



    public async Task<UserRoleForResultDto> AddUserRoleAsync(UserRoleForCreationDto userRole)
    {
        var user = await userService.RetrieveByIdasync(userRole.UserId)
            ?? throw new TechStationException(404, "User is not found");

        var existing = await repository.SelectAll()
            .FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId);

        if (existing is not null)
        {
            mapper.Map(userRole, existing);
            existing.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(existing);
            return mapper.Map<UserRoleForResultDto>(existing);
        }

        var mappedUserRole = mapper.Map<UserRole>(userRole);
        mappedUserRole.CreatedAt = DateTime.UtcNow;

        await repository.InsertAsync(mappedUserRole);
        return mapper.Map<UserRoleForResultDto>(mappedUserRole);
    }


    public async Task<bool> DeleteUserRoleAsync(long id)
    {
        var check = await repository.SelectAll()
                                                .Where(c => c.Id == id)
                                                .FirstOrDefaultAsync()
                                                ?? throw new TechStationException(404, "User is not found");

        return await repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserRoleForResultDto>> GetAllUserRolesAsync(PaginationParams @params)
    {
        var userRoles = await repository.SelectAll()
                                            .ToPagedList(@params)
                                            .AsNoTracking()
                                            .ToListAsync();

        return mapper.Map<IEnumerable<UserRoleForResultDto>>(userRoles);
    }

    public async Task<IEnumerable<UserRoleForResultDto>> GetUserRoleByRoleNameAsync(Role role)
    {
        var result = await repository.SelectAll()
                                   .Where(u => u.Role == role)
                                   .AsNoTracking()
                                   .ToListAsync()
                                   ?? throw new TechStationException(404, "Role not found");

        return mapper.Map<IEnumerable<UserRoleForResultDto>>(result);
    }

    public async Task<UserRoleForResultDto> UpdateUserRoleAsync(long id, UserRoleForUpdateDto userRole)
    {
        var user = await userService.RetrieveByIdasync(userRole.UserId)
            ?? throw new TechStationException(404, "User not found");

        var existing = await repository.SelectAll()
            .FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId);

        if (existing is not null && existing.Id != id)
            throw new TechStationException(409, "A different UserRole with this UserId and Role already exists");

        var toUpdate = await repository.SelectAll()
            .FirstOrDefaultAsync(ur => ur.Id == id)
            ?? throw new TechStationException(404, "UserRole not found");

        mapper.Map(userRole, toUpdate);
        toUpdate.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(toUpdate);

        return mapper.Map<UserRoleForResultDto>(toUpdate);
    }

}