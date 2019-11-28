using System.Collections.Generic;

namespace Untappd.Models
{
    public class CheckinResult
    {
        public List<Checkin> Checkins { get; set; }
        public long NextId { get; set; }
    }
}
