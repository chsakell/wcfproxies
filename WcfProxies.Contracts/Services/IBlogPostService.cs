using System.Collections.Generic;
using System.ServiceModel;
using WcfProxies.Contracts.Data;

namespace WcfProxies.Contracts.Services
{
    [ServiceContract]//(Namespace = "http://www.chsakell.com/wcfproxies")]
    public interface IBlogPostService
    {
        [OperationContract]
        PostData GetPost(int postId);

        [OperationContract]
        IEnumerable<PostData> GetBlogPosts(int blogId);
    }
}
