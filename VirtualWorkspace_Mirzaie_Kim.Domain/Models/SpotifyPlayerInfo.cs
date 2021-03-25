using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    [DataContract]
    public class SpotifyPlayerInfo
    {
        [DataMember(Name = "device")]
        public SpotifyDeviceInfo Device { get; set; }

        [DataMember(Name = "is_playing")]
        public bool IsPlaying { get; set; }

        [DataMember(Name = "shuffle_state")]
        public bool ShuffleState { get; set; }

        [DataMember(Name = "repeat_state")]
        public string RepeatState { get; set; }
    }
}
