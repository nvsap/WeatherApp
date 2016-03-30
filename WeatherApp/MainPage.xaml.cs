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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.ViewManagement;

namespace WeatherApp
{
    public sealed partial class MainPage : Page
    {
        XmlDocument doc = new XmlDocument();
        private enum Lang { EN, UA, RU };
        private string locCity = "autoip";
        private string locCountry = "";
        private Lang lang;

        public MainPage()
        {
            lang = Lang.EN;

            this.InitializeComponent();
            getWeather();
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
        
        private async void getWeather()
        {
            string url = "http://api.wunderground.com/api/a6743ab79712c4f3/geolookup/forecast10day/lang:"+ lang.ToString() +"/q/" + locCountry.ToString() + "/" + locCity.ToString() + ".xml";
            var client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync(new Uri(url));
            var xml = await msg.Content.ReadAsStringAsync();    
            doc.LoadXml(xml);
            WeatherDataStructures.Forecast7Days weather = DataHelper.ProcessXML(doc);
            FillLables(weather); 
        }
        
        private void FillLables(WeatherDataStructures.Forecast7Days weather)
        {
            this.TextLoc.Text = weather.Location;

            this.CurrDay.Text = weather.Forecast[0].Day;
            this.DayImg1.Source = new BitmapImage(new Uri(weather.Forecast[0].IconURL, UriKind.Absolute));
            this.DayHigh1.Text = weather.Forecast[0].Max.Celsius;
            this.NightHigh1.Text = weather.Forecast[0].Min.Celsius;
            this.TextWeather1.Text = weather.Metric;

            this.DayImg2.Source = new BitmapImage(new Uri(weather.Forecast[1].IconURL, UriKind.Absolute));
            this.TempDay1.Text = weather.Forecast[1].Max.Celsius;
            this.TempNight1.Text = weather.Forecast[1].Min.Celsius;
            this.textDay1.Text = weather.Forecast[1].Day;
            this.TextCond1.Text = weather.Forecast[1].Condition;

            this.DayImg3.Source = new BitmapImage(new Uri(weather.Forecast[2].IconURL, UriKind.Absolute));
            this.TempDay2.Text = weather.Forecast[2].Max.Celsius;
            this.TempNight2.Text = weather.Forecast[2].Min.Celsius;
            this.textDay2.Text = weather.Forecast[2].Day;
            this.TextCond2.Text = weather.Forecast[2].Condition;

            this.DayImg4.Source = new BitmapImage(new Uri(weather.Forecast[3].IconURL, UriKind.Absolute));
            this.TempDay3.Text = weather.Forecast[3].Max.Celsius;
            this.TempNight3.Text = weather.Forecast[3].Min.Celsius;
            this.textDay3.Text = weather.Forecast[3].Day;
            this.TextCond3.Text = weather.Forecast[3].Condition;

            this.DayImg5.Source = new BitmapImage(new Uri(weather.Forecast[4].IconURL, UriKind.Absolute));
            this.TempDay4.Text = weather.Forecast[4].Max.Celsius;
            this.TempNight4.Text = weather.Forecast[4].Min.Celsius;
            this.textDay4.Text = weather.Forecast[4].Day;
            this.TextCond4.Text = weather.Forecast[4].Condition;

            this.DayImg6.Source = new BitmapImage(new Uri(weather.Forecast[5].IconURL, UriKind.Absolute));
            this.TempDay5.Text = weather.Forecast[5].Max.Celsius;
            this.TempNight5.Text = weather.Forecast[5].Min.Celsius;
            this.textDay5.Text = weather.Forecast[5].Day;
            this.TextCond5.Text = weather.Forecast[5].Condition;

            this.DayImg7.Source = new BitmapImage(new Uri(weather.Forecast[6].IconURL, UriKind.Absolute));
            this.TempDay6.Text = weather.Forecast[6].Max.Celsius;
            this.TempNight6.Text = weather.Forecast[6].Min.Celsius;
            this.textDay6.Text = weather.Forecast[6].Day;
            this.TextCond6.Text = weather.Forecast[6].Condition;
        }

        private void OnLangClick(object sender, RoutedEventArgs e)
        {
            Lang prevLang = lang;
            if (sender is MenuFlyoutItem)
            {
                switch ((sender as MenuFlyoutItem).Name)
                {
                    case "UKR":
                    {
                        lang = Lang.UA;
                        break;
                    }
                    case "RUS":
                    {
                        lang = Lang.RU;
                        break;
                    }
                    default:
                    {
                        lang = Lang.EN;
                        break;
                    }
                }
            }

            if (lang != prevLang)
            {
                getWeather();
            }
         }
        private void OnClickLocation(object sender, RoutedEventArgs e)
        {
            string prevLocCity = locCity;
            string prevLocCountry = locCountry;
            locCity = CityS.Text;
            locCountry = CountryS.Text;
            if(String.IsNullOrEmpty(locCountry))
            {
                getWeather();
            }
            getWeather();
            //CountryS.Tex();
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Refresh_Button(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

        private void TextLoc_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnLocatioQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }
        
        private async void OnLocationTextCange(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string city = (sender as AutoSuggestBox).Text;
            Dictionary<string, string> map = new Dictionary<string, string>();
            DataHelper.GetAutocompletedCities(city, map);
            this.LocationBox.ItemsSource = map.Values;
            this.Debug.Text = city;
        }

        private void OnLocationSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }
    }

}
