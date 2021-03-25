using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Services
{
    public interface ISpotifyControllerService
    {
        Task<OAuthToken> Authentificate();
        SpotifyPlayerInfo GetPlayerInfo();
        SpotifyTrackInfo GetCurrentTrackInfo();

        Task<SpotifyPlayerInfo> Pause();
        Task<SpotifyPlayerInfo> Play();
        void NextTrack();
        void PreviousTrack();
    }
}
