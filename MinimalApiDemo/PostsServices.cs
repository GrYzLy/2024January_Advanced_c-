


public class PostsService : IPostsService
{
    private static readonly List<Post> AllPost = new();

    

    public Task CreatePost(Post item) {
        AllPost.Add(item);
        return Task.CompletedTask;
    }
    

    public Task<Post?> UpdatePost(int id, Post item) {
     var post = AllPost.FirstOrDefault(x => x.Id == id);
     if( post != null)
     {
        post.Title = item.Title;
        post.Body = item.Body;
        post.UserId = item.UserId;
     }

     return Task.FromResult(post);
    }

    public Task<Post?> GetPost(int id) {
        return Task.FromResult(AllPost.FirstOrDefault(x => x.Id == id));
    }

    public Task<List<Post>>GetAllPosts() {
        return Task.FromResult(AllPost);
    }

    public Task DeletePost(int id) {
         var post = AllPost.FirstOrDefault(x => x.Id == id);
        if( post != null)
        {
            AllPost.Remove(post);
        }

        return Task.CompletedTask;
    }

}