using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Learn.FloatingWidget.Droid.Services
{
  [Service]
  public class FloatingWidgetService : Service, View.IOnTouchListener
  {
    private IWindowManager _windowManager;
    private WindowManagerLayoutParams _layoutParams;
    private View _floatingView;

    private int _initialX;
    private int _initialY;
    private float _initialTouchX;
    private float _initialTouchY;

    public override IBinder OnBind(Intent intent)
    {
      return null;
    }

    public override void OnCreate()
    {
      base.OnCreate();
      _floatingView = LayoutInflater.From(this).Inflate(Resource.Layout.LayoutFloatingWidget, null);

      SetTouchListener();

      _layoutParams = new WindowManagerLayoutParams(
        ViewGroup.LayoutParams.WrapContent,
        ViewGroup.LayoutParams.WrapContent,
        WindowManagerTypes.Phone,
        WindowManagerFlags.NotFocusable,
        Format.Translucent)
      {
        Gravity = GravityFlags.Left | GravityFlags.Top
      };

      _windowManager = GetSystemService(WindowService).JavaCast<IWindowManager>();
      _windowManager.AddView(_floatingView, _layoutParams);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      if (_floatingView != null)
      {
        _windowManager.RemoveView(_floatingView);
      }
    }

    public bool OnTouch(View view, MotionEvent motionEvent)
    {
      bool result = false;

      switch (motionEvent.Action)
      {
        case MotionEventActions.Down:

          // Initial Position on press
          _initialX = _layoutParams.X;
          _initialY = _layoutParams.Y;

          // Touch Position
          _initialTouchX = motionEvent.RawX;
          _initialTouchY = motionEvent.RawY;

          result = true;
          break;

        case MotionEventActions.Move:

          // Calculate the X & Y coordinates of the View
          _layoutParams.X = _initialX + (int)(motionEvent.RawX - _initialTouchX);
          _layoutParams.Y = _initialY + (int)(motionEvent.RawY - _initialTouchY);

          // Move the view on the screen
          _windowManager.UpdateViewLayout(_floatingView, _layoutParams);

          result = true;
          break;
      }

      return result;
    }

    private void SetTouchListener()
    {
      var container = _floatingView.FindViewById<RelativeLayout>(Resource.Id.root);
      container.SetOnTouchListener(this);
    }
  }
}
