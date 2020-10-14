
using Infrastructure.Classes;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static object MapAsJson(this object source, List<MapSetting> settings, bool isMultiple = false)
        {
            List<JObject> result = new List<JObject>();
            IList objectLists;
            if (isMultiple)
            {
                objectLists = (IList)source;
            }

            else
            {
                objectLists = new List<object>() { source };
            }


            int objectsCount = objectLists.Count;

            for (int i = 0; i < objectsCount; i++)
            {
                result.Add(_mapObject(objectLists[i], settings));
            }

            if (!isMultiple)
            {
                return result.First();
            }

            else
            {
                return result;
            }
        }

        private static JObject _mapObject(object source, List<MapSetting> settings)
        {
            dynamic mappedObject = new JObject();
            int count = settings.Count;
            for (int i = 0; i < count; i++)
            {
                var currentSetting = settings[i];
                if (currentSetting.IsValueType)
                {
                    if (!string.IsNullOrEmpty(currentSetting.VirtualizationPropertyName))
                    {
                        var result = source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source);
                        if (result != null)
                            mappedObject[currentSetting.VirtualizationPropertyName] = JToken.FromObject(source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source));
                        else
                            mappedObject[currentSetting.VirtualizationPropertyName] = null;

                    }
                    else
                    {
                        var result = source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source);
                        if (result != null)
                            mappedObject[currentSetting.PropertyName] = JToken.FromObject(result);
                        else
                            mappedObject[currentSetting.PropertyName] = null;
                    }

                    continue;
                }

                if (!currentSetting.IsMultiple)
                {
                    if (!string.IsNullOrEmpty(currentSetting.VirtualizationPropertyName))
                    {


                        var result = _mapObject(source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source), currentSetting.Children.ToList());
                        if (result != null)
                            mappedObject[currentSetting.VirtualizationPropertyName] = JToken.FromObject(result);
                        else
                            mappedObject[currentSetting.VirtualizationPropertyName] = null;

                    }
                    else
                    {
                        var result = _mapObject(source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source), currentSetting.Children.ToList());
                        if (result != null)
                            mappedObject[currentSetting.PropertyName] = JToken.FromObject(result);
                        else
                            mappedObject[currentSetting.PropertyName] = null;
                    }
                    continue;
                }

                if (currentSetting.IsMultiple)
                {
                    IEnumerable objectIteration = (IEnumerable)source.GetType().GetProperty(currentSetting.PropertyName).GetValue(source);
                    List<JObject> result = new List<JObject>();
                    foreach (var item in objectIteration)
                    {
                        object currentObject = item;
                        result.Add(_mapObject(currentObject, currentSetting.Children.ToList()));
                    }

                    if (!string.IsNullOrEmpty(currentSetting.VirtualizationPropertyName))
                    {
                        if (result != null)
                            mappedObject[currentSetting.VirtualizationPropertyName] = JToken.FromObject(result);
                        else
                            mappedObject[currentSetting.VirtualizationPropertyName] = null;

                    }
                    else
                    {
                        if (result != null)
                            mappedObject[currentSetting.PropertyName] = JToken.FromObject(result);
                        else
                            mappedObject[currentSetting.PropertyName] = null;

                    }

                }
            }

            return mappedObject;
        }

    }
}
