using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Data.Repositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.DTOs.Users;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Users;

namespace TechStation.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    public UserService(IMapper mapper, IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var user = await userRepository.SelectAll()
            .Where(u => u.Email == dto.Email)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is not  null)
            throw new TechStationException(409, "User is already exists");

        var mapped = mapper.Map<User>(dto);
        mapped.Role = Role.user;
        mapped.CreatedAt = DateTime.UtcNow;
        await userRepository.InsertAsync(mapped);

        return mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await userRepository.SelectAll()
           .Where(u => u.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (user is null)
            throw new TechStationException(404, "User is not found");

        var mapped = mapper.Map(dto, user);
        mapped.UpdatedAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(mapped);

        return mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await userRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TechStationException(404, "User is not found");

        await userRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await userRepository.SelectAll()
            .Include(u => u.Orders)
            .Include(u => u.Favourites)
            .Include(u => u.CartItems)
            .ToPagedList(@params)
             .AsNoTracking()
             .ToListAsync();

        if (users is null)
            throw new TechStationException(404, "User is not found!");

        return mapper.Map<IEnumerable<UserForResultDto>>(users);
    }
    public async Task<int> CountAsync()
    {
        return await userRepository.CountAsync();
    }
    public async Task<UserForResultDto> RetrieveByIdasync(long id)
    {
        var user = await userRepository.SelectAll()
               .Where(u => u.Id == id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

        if (user is null)
            throw new TechStationException(404, "User is not found!");

        return mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserRoleForResultDto> AssignRoleToUser(UserRoleForCreationDto dto)
    {
        var user = await userRepository.SelectByIdAsync(dto.UserId);
        if (user == null)
            throw new TechStationException(404, "User not found");
        user.Role = dto.Role;

        await userRepository.UpdateAsync(user);

        return new UserRoleForResultDto
        {
            UserId = user.Id,
            User = mapper.Map<UserForResultDto>(user),
            Role = user.Role
        };
    }
}
