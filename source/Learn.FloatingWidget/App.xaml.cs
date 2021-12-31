using Learn.FloatingWidget.ViewModels;
using Learn.FloatingWidget.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Learn.FloatingWidget
{
  public partial class App
  {
    public App(IPlatformInitializer initializer)
      : base(initializer)
    {
    }

    protected override async void OnInitialized()
    {
      InitializeComponent();

      await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

      containerRegistry.RegisterForNavigation<NavigationPage>();
      containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
      containerRegistry.RegisterForNavigation<ConversationView, ConversationViewModel>();
    }
  }
}
