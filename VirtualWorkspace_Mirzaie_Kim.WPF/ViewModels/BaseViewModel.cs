using VirtualWorkspace_Mirzaie_Kim.WPF.Models;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        // Base class for all view models.       
        public abstract void RefreshView();
    }
}
