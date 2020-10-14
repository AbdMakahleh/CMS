using Dapper;

namespace Infrastructure.Database
{
   public interface IStoredProcedure
    {
        DynamicParameters Parameters { get; set; }
        string Query { get; set; }
        DataBaseVisualization[] ResultColumns { get; set; }
    }
}
