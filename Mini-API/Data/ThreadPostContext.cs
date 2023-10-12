using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ThreadPostContext : DbContext

    {
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<User> Users => Set<User>();
        public DbSet<ThreadPost> ThreadPosts => Set<ThreadPost>();


        public ThreadPostContext(DbContextOptions<ThreadPostContext> options)
            : base(options)
        {

        }

    }
}