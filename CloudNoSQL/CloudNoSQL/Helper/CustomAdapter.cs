using System;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Runtime;
using Java.Lang;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;

using CloudNoSQL.Class;

namespace CloudNoSQL.Helper
{
    public class CustomAdapter : BaseAdapter
    {
        private Context mContext;
        private List<User> users;
        public CustomAdapter(Context mContext, List<User> users)
        {
            this.mContext = mContext;
            this.users = users;
        }
        public override int Count
        {
            get { return users.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            { return position; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row, null);
            TextView txtUser = view.FindViewById<TextView>(Resource.Id.txtUser);
            txtUser.Text = users[position].user;
            return view;
        }
    }
}