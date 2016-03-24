using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    class WeatherDataStructures
    {
        public struct forecast
        {
            string text;
            SimpleForecastData[] forecastWeek;
        }

        public struct Forecast7Days
        {
            string location;
            string textImperial;
            string textMetric;
            SimpleForecastData[] forecastWeek;

            public string Location { get { return location; } set { location = value; } }
            public string Imperial { get { return textImperial; } set { textImperial = value; } }
            public string Metric { get { return textMetric; } set { textMetric = value; } }
            public SimpleForecastData[] Forecast { get { return forecastWeek; } set { forecastWeek = value; } }

            public Forecast7Days(string location, string imperial, string metric, SimpleForecastData[] forecast)
            {
                this.location = location;
                this.textImperial = imperial;
                this.textMetric = metric;
                this.forecastWeek = forecast;
            }
        }

        public struct SimpleForecastData
        {
            //Date date;
            string day;
            Temperature max;
            Temperature min;
            string condition;
            string iconName;
            string iconURL;
            //Wind maxWind;
            //Wind averageWind;

            public string Day { get { return day; } set { day = value; } }
            public Temperature Max { get { return max; } set { max = value; } }
            public Temperature Min { get { return min; } set { min = value; } }
            public string Condition { get { return condition; } set { condition = value; } }
            public string IconName { get { return iconName; } set { iconName = value; } }
            public string IconURL { get { return iconURL; } set { iconURL = value; } }
            //public Wind MaxWind { get { return maxWind; } set { maxWind = value; } }
            //public Wind AverageWind { get { return averageWind; } set { averageWind = value; } }

            public SimpleForecastData(/*Date date*/string day, Temperature max, Temperature min, string condition, string iconName, string iconURL/*, Wind maxWind, Wind averageWind*/)
            {
                this.day = day;
                this.max = max;
                this.min = min;
                this.condition = condition;
                this.iconName = iconName;
                this.iconURL = iconURL;
                //this.maxWind = maxWind;
                //this.averageWind = averageWind;
            }
        }

        public struct Date
        {
            string day { get; set; }
            string month;
            string year;
            string monthName;
            string monthNameShort;
            string weekDay;
            string weekDayShort;
        }

        public struct Temperature
        {
            string celsius;
            string fahrenheit;

            public string Celsius { get { return celsius; } set { celsius = value; } }
            public string Fahrenheit { get { return fahrenheit; } set { fahrenheit = value; } }


        }

        public struct Wind
        {
            string kph;
            string mph;
            string dir;
            string degreed;

            public string KpH { get { return kph; } set { kph = value; } }
            public string MpH { get { return mph; } set { mph = value; } }
            public string Direction { get { return dir; } set { dir = value; } }
            public string Degreed { get { return degreed; } set { degreed = value; } }

        }

        public struct Height
        {
            string cm;
            string inch;
        }
    }
}
