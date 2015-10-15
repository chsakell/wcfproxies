using System.Collections.Generic;
using WcfProxies.Data.Entities;

namespace WcfProxies.Data.Repositories
{
    public interface IBlogPostRepository
    {
        Post GetPost(int postId);
        Blog GetBlog(int blogId);
        List<Post> GetBlogPosts(int blogId);
    }
}
