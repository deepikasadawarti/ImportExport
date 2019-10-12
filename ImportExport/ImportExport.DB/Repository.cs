using System.Collections.Generic;
using System.Linq;
using ImportExport.DB.Entities;

namespace ImportExport.DB
{
    public class Repository : IRepository
    {
        private ImportExportContext _context;

        public Repository(ImportExportContext context)
        {
            _context = context;
        }

        public IEnumerable<Charger> GetChargers()
        {
            return _context.Charger.OrderBy(n => n.chargerId);
        }

        public Charger GetCharger(int Id)
        {
            return _context.Charger.FirstOrDefault(n => n.chargerId == Id);
        }

        public Charger AddCharger(Charger charger)
        {
            _context.Charger.Add(charger);
            _context.SaveChanges();
            return charger;
        }

        //Station
        public IEnumerable<Station> GetStation()
        {
            return _context.Station.OrderBy(n => n.stationId);
        }

        public Station GetStation(int Id)
        {
            return _context.Station.FirstOrDefault(n => n.stationId == Id);
        }

        public Station AddStation(Station station)
        {
            _context.Station.Add(station);
            _context.SaveChanges();
            return station;
        }

        //Site
        public IEnumerable<Site> GetSites()
        {
            return _context.Site.OrderBy(n => n.siteId);
        }

        public Site GetSite(int Id)
        {
            return _context.Site.FirstOrDefault(n => n.siteId == Id);
        }

        public Site AddSite(Site site)
        {
            _context.Site.Add(site);
            _context.SaveChanges();
            return site;
        }
    }
}