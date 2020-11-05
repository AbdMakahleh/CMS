using Dapper;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.StoredProcedure
{
    public class GetLookUpByMinorCodesByMajorSP : IStoredProcedure
    {
        public DynamicParameters Parameters { get; set; }
        public string Query { get; set; }
        public DataBaseVisualization[] ResultColumns { get; set; }
        public GetLookUpByMinorCodesByMajorSP(string majorCode)
        {
            Query = @"select * from ""Setup"".""GetLookUpByMinorCodesByMajor""(@majorCode);";
            Parameters = new DynamicParameters();
            Parameters.Add("majorCode", majorCode);
            ResultColumns = new DataBaseVisualization[4];
            ResultColumns[0] = new DataBaseVisualization("Id", "id");
            ResultColumns[1] = new DataBaseVisualization("MajorCode", "majorCode");
            ResultColumns[2] = new DataBaseVisualization("MinorCode", "minorCode");
            ResultColumns[3] = new DataBaseVisualization("Value", "value");


        }
    }
}
