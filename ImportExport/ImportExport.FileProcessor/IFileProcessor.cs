using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImportExport.DB.Entities;

namespace ImportExport.FileProcessor
{
    public interface IFileProcessor
    {

        List<Site> LoadInDB(string filePath);
    

    }
}
