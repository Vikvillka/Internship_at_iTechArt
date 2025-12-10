using CommunityHub.Contracts.DTOs.Common;
using CommunityHub.Domain.Common;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingSearch
{
    public static PagedResponse<TDto> ToPagedResponse<TEntity, TDto>(
        this PagedResult<TEntity> result,
        Func<TEntity, TDto> mapFunc
        )
    {
        return new PagedResponse<TDto>
        {
            Items = result.Items.Select(mapFunc).ToList(),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }
}

