using Bloggee.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggee.Data
{
    public class BloggeeDbContext : DbContext
    {

        public DbSet<BloggPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public BloggeeDbContext(DbContextOptions options) : base(options)
        {

        }



    }
}
