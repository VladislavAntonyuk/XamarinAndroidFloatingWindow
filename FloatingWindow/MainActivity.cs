// <copyright file="MainActivity.cs" company="Vladislav Antonyuk">
// Copyright (c). All rights reserved.
// </copyright>
// <author>Vladislav Antonyuk</author>

namespace FloatingWindow
{
    using Android.App;
    using Android.OS;
    using Android.Widget;

    /// <summary>
    /// Main Activity
    /// </summary>
    [Activity(Label = "FloatingWindow", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Called by the system when the activity is first created.
        /// </summary>
        /// <param name="bundle">The bundle</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Main);
            Button b = this.FindViewById<Button>(Resource.Id.btn);
            b.Click += (sender, e) =>
            {
                StartService(new Android.Content.Intent(this, typeof(FloatingWindowService)));
                OnBackPressed();
            };
        }
    }
}