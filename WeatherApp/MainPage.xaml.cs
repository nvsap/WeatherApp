using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WeatherNet;
using WeatherNet.Clients;
using WeatherNet.Model;
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
//using System.Web;


namespace WeatherApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SingleResult<CurrentWeatherResult> result;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            String st = String.Format(@"http://api.wunderground.com/api/a6743ab79712c4f3/forecast/lang:UA/q/zmw:00000.1.33345.json");
            doc.LoadXml(st);
            textBlock.Text = doc.ToString();
            //try
            //{
            //    //doc.LoadXml(st);
            //    textBlock.Text = /*doc.ToString()*/st;
            //}
            //catch(System.Exception ex)
            //{
            //    textBlock.Text = ex.ToString();
            //}
        }
    }
}
