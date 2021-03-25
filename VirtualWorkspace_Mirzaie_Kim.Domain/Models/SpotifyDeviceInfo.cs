using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    [DataContract]
    public class SpotifyDeviceInfo
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "volume_percent")]
        public string VolumePercent { get; set; }
    }
}
