using Microsoft.EntityFrameworkCore;

using Data;
using shared.Model;
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
        return db.ThreadPosts.Include(u => u.User).Include(x => x.Comments).ThenInclude(u => u.User).OrderBy(p => p.Date).ToList();
        
    }

    public ThreadPost GetThreadPost(int id)
    {
        return db.ThreadPosts.Include(u => u.User).Include(x => x.Comments).ThenInclude(u => u.User).FirstOrDefault(x => x.Id == id);
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
        threadPost.Date = DateTime.Now;
        db.ThreadPosts.Add(threadPost);
        db.SaveChanges();

        return "Threadpost created, id: " + threadPost.Id;
    }

    public void ThreadUpvotes(int id)
    {
        var thread = db.ThreadPosts.FirstOrDefault(x => x.Id == id);

        thread.Upvotes++;

        db.SaveChanges();


    }

    public void CommentUpvotes(int id)

    {
        var comment = db.Comments.FirstOrDefault(x => x.Id == id);

        comment.Upvotes++;
        db.SaveChanges();
    }
    //todo

    public void ThreadDownvotes(int id)

    //todo
    {
        var thread = db.ThreadPosts.FirstOrDefault(x => x.Id == id);

        thread.Upvotes--;

        db.SaveChanges();


    }

    public void CommentDownvotes(int id)

    //todo
    {
        var comment = db.Comments.FirstOrDefault(x => x.Id == id);

        comment.Upvotes--;
        db.SaveChanges();
    }

    public string CreateComment(Comment comment, int Postid)
    {
        comment.Date = DateTime.Now;
       var Post = db.ThreadPosts.Include(c => c.Comments).FirstOrDefault(x => x.Id == Postid);
        Post.Comments.Add(comment);
        db.SaveChanges();
        return "Comment created";
    }

        public List<Comment> GetComments()
        {
            return db.Comments.Include(t => t.User).OrderBy(c => c.Date).ToList();
    }

  
}