using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRequest
    {
        private readonly ApplicationDBContext dBContext;
        public BlogPostRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;      
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dBContext.BlogPosts.AddAsync(blogPost);
            await dBContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
