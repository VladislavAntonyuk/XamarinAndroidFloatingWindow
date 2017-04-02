// <copyright file="FloatingWindowService.cs" company="Vladislav Antonyuk">
// Copyright (c). All rights reserved.
// </copyright>
// <author>Vladislav Antonyuk</author>

namespace FloatingWindow
{
    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;

    /// <summary>
    /// Floating Window Service
    /// </summary>
    [Service]
    public class FloatingWindowService : Service
    {
        /// <summary>
        /// // This is a started service, not a bound service, so I just return null.
        /// </summary>
        /// <param name="intent">The Intent that was used to bind to this service</param>
        /// <returns>Return the communication channel to the service.</returns>
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        /// <summary>
        /// Called by the system when the service is first created.
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            IWindowManager wm = GetSystemService(WindowService).JavaCast<IWindowManager>();
            LinearLayout ll = new LinearLayout(this);
            ll.SetBackgroundColor(Color.Red);
            LinearLayout.LayoutParams layoutParameteres = new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    400);
            ll.SetBackgroundColor(Color.Argb(90, 0, 0, 0));
            ll.LayoutParameters = layoutParameteres;
            WindowManagerLayoutParams parameters = new WindowManagerLayoutParams(
                    500,
                    200,
                    0,
                    0,
                    WindowManagerTypes.Phone,
                    WindowManagerFlags.NotFocusable,
                    Format.Translucent);
            parameters.Gravity = GravityFlags.Center | GravityFlags.Center;
            Button stop = new Button(this);
            stop.Text = "Stop";
            stop.Click += (sender, e) =>
            {
                wm.RemoveView(ll);
                StopSelf();
            };
            ViewGroup.LayoutParams btnParameters = new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            stop.LayoutParameters = btnParameters;
            ll.AddView(stop);
            wm.AddView(ll, parameters);
            double x = 0;
            double y = 0;
            double pressedX = 0;
            double pressedY = 0;
            ll.Touch += (s, e) =>
            {
                WindowManagerLayoutParams updatedParameters = parameters;
                switch (e.Event.Action)
                {
                    case MotionEventActions.Down:
                        x = updatedParameters.X;
                        y = updatedParameters.Y;
                        pressedX = e.Event.RawX;
                        pressedY = e.Event.RawY;
                        break;
                    case MotionEventActions.Move:
                        updatedParameters.X = (int)(x + (e.Event.RawX - pressedX));
                        updatedParameters.Y = (int)(y + (e.Event.RawY - pressedY));
                        wm.UpdateViewLayout(ll, updatedParameters);
                        break;
                    default:
                        break;
                }
            };
        }

        /// <summary>
        /// Called by the system to notify a Service that it is no longer used and is being removed.
        /// </summary>
        public override void OnDestroy()
        {
            this.OnDestroy();
            this.StopSelf();
        }
    }
}