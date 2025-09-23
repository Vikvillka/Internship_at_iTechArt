using CommunityHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.API.Repositories.Interfaces;

public interface ICommunityRepository : IRepository<Community>
{
    Task<IList<Community>> SearchAsync(string? category, string? city, string? country);
}