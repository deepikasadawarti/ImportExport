using System;

namespace ImportExport.DB.Entities
{
    public class Charger
    {

        public int chargerId { get; set; }
        public Guid chargerUuid { get; set; }
        public string chargerFriendlyName { get; set; }

        //References
        public int stationId { get; set; }
        public Station Station { get; set; }
    }
}
