namespace Gateway.API.DTOs.EventDTOs
{
    public class GatewayEventSearchRequest
    {
        public string? Keywords { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
