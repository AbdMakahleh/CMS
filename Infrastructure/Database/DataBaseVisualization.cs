using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database
{
    public struct DataBaseVisualization
    {
        public string PropertyName;
        public string PropertyVisualization;
        public DataBaseVisualization(string propertyName, string propertyVisualization)
        {
            this.PropertyName = propertyName;
            this.PropertyVisualization = propertyVisualization;
        }

        public DataBaseVisualization(string propertyName)
        {
            this.PropertyName = propertyName;
            this.PropertyVisualization = propertyName;
        }
    }
}
