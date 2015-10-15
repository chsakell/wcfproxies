using System.Runtime.Serialization;

namespace WcfProxies.Contracts.Data
{
    [DataContract]//(Namespace = "http://www.chsakell.com/wcfproxies")]
    public class PostData
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string URI { get; set; }
        [DataMember]
        public string Blog { get; set; }
    }
}
