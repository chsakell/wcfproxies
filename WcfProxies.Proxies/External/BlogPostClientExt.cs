using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfProxies.Proxies.External
{
    public class BlogPostClientExt
    {
        readonly WcfProxies.Proxies.External.IBlogPostService _blogPostService;

        public BlogPostClientExt(IBlogPostService blogPostService)
        {
            this._blogPostService = blogPostService;
        }

        public PostData GetPost(int postId)
        {
            return _blogPostService.GetPost(postId);
        }

        public IEnumerable<PostData> GetBlogPosts(int blogId)
        {
            return _blogPostService.GetBlogPosts(blogId);
        }
    }
}
