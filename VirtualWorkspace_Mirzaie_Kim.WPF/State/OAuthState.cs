using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.State
{
    public class OAuthState : ObservableObject
    {
        private OAuthToken _token;

        public OAuthToken Token
        {
            get { return _token; }
            set 
            { 
                _token = value;
                OnPropertyChanged(nameof(Token));
            }
        }

        public bool IsSigned => Token != null && Token.ExpirationDate > DateTime.Now;
        public bool IsNotSigned => !IsSigned;
    }
}
