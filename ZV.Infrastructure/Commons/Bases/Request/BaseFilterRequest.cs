namespace ZV.Infrastructure.Commons.Bases.Request
{
    public class BaseFilterRequest : BasePaginationRequest 
    {
        public string _start_date { get; set; } = null;
        public string _end_date { get; set;} = null;
        /*public int? TransactionCode { get; set; } = null;
        public string? userid { get; set; } = null;
        */

    }
}
