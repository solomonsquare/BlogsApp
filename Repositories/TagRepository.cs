using Bloggee.Data;
using Bloggee.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bloggee.Repositories
{
    //implementation of the the ITagInterface Repository
    public class TagRepository : ITagRepository
    {
        private readonly BloggeeDbContext bloggeeDbContext;

        public TagRepository(BloggeeDbContext bloggeeDbContext)
        {
            this.bloggeeDbContext = bloggeeDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggeeDbContext.Tags.AddAsync(tag);
            await bloggeeDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var existingTag = await bloggeeDbContext.Tags.FindAsync(id);

            if (existingTag != null) 
            { 
                bloggeeDbContext.Tags.Remove(existingTag);
                await bloggeeDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggeeDbContext.Tags.ToListAsync();
            
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            return await bloggeeDbContext.Tags.FindAsync(id);
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggeeDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggeeDbContext.SaveChangesAsync();

                return existingTag;

            }
            return null;
        }
    }
}
