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

        var check = await repository.SelectAll()
                                         .Where(ur => ur.Id == id)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync()
                                         ?? throw new TechStationException(404, "UserRole not found");

        var entity = await repository.SelectAll()
                                         .Where(ur => ur.UserId == userRole.UserId && ur.Role == userRole.Role)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync()
                                         ?? throw new TechStationException(409, "UserRole already exists");

        var mappedUserRole = mapper.Map(userRole, check);
        mappedUserRole.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(mappedUserRole);

        return mapper.Map<UserRoleForResultDto>(mappedUserRole);
    }
}