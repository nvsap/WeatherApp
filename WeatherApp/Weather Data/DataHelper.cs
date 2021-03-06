﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.Net.Http;
using Windows.Data.Json;

namespace WeatherApp
{
    class DataHelper
    {
        public static WeatherDataStructures.Forecast7Days ProcessXML(XmlDocument xml)
        {
            WeatherDataStructures.Forecast7Days f7d = new WeatherDataStructures.Forecast7Days();
            XmlNode forecast;
            try
            {
                forecast = xml.LastChild;
            }
            catch (System.NullReferenceException)
            {
                return f7d;
            }

            foreach (XmlNode node in forecast.ChildNodes)
            {
                if ("location".Equals(node.Name))
                {
                    string loc = node.ChildNodes.Item(5).InnerText;
                    if (!"".Equals(node.ChildNodes.Item(3).InnerText))
                    {
                        loc += ", " + node.ChildNodes.Item(3).InnerText;
                    }
                    else
                    {
                        loc += ", " + node.ChildNodes.Item(4).InnerText;
                    }
                    f7d.Location = loc;
                }
                else if ("forecast".Equals(node.Name))
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        if ("txt_forecast".Equals(n.Name))
                        {
                            XmlNode forecastday = n.LastChild.FirstChild;
                            f7d.Imperial = forecastday.ChildNodes.Item(4).InnerText;
                            f7d.Metric = forecastday.ChildNodes.Item(5).InnerText;
                        }
                        else if ("simpleforecast".Equals(n.Name))
                        {
                            XmlNode forecastday = n.LastChild;
                            f7d.Forecast = Get7DayForecast(forecastday);
                        }
                    }
                }
            }

            return f7d;
        }

        //private static 

        private static WeatherDataStructures.SimpleForecastData[] Get7DayForecast(XmlNode forecastdays)
        {
            WeatherDataStructures.SimpleForecastData[] result = new WeatherDataStructures.SimpleForecastData[7];
            string[] data = new string[4];
            WeatherDataStructures.Temperature[] temp = new WeatherDataStructures.Temperature[2];

            int i = 0;
            foreach (XmlLinkedNode forecastday in forecastdays)
            {
                if (i >= 7) break;

                data[0] = forecastday.ChildNodes.Item(0).ChildNodes.Item(14).InnerText;
                temp[0] = new WeatherDataStructures.Temperature();
                temp[0].Fahrenheit = forecastday.ChildNodes.Item(2).ChildNodes.Item(0).InnerText;
                temp[0].Celsius = forecastday.ChildNodes.Item(2).ChildNodes.Item(1).InnerText;
                temp[1] = new WeatherDataStructures.Temperature();
                temp[1].Fahrenheit = forecastday.ChildNodes.Item(3).ChildNodes.Item(0).InnerText;
                temp[1].Celsius = forecastday.ChildNodes.Item(3).ChildNodes.Item(1).InnerText;
                data[1] = forecastday.ChildNodes.Item(4).InnerText;
                data[2] = forecastday.ChildNodes.Item(5).InnerText;
                data[3] = forecastday.ChildNodes.Item(6).InnerText;

                result[i] = new WeatherDataStructures.SimpleForecastData(data[0], temp[0], temp[1], data[1], data[2], data[3]);

                ++i;
            }

            return result;
        }

        public static async /*Task<Dictionary<string, string>string>*/void GetAutocompletedCities(string input, Dictionary<string, string> map)
        {
            JsonObject json;
            //Dictionary<string, string> map = new Dictionary<string, string>();
            string querry = "http://autocomplete.wunderground.com/aq?query=" + input;
            var client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync(querry);
            string jsonString = await msg.Content.ReadAsStringAsync();
            json = JsonValue.Parse(jsonString).GetObject();

            JsonArray cities = json.GetNamedArray("RESULTS");

            JsonValueType type = cities.GetObjectAt(0).ValueType;

            foreach(JsonValue value in cities)
            {
                JsonObject jo = JsonValue.Parse(value.ToString()).GetObject();
                if ("city".Equals(jo.GetNamedString("type")))
                {
                    string name = jo.GetNamedString("name");
                    string loc = jo.GetNamedString("l");
                    map.Add(name, loc);
                }
            }
            
        }
    }
}
