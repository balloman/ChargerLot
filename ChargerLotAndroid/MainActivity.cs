using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using AndroidX.Fragment.App;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using ChargerLotLib;
using ChargerLotLib.Handlers;
using ChargerLotLib.Models;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;

namespace ChargerLotAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        // ReSharper disable once InconsistentNaming
        private readonly string[] PERMISSIONS = 
        {
            Manifest.Permission.AccessNetworkState,
            Manifest.Permission.AccessMockLocation,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.LocationHardware,
            Manifest.Permission.Internet
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            if (ContextCompat.CheckSelfPermission(this,
                    Manifest.Permission.Internet) == (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS, 420);
            }

            var mapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Hide();
            fab.Click += FabOnClick;
        }

        public void SendReport()
        {
            ReportController.ReportUserPark(new ParkingReport
            {
                
            });
        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (grantResults[0]== Permission.Denied)
            {
                Finish();
            }
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /**
         * Configure our map settings
         */
        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(34.7271267,-86.6408495),
                16.75f));
        }
    }
}

