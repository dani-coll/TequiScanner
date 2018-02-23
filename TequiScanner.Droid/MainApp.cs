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
using Autofac;
using TequiScanner.Shared.Services;

namespace TequiScanner.Droid
{
    [Application]
    public class MainApp : Application
    {
        public static IContainer Builder;

        public MainApp(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            // register ioc services
            Builder = new ContainerBuilder().Build(); 
        }
    }
}