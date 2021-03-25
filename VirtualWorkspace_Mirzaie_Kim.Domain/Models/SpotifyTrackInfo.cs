using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    [DataContract]
    public class SpotifyTrackInfo
    {
        [DataMember(Name = "item")]
        public SpotifyTrack Item { get; set; }

        [DataMember(Name = "progress_ms")]
        public int Progress { get; set; }

        [DataMember(Name = "is_playing")]
        public bool IsPlaying { get; set; }
    }
}
