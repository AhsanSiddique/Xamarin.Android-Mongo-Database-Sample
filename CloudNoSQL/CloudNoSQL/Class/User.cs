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
using Newtonsoft.Json;

namespace CloudNoSQL.Class
{
    public class Id
    {
        [JsonProperty (PropertyName ="$oid")]
        public string oid { get; set; }
    }
    public class User
    {
        public Id _id { get; set; }
        public string user { get; set; }
    }
}