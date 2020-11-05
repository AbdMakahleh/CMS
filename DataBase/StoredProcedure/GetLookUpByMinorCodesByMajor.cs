using Dapper;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.StoredProcedure
{
    public class GetLookUpByMajorCodeSP : IStoredProcedure
    {
        public DynamicParameters Parameters { get; set; }
        public string Query { get; set; }
        public DataBaseVisualization[] ResultColumns { get; set; }
        public GetLookUpByMajorCodeSP(string majorCode)
        {
            Query = @"select * from ""Setup"".""GetLookUpByMajorCode""(@majorCode);";
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
