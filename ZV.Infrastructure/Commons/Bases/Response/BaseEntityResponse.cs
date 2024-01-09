namespace ZV.Infrastructure.Commons.Bases.Response
{
    public class BaseEntityResponse <T>
    {   
        //Total transaction Cost
        public decimal ? TotalTransactionsSum { get; set; }
        public int? CountRegister { get; set; }
        public List<T>? Items { get; set; }

    }
}
