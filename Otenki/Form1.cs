using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace Otenki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new RestClient();
            var request = new RestRequest();
            client.BaseUrl = new Uri("http://weather.livedoor.com/forecast/webservice/json/v1");

            request.Method = Method.GET;
            request.AddParameter("city", "230010", ParameterType.GetOrPost);
            var response = client.Execute(request);
            var tenki = Newtonsoft.Json.JsonConvert.DeserializeObject<Tenki>(response.Content);
            pictureBox1.ImageLocation = tenki.forecasts[1].image.url;
            label1.Text =
                tenki.forecasts[1].dateLabel + "の天気は" + tenki.forecasts[1].telop + "\r\n" +
                "最高気温：" + tenki.forecasts[1].temperature.max.celsius + "℃\r\n" +
                "最低気温：" + tenki.forecasts[1].temperature.min.celsius + "℃";
        }

        public class Tenki
        {
            public Pinpointlocation[] pinpointLocations { get; set; }
            public string link { get; set; }
            public Forecast[] forecasts { get; set; }
            public Location location { get; set; }
            public DateTime publicTime { get; set; }
            public Copyright copyright { get; set; }
            public string title { get; set; }
            public Description description { get; set; }
        }

        public class Location
        {
            public string city { get; set; }
            public string area { get; set; }
            public string prefecture { get; set; }
        }

        public class Copyright
        {
            public Provider[] provider { get; set; }
            public string link { get; set; }
            public string title { get; set; }
            public Image image { get; set; }
        }

        public class Image
        {
            public int width { get; set; }
            public string link { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public int height { get; set; }
        }

        public class Provider
        {
            public string link { get; set; }
            public string name { get; set; }
        }

        public class Description
        {
            public string text { get; set; }
            public DateTime publicTime { get; set; }
        }

        public class Pinpointlocation
        {
            public string link { get; set; }
            public string name { get; set; }
        }

        public class Forecast
        {
            public string dateLabel { get; set; }
            public string telop { get; set; }
            public string date { get; set; }
            public Temperature temperature { get; set; }
            public Image1 image { get; set; }
        }

        public class Temperature
        {
            public Min min { get; set; }
            public Max max { get; set; }
        }

        public class Min
        {
            public string celsius { get; set; }
            public string fahrenheit { get; set; }
        }

        public class Max
        {
            public string celsius { get; set; }
            public string fahrenheit { get; set; }
        }

        public class Image1
        {
            public int width { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public int height { get; set; }
        }


    }
}
