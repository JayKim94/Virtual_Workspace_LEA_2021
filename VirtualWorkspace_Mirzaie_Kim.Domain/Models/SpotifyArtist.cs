using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    [DataContract]
    public class SpotifyArtist
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
