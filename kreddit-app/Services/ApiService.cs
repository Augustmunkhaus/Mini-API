using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using shared.Model;

namespace kreddit_app.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public async Task<ThreadPost[]> GetPosts()
    {
        string url = $"{baseAPI}api/threadposts";
        return await http.GetFromJsonAsync<ThreadPost[]>(url);
    }

    public async Task<ThreadPost> GetPost(int id)
    {
        string url = $"{baseAPI}api/threadposts/{id}";
        return await http.GetFromJsonAsync<ThreadPost>(url);
    }

    public async Task<User> GetUser(int id)
    {
        string url = $"{baseAPI}api/threadposts/users/{id}";
        return await http.GetFromJsonAsync<User>(url);
    }

    public async Task<Comment> GetComment()
    {
        string url = $"{baseAPI}api/threadposts/comments";
        return await http.GetFromJsonAsync<Comment>(url);
    }

    public async Task<ThreadPost> CreateThreadPost(string content, string title, User user)
    {
        string url = $"{baseAPI}api/threadposts";
        return await http.GetFromJsonAsync<ThreadPost>(url);
    }

    public async Task<Comment> CreateComment(int postId, Comment comment )
    {
        string url = $"{baseAPI}api/comments/{postId}";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, comment);

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return newComment;
    }

    public async Task UpvotePost(int id)
    {
        string url = $"{baseAPI}api/threadposts/{id}/like";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

       
    }
    public async Task DownvotePost(int id)
    {
        string url = $"{baseAPI}api/threadposts/{id}/dislike";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        
    }


    public async Task UpvoteComment(int id)
    {
        string url = $"{baseAPI}api/comments/{id}/like";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        
    }

    public async Task DownvoteComment(int id)
    {
        string url = $"{baseAPI}api/comments/{id}/dislike";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        
    }


}





