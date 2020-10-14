using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Classes
{
    public class MapSetting
    {
        public string PropertyName { get; set; }
        public string VirtualizationPropertyName { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsValueType { get; set; }
        public IEnumerable<MapSetting> Children { get; set; }
        public MapSetting(string propertyName, bool isValueType, bool isMultiple, string VirtualizationPropertyName)
        {
            this.PropertyName = propertyName;
            this.IsValueType = isValueType;
            this.IsMultiple = isMultiple;
            this.VirtualizationPropertyName = VirtualizationPropertyName.ToLower();
        }
        public MapSetting(string propertyName, bool isValueType, string VirtualizationPropertyName)
        {
            this.PropertyName = propertyName;
            this.IsValueType = isValueType;
            this.VirtualizationPropertyName = VirtualizationPropertyName.ToLower();
        }
        public MapSetting(string propertyName, bool isValueType, bool isMultiple, IEnumerable<MapSetting> childrens)
        {
            this.PropertyName = propertyName;
            this.IsValueType = isValueType;
            this.IsMultiple = isMultiple;
            this.Children = childrens;
        }

        public MapSetting(string propertyName, bool isValueType, bool isMultiple, IEnumerable<MapSetting> childrens, string VirtualizationPropertyName)
        {
            this.PropertyName = propertyName;
            this.IsValueType = isValueType;
            this.IsMultiple = isMultiple;
            this.Children = childrens;
            this.VirtualizationPropertyName = VirtualizationPropertyName.ToLower();
        }
    }
}
