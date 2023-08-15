using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Domain
{
    public class PlayerInfo
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string ShirtNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string PreferredFoot { get; set; }
    }
}
