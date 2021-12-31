using System;
using Android.App;
using Android.Runtime;
using Learn.FloatingWidget.Droid.Services;

namespace Learn.FloatingWidget.Droid
{
  [Application(Theme = "@style/MainTheme")]
  public class MainApplication : Application
  {
    public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
      base.OnCreate();
      Xamarin.Essentials.Platform.Init(this);

      SetContentView(Resource.Layout.Main);

      StartService(new Android.Content.Intent(this, typeof(FloatingWidgetService)));
    }
  }
}
