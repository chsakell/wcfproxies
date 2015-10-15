using System.Collections.Generic;
using System.ServiceModel;
using WcfProxies.Contracts.Data;
using WcfProxies.Contracts.Services;
using WcfProxies.Data.Entities;
using WcfProxies.Data.Repositories;

namespace WcfProxies.Services
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BlogPostController : IBlogPostService
    {
        IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }

        public PostData GetPost(int postId)
        {
            PostData _post = null;
            Post _postEntity = _blogPostRepository.GetPost(postId);

            if (_postEntity != null)
            {
                Blog _blog = _blogPostRepository.GetBlog(_postEntity.BlogId);
                _post = new PostData()
                {
                    Title = _postEntity.Title,
                    Author = _postEntity.Author,
                    URI = _postEntity.URI,
                    Blog = _blog.Name
                };
            }

            return _post;
        }

        public IEnumerable<PostData> GetBlogPosts(int blogId)
        {
            List<PostData> _posts = null;
            List<Post> _postEntities = _blogPostRepository.GetBlogPosts(blogId);

            if (_postEntities != null)
            {
                _posts = new List<PostData>();
                foreach (var post in _postEntities)
                {
                    Blog _blog = _blogPostRepository.GetBlog(post.BlogId);
                    _posts.Add(new PostData()
                    {
                        Title = post.Title,
                        Author = post.Author,
                        URI = post.URI,
                        Blog = _blog.Name
                    });
                }
            }

            return _posts;
        }
    }
}
