using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace weatherApi
{
    
    [Activity(Label = "CityWeatherActivity", Icon = "@drawable/icon")]
    public class CityWeatherActivity : Android.App.Activity
    {
        // Global Variables
        double latx = 0.00;
        double laty = 0.00;
        //Button btntest;

     //   TextView STextView;
        //TextView TextWindSpeed;
        ImageView Simageview, Simageview2, Simageview3, Simageview4, Simageview5;
        TextView SwindSpeed, SwindDirection, SwindDirectionDegrees, StimeCurrent, SdateCurrent, SdateCurrent2, SdateCurrent3, SdateCurrent4, SdateCurrent5;
        TextView ScityTemperature, ScityTemperature2, ScityTemperature3, ScityTemperature4, ScityTemperature5;
        TextView Sskydesc, Sskydesc2, Sskydesc3, Sskydesc4, Sskydesc5;  
        TextView SunitTemp, ScityName;
        TextView textViewTimeCurrent, textViewDateCurrent;
        TextView Fskydesc2, Fskydesc3, Fskydesc4, Fskydesc5, nextDay;
    
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            GetSetWeather();
            GetDate();
        }
        //OnCreate END
               
        void GetSetWeather()
        {
            // Set our view from the "main" layout resource
            // We are now populating the "CityWeather" layout resource
            SetContentView(Resource.Layout.CityWeather);
            Simageview = FindViewById<ImageView>(Resource.Id.imageView1);
            Simageview2 = FindViewById<ImageView>(Resource.Id.imageView2);
            Simageview3 = FindViewById<ImageView>(Resource.Id.imageView3);
            Simageview4 = FindViewById<ImageView>(Resource.Id.imageView4);
            Simageview5 = FindViewById<ImageView>(Resource.Id.imageView5);
            ScityTemperature = FindViewById<TextView>(Resource.Id.cityTemp);
            ScityTemperature2 = FindViewById<TextView>(Resource.Id.cityTemp2);
            ScityTemperature3 = FindViewById<TextView>(Resource.Id.cityTemp3);
            ScityTemperature4 = FindViewById<TextView>(Resource.Id.cityTemp4);
            ScityTemperature5 = FindViewById<TextView>(Resource.Id.cityTemp5);
      
            SunitTemp = FindViewById<TextView>(Resource.Id.unittemp);
            Sskydesc = FindViewById<TextView>(Resource.Id.skydesc);
            Sskydesc2 = FindViewById<TextView>(Resource.Id.skydesc2);
            Sskydesc3 = FindViewById<TextView>(Resource.Id.skydesc3);
            Sskydesc4 = FindViewById<TextView>(Resource.Id.skydesc4);
            Sskydesc5 = FindViewById<TextView>(Resource.Id.skydesc5);
            StimeCurrent = FindViewById<TextView>(Resource.Id.timeCurrent);
            SdateCurrent = FindViewById<TextView>(Resource.Id.dateCurrent);//day current 1
            SdateCurrent2 = FindViewById<TextView>(Resource.Id.TextView02);//next day 2
            SdateCurrent3 = FindViewById<TextView>(Resource.Id.TextView03);//next day 3
            SdateCurrent4 = FindViewById<TextView>(Resource.Id.TextView04);//next day 4
            SdateCurrent5 = FindViewById<TextView>(Resource.Id.TextView05);//next day 5
            ScityName = FindViewById<TextView>(Resource.Id.cityName); // put city name into this view ::by intent

            //  DAYss
            // ********
            // IMAGE VIEW
            //DISPLAY IMAGE ICON: Loaded component URLImageViewHelper ::Get url from openweathermap to load their png images  
            //GET image for: WEATHER CURRENT::5th Reading ... 8 readings PER 24hr begin: 00 to 3am etc...
            string temp = Storage.listofforcasts[0].Forecast.Time[4].Symbol.Var;
            Koush.UrlImageViewHelper.SetUrlDrawable(Simageview, "http://openweathermap.org/img/w/" + temp + ".png");
            Simageview2 = FindViewById<ImageView>(Resource.Id.imageView2);

            string temp2 = Storage.listofforcasts[0].Forecast.Time[12].Symbol.Var;
            Koush.UrlImageViewHelper.SetUrlDrawable(Simageview2, "http://openweathermap.org/img/w/" + temp2 + ".png");
            
            Simageview3 = FindViewById<ImageView>(Resource.Id.imageView3);
            string temp3 = Storage.listofforcasts[0].Forecast.Time[20].Symbol.Var;
            Koush.UrlImageViewHelper.SetUrlDrawable(Simageview3, "http://openweathermap.org/img/w/" + temp3 + ".png");
            
            Simageview4 = FindViewById<ImageView>(Resource.Id.imageView4);
            string temp4 = Storage.listofforcasts[0].Forecast.Time[28].Symbol.Var;
            Koush.UrlImageViewHelper.SetUrlDrawable(Simageview4, "http://openweathermap.org/img/w/" + temp4 + ".png");
            
            Simageview5 = FindViewById<ImageView>(Resource.Id.imageView5);
            string temp5= Storage.listofforcasts[0].Forecast.Time[36].Symbol.Var;
            Koush.UrlImageViewHelper.SetUrlDrawable(Simageview5, "http://openweathermap.org/img/w/" + temp5 + ".png");
            
            // CLOUD VIEW - sky description
            //GET SKY DESCRIPTION
            Sskydesc.Text = Storage.listofforcasts[0].Forecast.Time[0].Clouds.Value;
            Sskydesc2.Text = Storage.listofforcasts[0].Forecast.Time[12].Clouds.Value;
            Sskydesc3.Text = Storage.listofforcasts[0].Forecast.Time[20].Clouds.Value;
            Sskydesc4.Text = Storage.listofforcasts[0].Forecast.Time[28].Clouds.Value;
            Sskydesc5.Text = Storage.listofforcasts[0].Forecast.Time[36].Clouds.Value;

            //GET CITY NAME
            // ScityName.Text = Storage.listofforcasts[0].Location.Name;
            ScityName.Text = Intent.GetStringExtra("Name");
            // *********************************************************

            //GET TEMPERATURE
            ScityTemperature.Text = Storage.listofforcasts[0].Forecast.Time[0].Temperature.Value;
            string tempTemperature = ScityTemperature.Text; //12.555 celsius
            string removeDecpoint = tempTemperature.Substring(0, tempTemperature.IndexOf('.'));  //elegant as stated in stackoverflow ;/
            Console.WriteLine("TEMPERATURE CELSIUS: {0}", tempTemperature);
            ScityTemperature.Text = removeDecpoint;
            //CITY TEMP VIEW
            ScityTemperature2.Text = Storage.listofforcasts[0].Forecast.Time[12].Temperature.Value;
            string tempTemperature2 = ScityTemperature2.Text; //12.555 celsius
            string removeDecpoint2 = tempTemperature2.Substring(0, tempTemperature2.IndexOf('.'));  //elegant as stated in stackoverflow ;/
            Console.WriteLine("TEMPERATURE CELSIUS DAY 2: ", tempTemperature2);
            ScityTemperature2.Text = removeDecpoint2;
            //3
            ScityTemperature3.Text = Storage.listofforcasts[0].Forecast.Time[20].Temperature.Value;
            string tempTemperature3 = ScityTemperature3.Text; //12.555 celsius
            string removeDecpoint3 = tempTemperature3.Substring(0, tempTemperature3.IndexOf('.'));  //elegant as stated in stackoverflow ;/
            Console.WriteLine("TEMPERATURE CELSIUS DAY 2: ", tempTemperature3);
            ScityTemperature3.Text = removeDecpoint3;
            //4
            ScityTemperature4.Text = Storage.listofforcasts[0].Forecast.Time[28].Temperature.Value;
            string tempTemperature4 = ScityTemperature4.Text; //12.555 celsius
            string removeDecpoint4 = tempTemperature2.Substring(0, tempTemperature4.IndexOf('.'));  //elegant as stated in stackoverflow ;/
            Console.WriteLine("TEMPERATURE CELSIUS DAY 2: ", tempTemperature2);
            ScityTemperature4.Text = removeDecpoint4;
            //5
            ScityTemperature5.Text = Storage.listofforcasts[0].Forecast.Time[36].Temperature.Value;
            string tempTemperature5 = ScityTemperature5.Text; //12.555 celsius
            string removeDecpoint5 = tempTemperature5.Substring(0, tempTemperature5.IndexOf('.'));  //elegant as stated in stackoverflow ;/
            Console.WriteLine("TEMPERATURE CELSIUS DAY 2: ", tempTemperature5);
            ScityTemperature5.Text = removeDecpoint5;

            //GET TEMPERATURE unit CELSIUS
            SunitTemp.Text = Storage.listofforcasts[0].Forecast.Time[4].Temperature.Unit;
            Console.WriteLine("||||||||| list of forecasts::  " + Storage.listofforcasts.Count);
            Console.WriteLine("||||||||| list of forecasts::  " + Storage.listofforcasts[0].Forecast.Time.Count);

            //GET TIME FROM Forecast.Time 0-9 in string
            StimeCurrent.Text = Storage.listofforcasts[0].Forecast.Time[0].From;
            string StimeCurrenttempstring = StimeCurrent.Text;
            string timecurrentTemp = StimeCurrenttempstring.Substring(11, 8);
            Console.WriteLine("Substring: {0}", StimeCurrenttempstring);
            StimeCurrent.Text = timecurrentTemp;

            //GET DATE FROM Forecast.Time 0-9 in string
            SdateCurrent.Text = Storage.listofforcasts[0].Forecast.Time[0].From;
            string SdateCurrenttempstring = SdateCurrent.Text;
            string datecurrentTemp = SdateCurrenttempstring.Substring(0, 10);
            Console.WriteLine("Substring: {0}", SdateCurrenttempstring);
            SdateCurrent.Text = datecurrentTemp;

        }
        void GetDate ()
		{           
            DateTime dt = DateTime.Now;
            SdateCurrent2.Text = (dt.DayOfWeek.ToString());           
            dt = dt.AddDays(1);
            SdateCurrent3.Text = (dt.DayOfWeek.ToString()); 
            dt = dt.AddDays(1);
            SdateCurrent4.Text = (dt.DayOfWeek.ToString()); 
            dt = dt.AddDays(1);
            SdateCurrent5.Text = (dt.DayOfWeek.ToString());              
        }
    }
}


















