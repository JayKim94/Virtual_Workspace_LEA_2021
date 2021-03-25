using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.State
{
    public class SpotifyAuthState : ObservableObject
    {
        private OAuthToken _token;

        public OAuthToken Token
        {
            get { return _token; }
            set 
            { 
                _token = value;
                OnPropertyChanged(nameof(Token));
                OnPropertyChanged(nameof(IsSigned));
                OnPropertyChanged(nameof(IsNotSigned));
            }
        }

        private SpotifyPlayerInfo _playerInfo;

        public SpotifyPlayerInfo PlayerInfo
        {
            get { return _playerInfo; }
            set 
            { 
                _playerInfo = value;
                OnPropertyChanged(nameof(PlayerInfo));
            }
        }

        private SpotifyTrackInfo _trackInfo;

        public SpotifyTrackInfo TrackInfo
        {
            get { return _trackInfo; }
            set 
            { 
                _trackInfo = value;
                OnPropertyChanged(nameof(TrackInfo));
            }
        }

        public bool IsSigned => Token != null && Token.ExpirationDate > DateTime.Now;
        public bool IsNotSigned => !IsSigned;
    }
}
