using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using CloudNoSQL.Class;
using System.Collections.Generic;
using static Android.Widget.AdapterView;
using Android.Views;
using Java.Lang;
using Newtonsoft.Json;
using CloudNoSQL.Helper;

namespace CloudNoSQL
{
    [Activity(Label = "CloudNoSQL", MainLauncher = true, Theme ="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity , IOnItemClickListener
    {
        private ListView lstView;
        private Button btnAdd, btnEdit, btnDelete;
        private EditText edtUser;
        private User SelectedUser = null;
        private List<User> users;

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            SelectedUser = users[position];
            edtUser.Text = SelectedUser.user; //Set Username to Edit Text
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            lstView = FindViewById<ListView>(Resource.Id.lstView);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            edtUser = FindViewById<EditText>(Resource.Id.edtUser);
            
            //Add Event to Control
            lstView.OnItemClickListener = this;

            btnAdd.Click += delegate
            {
                new PostData(edtUser.Text, this).Execute(Common.GetAddressAPI());
            };

            btnEdit.Click += delegate 
            {
                new PutData(SelectedUser,edtUser.Text, this).Execute(Common.getAddressSingle(SelectedUser));
            };

            btnDelete.Click += delegate
            {
                new DeleteData(SelectedUser, edtUser.Text, this).Execute(Common.getAddressSingle(SelectedUser));
            };

            //Load Data when opened app
            new GetData(this).Execute(Common.GetAddressAPI());
        }

        //User Inner Class for process data
        private class GetData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private MainActivity activity;

            public GetData(MainActivity activity)
            {
                this.activity = activity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlString = @params[0];
                HttpDataHandler http = new HttpDataHandler();
                stream = http.GetHTTPData(urlString);
                return stream;
            }

            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                activity.users = JsonConvert.DeserializeObject<List<User>>(result);
                CustomAdapter adapter = new CustomAdapter(Application.Context, activity.users);
                activity.lstView.Adapter = adapter;
                pd.Dismiss();
            }
        }

        private class PostData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            string userName = "";
            private MainActivity mainActivity;


            public PostData(string userName, MainActivity mainActivity)
            {
                this.userName = userName;
                this.mainActivity = mainActivity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string urlString = @params[0];
                HttpDataHandler http = new HttpDataHandler();
                User user = new User();
                user.user = userName;
                string json = JsonConvert.SerializeObject(user);
                http.PostHTTPData(urlString, json);
                return string.Empty;
            }

            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                new GetData(mainActivity).Execute(Common.GetAddressAPI());
                pd.Dismiss();
            }
        }

        private class PutData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            string newUser = "";
            private MainActivity mainActivity;
            User selectedUser = null;


            public PutData(User selectedUser,string newUser, MainActivity mainActivity)
            {
                this.newUser = newUser;
                this.mainActivity = mainActivity;
                this.selectedUser = selectedUser;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string urlString = @params[0];
                HttpDataHandler http = new HttpDataHandler();
                selectedUser.user = newUser;
                string json = JsonConvert.SerializeObject(selectedUser);
                http.PutHTTPData(urlString, json);
                return string.Empty;
            }

            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                new GetData(mainActivity).Execute(Common.GetAddressAPI());
                pd.Dismiss();
            }
        }

        private class DeleteData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            string newUser = "";
            private MainActivity mainActivity;
            User selectedUser = null;


            public DeleteData(User selectedUser, string newUser, MainActivity mainActivity)
            {
                this.newUser = newUser;
                this.mainActivity = mainActivity;
                this.selectedUser = selectedUser;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string urlString = @params[0];
                HttpDataHandler http = new HttpDataHandler();
                selectedUser.user = newUser;
                string json = JsonConvert.SerializeObject(selectedUser);
                http.DeleteHTTPData(urlString, json);
                return string.Empty;
            }

            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                new GetData(mainActivity).Execute(Common.GetAddressAPI());
                pd.Dismiss();
            }
        }

    }
}

