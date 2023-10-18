using Bloggee.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bloggee.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BloggPost>> GetAllAsync();

        Task<BloggPost?> GetAsync(Guid id);

        Task<BloggPost> AddAsync(BloggPost blogPost);

        Task<BloggPost?> UpdateAsync(BloggPost blogPost);

        Task<BloggPost?> DeleteAsync(Guid id);






        
    }
}
