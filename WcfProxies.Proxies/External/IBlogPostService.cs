using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfProxies.Proxies.External
{
    [ServiceContract]
    public interface IBlogPostService
    {
        [OperationContract]
        PostData GetPost(int postId);

        [OperationContract]
        IEnumerable<PostData> GetBlogPosts(int blogId);
    }

    [DataContract]
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
