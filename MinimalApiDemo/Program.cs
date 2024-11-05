using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostsService, PostsService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// app.MapGet("/posts", () => list).WithName("GetPosts").WithOpenApi().WithTags("Posts");

app.MapGet("/posts", async (IPostsService postsService) => {
    var posts = await postsService.GetAllPosts();
    return posts;
}).WithName("GetPosts").WithOpenApi().WithTags("Posts");


// app.MapPost("/posts", (Post post) =>
// {
//     list.Add(post);
//     return Results.Created($"/posts/{post.Id}", post);
// }).WithName("CreatePost").WithOpenApi().WithTags("Posts");

app.MapPost("/posts", async (IPostsService postService, Post post) =>
{
    await postService.CreatePost(post);
    return Results.Created($"/posts/{post.Id}", post);
}).WithName("CreatePost").WithOpenApi().WithTags("Posts");


app.MapGet("/posts/{id}", async (IPostsService postService, int id) =>
{
    var post =  await postService.GetPost(id);
    return post == null ? Results.NotFound() : Results.Ok(post);
}
).WithName("GetPost").WithOpenApi().WithTags("Posts");

app.MapPut("/posts/{id}", async (IPostsService postService, int id, Post post) =>
{
    var updatedPost = await postService.UpdatePost(id, post);
    
    return Results.Ok(updatedPost);
}).WithName("UpdatePost").WithOpenApi().WithTags("Posts");




app.Run();
