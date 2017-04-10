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
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Locations;
using Android.Gms.Maps.Model;
using Android.Gms.Location.Places;
using Android.Gms.Location;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Location.Places.UI;
using Android.Gms.Common;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace CheckInAppV3
{
    [Activity(Label = "MapActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MapActivity : Activity, IOnMapReadyCallback/*, Android.Gms.Location.ILocationListener*/
    {
        //private SupportToolbar mToolBar;
        int PLACE_PICKER_REQUEST = 1;
        /*Google Maps location service*/
        LocationManager locMgr;
        /*Google Map*/
        private GoogleMap GMap;
        private int REQUEST_PLACE_PICKER = 1;


        /*Setting up marker for current position*/
        private void SetMarker(LatLng coord)
        {
            GMap.AddMarker(new MarkerOptions()
                .SetPosition(coord)
                .SetTitle("NAU"));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home);
            ImageButton checkInButt = FindViewById<ImageButton>(Resource.Id.checkInButt);
            SetUpMap();
            checkInButt.Click += (s, e) => {
                //Location loc = GMap.MyLocation;
                //Location loc = new Location(locMgr.GetBestProvider(new Criteria(), true));
                PlacesPicker();
                //Bundle extras = Intent.Extras;
                //SetMarker(new LatLng(Convert.ToDouble(extras.Get("lat")), Convert.ToDouble(extras.Get("lon"))));
            };
            //mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(mToolBar);
        }
        
        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        private void PlacesPicker()
        {
            try
            {
                PlacePicker.IntentBuilder intentBuilder = new PlacePicker.IntentBuilder();
                Intent intent = intentBuilder.Build(this);
                StartActivityForResult(intent, REQUEST_PLACE_PICKER);
            }
            catch (GooglePlayServicesRepairableException e)
            {
            }
            catch (GooglePlayServicesNotAvailableException e)
            {
            }
        }

        protected void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PLACE_PICKER_REQUEST)
            {
                if (resultCode == Result.Ok)
                {
                    IPlace place = PlacePicker.GetPlace(this, data);
                    String toastMsg = String.Format("Place: %s", place.NameFormatted);
                    Toast.MakeText(this, toastMsg, ToastLength.Long).Show();
                    //SetMarker(place.LatLng);
                    data.PutExtra("lat", place.LatLng.Latitude);
                    data.PutExtra("lon", place.LatLng.Longitude);
                }
            }
        }


        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.UiSettings.SetAllGesturesEnabled(true);
            googleMap.MyLocationEnabled = true;
            this.GMap = googleMap;
            //PlacesPicker();
        }
    }
}