using Bloggee.Data;
using Bloggee.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bloggee.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggeeDbContext bloggeeDbContext;

        public BlogPostRepository(BloggeeDbContext bloggeeDbContext)
        {
            this.bloggeeDbContext = bloggeeDbContext;
        }
        public async Task<BloggPost> AddAsync(BloggPost blogPost)
        {
            await bloggeeDbContext.AddAsync(blogPost);
            await bloggeeDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BloggPost> DeleteAsync(Guid id)
        {
            var existingBlogPost = await bloggeeDbContext.BlogPosts.FindAsync(id);

            if (existingBlogPost != null)
            {
                bloggeeDbContext.BlogPosts.Remove(existingBlogPost);
                await bloggeeDbContext.SaveChangesAsync();

                return existingBlogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BloggPost>> GetAllAsync()
        {
            //using the include method to get the list of collection of entities or related data found in the model(table).
            var listBlogs = await bloggeeDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
            return listBlogs;
        }

        public async Task<BloggPost?> GetAsync(Guid id)
        {
            var ID = await bloggeeDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
            return ID;
        }

        public async Task<BloggPost> UpdateAsync(BloggPost blogPost)
        {


            var existingBlog = await bloggeeDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDated = blogPost.PublishedDated;
                existingBlog.Tags = blogPost.Tags;

                //await bloggeeDbContext.BlogPosts.AddAsync(existingBlog);
                await bloggeeDbContext.SaveChangesAsync();
                return existingBlog;

            }

            return null;
       
        }
    }
}
