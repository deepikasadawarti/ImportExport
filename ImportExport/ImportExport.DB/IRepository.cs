using System.Collections.Generic;
using ImportExport.DB.Entities;

namespace ImportExport.DB
{
    public interface IRepository
    {
        IEnumerable<Charger> GetChargers();
        Charger GetCharger(int chargerId);
        Charger AddCharger(Charger charger);

        IEnumerable<Station> GetStation();
        Station GetStation(int Id);
        Station AddStation(Station charger);

        IEnumerable<Site> GetSites();
        Site GetSite(int chargerId);
        Site AddSite(Site charger);
    }
}