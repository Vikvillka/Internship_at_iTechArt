using HistoryService.Application.Intefaces.Repositories;
using HistoryService.Domain.Entities;
using HistoryService.Infrastructure.Data;

namespace HistoryService.Infrastructure.Repositories;

public class HistoryRecordRepository : EfRepository<HistoryRecord>, IHistoryRecordRepository
{
    public HistoryRecordRepository(HistoryServiceDbContext context) : base(context) { }
}
