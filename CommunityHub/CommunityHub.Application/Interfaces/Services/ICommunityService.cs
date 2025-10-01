using CommunityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityHub.Application.Interfaces.Services
{
    public interface ICommunityService
    {
        Task<IList<Community>> GetAllAsync();
        Task<Community?> GetByIdAsync(Guid id);
        Task<Community> CreateAsync(Community community);
        Task<bool> UpdateAsync(Community community);
        Task<bool> DeleteAsync(Guid id);
        Task<IList<Community>> SearchAsync(string? category, string? city, string? country);
    }
}
