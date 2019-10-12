using System.Collections.Generic;

namespace ImportExport.DB.Entities
{
    public class Site
    {
        public Site()
        {
            this.stations = new List<Station>();
        }

        public int siteId { get; set; }
        public string siteName { get; set; }

        //one site can has multiple stations
        public List<Station> stations { get; set; }
    }
}
