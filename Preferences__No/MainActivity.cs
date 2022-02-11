using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Xamarin.Essentials;
using System;

namespace Preferences__No
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button savedetails,Clear;
        EditText takeinput;
        Switch streamSelection;
        SeekBar volumeSeekbar;
        private const string Name = "name";
        private const string StreamSelection = "streamselection";
        private const string Volume = "volume";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            savedetails = FindViewById<Button>(Resource.Id.savebtn);
            takeinput = FindViewById<EditText>(Resource.Id.takename);
            streamSelection = FindViewById<Switch>(Resource.Id.switchtext);
            volumeSeekbar = FindViewById<SeekBar>(Resource.Id.seekba);
            Clear = FindViewById<Button>(Resource.Id.clean);
            savedetails.Click += Savedetails_Click;
            Clear.Click += Clear_Click;
            ShowUserPreferenceIfAlreadyExist();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
           Preferences.Remove(Name);
        }

        private void ShowUserPreferenceIfAlreadyExist()
        {
            if (Preferences.ContainsKey(Name))
            {
                takeinput.Text = Preferences.Get(Name, String.Empty);
            }
            if (Preferences.ContainsKey(StreamSelection))
            {
                streamSelection.Checked = Preferences.Get(StreamSelection,false);
            }
            int volume = Preferences.Get(Volume, 0);
            volumeSeekbar.SetProgress(volume, true);
        }

        private void Savedetails_Click(object sender, System.EventArgs e)
        {
            string name = takeinput.Text;
            bool shouldstreamonwifi = streamSelection.Checked;
            int vol = volumeSeekbar.Progress;
            Preferences.Set(Name, name);
            Preferences.Set(StreamSelection, shouldstreamonwifi);
            Preferences.Set(Volume, vol);
            Toast.MakeText(this, "Users preferences saved successfully", ToastLength.Short).Show();
        }

      
    }
}