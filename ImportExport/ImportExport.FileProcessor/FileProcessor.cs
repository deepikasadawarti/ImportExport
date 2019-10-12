using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using ImportExport.DB;
using ImportExport.DB.Entities;
using Microsoft.Data.SqlClient;

namespace ImportExport.FileProcessor
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IRepository _repository;
        public FileProcessor(IRepository repository)
        {
            _repository = repository;
            SpreadsheetInfo.SetLicense("FREE-LIMITED_KEY");
        }

        public List<Site> LoadInDB (string filePath)
        {
            var filedata = LoadFileDataInMemory(filePath);

            var siteList = filedata.ToList().Select(n => new Site() { siteName = n.SiteName }).ToList().Distinct().ToList();

            foreach (var data in filedata)
            {
                foreach (var site in siteList)
                {
                    //File Load Logic to DB
                    _repository.AddSite(site);
                    var stationList = filedata.ToList().Select(s => new Station() { stationName = s.StationName, stationBoxId = s.StationBoxId, stationUuId = s.StationUuId }).Where(s => s.stationId == site.siteId).ToList().Distinct().ToList();
                    foreach (var station in stationList)
                    {
                        _repository.AddStation(station);
                        var chargerList = filedata.ToList().Select(c => new Charger() { chargerUuid = c.outletUuId, chargerFriendlyName = c.OutletFriendlyName }).Where(c => c.chargerId == station.stationId).ToList().Distinct().ToList();
                        foreach (var charger in chargerList)
                        {
                            _repository.AddCharger(charger);
                        }
                    }

                }

            }
            return siteList;
        }


       


        private List<FileData> LoadFileDataInMemory(string filePath)
        {
            var filedateList = new List<FileData>();
            try
            {
                var workbook = ExcelFile.Load(filePath);
                foreach (var worksheet in workbook.Worksheets)
                {
                    foreach (var row in worksheet.Rows)
                    {
                        if (row.Index > 0)
                        {
                            if (row.AllocatedCells != null)
                            {
                                filedateList.Add(new FileData()
                                {
                                    SiteName = row.AllocatedCells[0].Value.ToString(),
                                    StationName = row.AllocatedCells[1].Value.ToString(),
                                    StationBoxId = row.AllocatedCells[2].Value.ToString(),
                                    StationUuId = Guid.Parse(row.AllocatedCells[3].Value.ToString()),
                                    outletUuId = Guid.Parse(row.AllocatedCells[4].Value.ToString()),
                                   // outletId = row.AllocatedCells[5].Value.ToString(),
                                    OutletFriendlyName = row.AllocatedCells[5].Value.ToString(),
                                });
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return filedateList;
        }
    }

    public class FileData
    {
        public string SiteName { get; set; }
        public string StationName { get; set; }
        public string StationBoxId { get; set; }
        public Guid StationUuId { get; set; }
        public Guid outletUuId { get; set; }
       // public string outletId { get; set; }
        public string OutletFriendlyName { get; set; }


    }
}