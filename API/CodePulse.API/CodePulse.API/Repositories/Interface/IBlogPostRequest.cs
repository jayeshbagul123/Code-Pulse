using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRequest
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
