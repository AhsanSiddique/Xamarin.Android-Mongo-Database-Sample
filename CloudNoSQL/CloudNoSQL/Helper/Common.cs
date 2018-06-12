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
using CloudNoSQL.Class;

namespace CloudNoSQL.Helper
{
    class Common
    {
        private static String DB_NAME = "xamarindb";
        private static String COLLECTION_NAME = "user";
        private static String API_KEY = "ow6xCr4R6mh9Ac-WnOEYe31Uv-4-j5nh";

        public static string getAddressSingle(User user)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("/" + user._id.oid + "?apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

        public static string GetAddressAPI()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?apiKey=" + API_KEY);
            return strBuilder.ToString();
        }
    }
}