using System;
//using System.Json; // add the referemce
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Locations;
using System.Collections.Generic;


namespace weatherApi
{
    [Activity(Label = "weatherApi", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity , ILocationListener
	{
        RESThandler objWA;
		LocationManager locMgr;
        string url, city, mycity,mygpscity;
        string latlng;
        string gpsurl;
		EditText txtLat;
		EditText txtLong;
		EditText txtAddress;
		Button btnUseLocation;
		
        Button btnOpenMap;
        List<string> listofcities = new List<string>();
        List<string> listofcityCoords = new List<string>();
       // Button btnOpenCityList;

        Spinner spCity;
        ImageView imgCity;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnUseLocation = FindViewById<Button> (Resource.Id.btnGetLocation);
			
            btnOpenMap = FindViewById<Button>(Resource.Id.btnShowinMap);
         //   btnOpenCityList = FindViewById<Button>(Resource.Id.btnOpenCityList);

			txtLat = FindViewById<EditText> (Resource.Id.txtLat);
			txtLong = FindViewById<EditText> (Resource.Id.txtLong);
			txtAddress = FindViewById<EditText> (Resource.Id.txtAddress);

            btnUseLocation.Click += btnUseLocation_Click;
			btnOpenMap.Click += OpenMapActivity;
         //   btnOpenCityList.Click += OpenCityList;

            //SPINCITY  POPULATE LIST WITH MAIN CITIES REQUIRED + THEIR LAT LON RELEVANT TO LIST POSITION FOR CITY
            spCity = FindViewById<Spinner>(Resource.Id.spCity);
            //imgCity = FindViewById<ImageView>(Resource.Id.imgCity); //if you require image
            //SET UP EVENT HANDLER
            spCity.ItemSelected += spCity_ItemSelected;

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.City_Names, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spCity.Adapter = adapter;



            // setuplists
            listofcities.Add("SELECT A CITY: VIEW CURRENT WEATHER");
            listofcities.Add("Auckland");
            listofcities.Add("Hamilton");
            listofcities.Add("Wellington");
            listofcities.Add("ChristChurch");
            listofcities.Add("Dunedin");
         
            listofcityCoords.Add("fdgdjfjtgdfgj");
            listofcityCoords.Add("lat=-36.866669&lon=174.766663"); //au
            listofcityCoords.Add("lat=-37.783329&lon=175.283325"); //ha 
            listofcityCoords.Add("lat=-36.908199&lon=174.82019"); //we
            listofcityCoords.Add("lat=-43.533329&lon=172.633331"); //cc  43.5300° S, 172.6203° E
            listofcityCoords.Add("lat=-45.874161&lon=170.503616"); // du  "lon":170.503616,"lat":-45.874161}

		}

        void btnUseLocation_Click(object sender, EventArgs e)
        {
           //test gps figures
           // Console.WriteLine("gpsurl-----------------" + gpsurl);
            LoadWeatherapiList(gpsurl,mygpscity);
            
        }

       public void spCity_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            latlng = listofcityCoords[e.Position]; //global
            city  = listofcities[e.Position];

            if (e.Position == 0)
            {

            }
            else
            {
                url = "http://api.openweathermap.org/data/2.5/forecast?mode=xml&units=metric&" + latlng;
                mycity = city;
               // Console.WriteLine(":::::::::::::::::::::::::: CITY name = " + mycity);
               // Console.WriteLine(":::::::::::::::::::::::::: CITY ITEM SELECTED URL = " + url);
                LoadWeatherapiList(url, mycity); // gts weather forcast from api
            }
        }

       //OnResume will Get GPS once then will stop (OnLocationChanged) until returned back to Main Activity then will fire again once
		protected override void OnResume ()
		{
			base.OnResume (); 
			// initialize location manager
			locMgr = GetSystemService (Context.LocationService) as LocationManager;
            GetLocation();
		}

		void GetLocation ()
		{
			Criteria locationCriteria = new Criteria();
			locationCriteria.Accuracy = Accuracy.Coarse;
			locationCriteria.PowerRequirement = Power.Medium;
			string locationProvider = locMgr.GetBestProvider(locationCriteria, true);
			if(locationProvider != null)
			{
				locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);
				Toast.MakeText(this,  "Provider:" + locationProvider,ToastLength.Short).Show();
			} 
			else 
			{
				Toast.MakeText(this,  "No location providers available",ToastLength.Short).Show();
			}
		}

        //GPS GET LATLONG URL DATA FOR weather
		public void OnLocationChanged (Android.Locations.Location location)
		{
			txtLat.Text = location.Latitude.ToString();
			txtLong.Text = location.Longitude.ToString ();
           // made button visible
            latlng = "&lat="+location.Latitude.ToString() + "&lon=" + location.Longitude.ToString();
            gpsurl = @"http://api.openweathermap.org/data/2.5/forecast?mode=xml&units=metric" + latlng;
           // Console.WriteLine(":::::::::::::::::::::::::: DETECTED_______________________________" + latlng);
            GetAddress();
           // turn off gps until return back to this layout & then Resume... (to get fresh GPS loc.)
            locMgr.RemoveUpdates(this);
		}

		public void OnProviderEnabled (string provider)
		{
			Toast.MakeText(this, "Provider Enabled",ToastLength.Short).Show();
		}
		public void OnProviderDisabled (string provider)
		{
			Toast.MakeText (this, "Provider Disabled", ToastLength.Short).Show ();
		}
		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			Toast.MakeText(this, "Provider status"  + status.ToString(),ToastLength.Short).Show();
		}
			
	    private async void GetAddress()
		{
			var geo = new Geocoder (this);
			var addresses = await geo.GetFromLocationAsync (Convert.ToDouble(txtLat.Text), Convert.ToDouble(txtLong.Text), 1);
			 
			if (addresses.Count > 0)
			{
              //  addresses.ToList().ForEach (addr => txtAddress.Text = addr.GetAddressLine(0) + "\n" + addr.GetAddressLine(1) + "\n" + addr.GetAddressLine(2));
              //  addresses.ToList().ForEach(addr => txtAddress.Text = addr.GetAddressLine(2));
                mygpscity=(addresses[0].Locality.ToString());
                locMgr.RemoveUpdates(this);
		    }
			else
			{
				Toast.MakeText(this, "No address Found",ToastLength.Short).Show();
			}
		}

		void OpenMapActivity (object sender, EventArgs e)
		{
			var mapactivity = new Intent (this, typeof(MapActivity));
			mapactivity.PutExtra ("Latitude", txtLat.Text);
			mapactivity.PutExtra ("Longitude", txtLong.Text);
			//mapactivity.PutExtra ("Address",txtAddress.Text);
            //MapActivity.dothis();
			StartActivity (mapactivity);
		}

        // Open new layout with WEATHER DATA OpenCityList
//void OpenCityList(object sender, EventArgs e){}

        //add the asynchronous call to load the list of data from open weather map api
        //data forecastreturn.cs with data //will be called from RESThandler.cs to serialize deserialize data
        public async void LoadWeatherapiList(string xurl,string citylabel)
		{
            objWA = new RESThandler(@xurl);
			var Response = await objWA.ExecuteRequestAsync ();
          //Storage.lstofEntry = Response.Forecast.Time[0].
		  // listNews.Adapter = new DataAdapter (this, Response.Entries.Entry);
            Storage.listofforcasts.Clear();
            Storage.listofforcasts.Add(Response);
            var cityActivity = new Intent(this, typeof(CityWeatherActivity));
            cityActivity.PutExtra("Name", citylabel);           
            StartActivity(cityActivity);          
		}
	}
}


