using Prism.Navigation;

namespace Learn.FloatingWidget.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel(INavigationService navigationService)
      : base(navigationService)
    {
      Title = "Main Page";
    }
  }
}
