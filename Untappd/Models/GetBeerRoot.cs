namespace Untappd.Models
{
    public class GetBeerRoot
    {
        public Meta Meta { get; set; }
        public object[] notifications { get; set; }
        public GetBeerResponse Response { get; set; }
    }


}
