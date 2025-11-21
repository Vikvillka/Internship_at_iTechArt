namespace Gateway.API.Services.Сache.CacheHelpers;

public class UserCacheKeyHelper
{
    private const string RecordByIdPrefix = "User:";

    public static string GetRecordByIdKey(Guid id) => $"{RecordByIdPrefix}{id}";
}
