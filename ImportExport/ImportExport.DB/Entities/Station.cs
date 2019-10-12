using System;
using System.Collections.Generic;

namespace ImportExport.DB.Entities
{
    public class Station
    {
        public Station()
        {
            this.chargers = new List<Charger>();
        }

        public int stationId { get; set; }
        public string stationName { get; set; }
        public string stationBoxId { get; set; }
        public Guid stationUuId { get; set; }
       

        //References
        public int siteId { get; set; }
        public Site Site { get; set; }

        //one station  can has many chargers
        public List<Charger> chargers { get; set; }

        
    }
}
