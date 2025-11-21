namespace Gateway.API.Services.Сache.CacheHelpers;

public static class CommunityCacheKeyHelper
{
    private const string RecordByIdPrefix = "Community:";

    public static string GetRecordByIdKey(Guid id) => $"{RecordByIdPrefix}{id}";
}
