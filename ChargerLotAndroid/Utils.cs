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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace ChargerLotAndroid
{
    public class Utils
    {
        public static LatLng ConvertLatLng(Google.Type.LatLng l) => new LatLng(l.Latitude, l.Longitude);
    }
}