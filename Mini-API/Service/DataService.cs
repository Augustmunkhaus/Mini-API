using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private ThreadPostContext db { get; }

    public DataService(ThreadPostContext db)
    {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {

        ThreadPost threadpost = db.ThreadPosts.FirstOrDefault();
        if (threadpost == null)
        {
            threadpost = new ThreadPost(new User("August"), "ThreadPost 1", "første thread", 5, 60);

            db.ThreadPosts.Add(threadpost);
            threadpost.Comments.Add(new Comment("Navn")
            {
                User = new User("Bob")
            });
        } db.SaveChanges();

    }

        // ThreadPosts
        public List<ThreadPost> GetThreadPosts()
        {
            return db.ThreadPosts.Include(x => x.Comments).ThenInclude(u => u.User).ToList();
        }

        public ThreadPost GetThreadPost(int id)
        {
            return db.ThreadPosts.Include(x => x.Comments).ThenInclude(u => u.User).FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public string CreateThreadPost(ThreadPost threadPost)
        {
            db.ThreadPosts.Add(threadPost);
            db.SaveChanges();

            return "Threadpost created, id: " + threadPost.Id;
        }

    public void Threadvotes(int id)

        //todo

    public string CreateComment(Comment comment)
    {
        db.Comments.Add(comment);
        db.SaveChanges();
        return "Comment created";
    }

        public List<Comment> GetComments()
        {
            return db.Comments.Include(t => t.User).ToList();
        } 
} 