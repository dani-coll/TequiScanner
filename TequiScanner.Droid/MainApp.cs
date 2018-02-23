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
using TequiScanner.Shared.Services.Intefaces;

namespace TequiScanner.Droid
{
    [Application]
    public class MainApp : Application
    {
        public static IContainer Container { get; set; }

        public MainApp(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            Initialize();

            base.OnCreate();
        }

        private static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new ComputerVisionService()).As<IComputerVisionService>();
            MainApp.Container = builder.Build();
        }
    }
}