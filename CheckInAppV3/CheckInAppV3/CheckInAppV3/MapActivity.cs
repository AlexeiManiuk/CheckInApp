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
using CheckInAppV3.checkInRef;
using static Android.Gms.Maps.GoogleMap;

namespace CheckInAppV3
{
    [Activity(Label = "MapActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MapActivity : FragmentActivity, IOnMapReadyCallback, IOnMarkerClickListener
    {
        public List<LatLng> Coords { get; set; }
        public object Distance { get; private set; }

        /*Google Map*/
        private GoogleMap GMap;
        private int REQUEST_PLACE_PICKER = 1;

        private void GetCoordinates()
        {
            /*
            checkInRef.Check_InService client= new Check_InService();
            var obj = client.GetCoordinates(Intent.GetStringExtra("Login"));
            foreach (TripleL i in obj)
            {
                Coords.Add(new LatLng(i.Latitude, i.Longitude));
            }
            SetMarkers();*/
        }

        ListView listView;

         private void SetMarkers()
        {
            foreach (var coord in Coords)
            {
                var marker = new MarkerOptions();
                marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
                GMap.AddMarker(marker.SetPosition(coord));
            }
        }

        private void AddNewMarker(double lat, double lon)
        {
            var client = new Check_InService();
            client.AddNewCoordinates(lat, lon, Intent.GetStringExtra("Login"));
        }

        /*Setting up marker for current position*/
        private void SetMarker(LatLng coord, string title)
        {
            GMap.AddMarker(new MarkerOptions()
                .SetPosition(coord)
                .SetTitle(title));
            Coords.Add(coord);
            AddNewMarker(coord.Latitude, coord.Longitude);
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home);
            ImageButton checkInButt = FindViewById<ImageButton>(Resource.Id.checkInButt);
            SetUpMap();
            Coords = new List<LatLng>();
            GetCoordinates();

            PopulateListView();
            checkInButt.Click += (s, e) => {
                PlacesPicker();
            };
        }

        private void PopulateListView()
        {
            listView = FindViewById<ListView>(Resource.Id.left_drawer);

            List<string> items = new List<string>
            {
                Intent.GetStringExtra("Login") ?? "Data not found",
                "\tMy profile\n",
                "My friends",
                "My messages",
                "My places",
                "\n\n",
                "Settings"
            };

            ArrayAdapter<string> stringAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

            listView.Adapter = stringAdapter;
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_PLACE_PICKER)
            {
                if (resultCode == Result.Ok)
                {
                    IPlace place = PlacePicker.GetPlace(this, data);
                    SetMarker(place.LatLng, place.NameFormatted.ToString());
                }
            }
        }
        

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.UiSettings.SetAllGesturesEnabled(true);
            googleMap.MyLocationEnabled = true;
            this.GMap = googleMap;
            GMap.SetOnMarkerClickListener((IOnMarkerClickListener) this);

            CameraPosition cameraPosition = new CameraPosition.Builder()
                .Target(new LatLng(50.4403017, 30.4301508))
                .Zoom(15)                 
                .Build();
            GMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));

            GMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(50.4403017, 30.4301508)).SetTitle("Nau"));
            GMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(50.4097209, 30.6465824)).SetTitle("Home").SetSnippet("Home sweet home"));
            GMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(50.4400762, 30.4319962)).SetTitle("Forsage").SetSnippet("Bomzhatik"));
            GMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(50.4471729, 30.4621784)).SetTitle("Politechnichna station").SetSnippet("Metro station"));
            GMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(50.3619176, 30.9220831)).SetTitle("Aeromall").SetSnippet("Borispils fines"));
        }

        public bool OnMarkerClick(Marker marker)
        {
            if (marker != null)
            {
                marker.ShowInfoWindow();
                return true;
            }
            else
                return false;
        }
    }
}