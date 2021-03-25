using System.Windows.Input;
using System.Windows.Media;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; private set; }

        public ICommand CloseApplicationCommand { get => new GeneralCommand(CloseApplication); }

        /// <summary>
        /// MainViewModel manages the navigation and the content-control.
        /// </summary>
        /// <param name="navigator">Updates current selected view model.</param>
        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }

        private void CloseApplication(object parameter)
        {
            App.Current.Shutdown();
        }

        public override void RefreshView()
        {
            throw new System.NotImplementedException();
        }
    }
}
