namespace ZV.Infrastructure.Commons.Bases.Request
{
    public class UserFilterRequest
    {
        public string userid { get; set; } = "";
        public string pass { get; set; } = "";
        public string username { get; set; } = "";
        public bool usertype { get; set; } = false;

    }
}
