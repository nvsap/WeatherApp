using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using Windows.Data.Json;

namespace WeatherApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        JsonObject o;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void getMsg()
        {
            string url = /*@*/"http://api.wunderground.com/api/a6743ab79712c4f3/forecast/lang:UA/q/zmw:00000.1.33345.json";
            var client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync(new Uri(url));
            var json = await msg.Content.ReadAsStringAsync();
            o = JsonValue.Parse(json).GetObject();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // string url = /*@*/"http://api.wunderground.com/api/a6743ab79712c4f3/forecast/lang:UA/q/zmw:00000.1.33345.xml";
            //XmlDocument doc = new XmlDocument();

            //doc.LoadXml("http://api.wunderground.com/api/a6743ab79712c4f3/forecast/lang:UA/q/zmw:00000.1.33345.xml");

            //var clinet = new HttpClient();

            //JsonObject o = null;
            getMsg();
            textBlock.Text = o != null ? o.ToString() : "Null!!!";

            //HttpResponseMessage msg = await client.GetAsync(new Uri(url));

            //textBlock.Text = doc.ToString();
           /* var m_strFilePath = "http://api.wunderground.com/api/a6743ab79712c4f3/forecast/lang:UA/q/zmw:00000.1.33345.xml";
            string xmlStr;
        
            using (var wc = new WebClient())
            {
                xmlStr = wc.DownloadString(m_strFilePath);
            }
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);*/

            //string s = "";
            //while(reader.Read())
            //{
            // s += reader.Name;
            // s += "\n";
            //}

            //textBlock.Text = s;
        }
        
    }

}
