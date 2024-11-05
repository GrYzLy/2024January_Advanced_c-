using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyApp.Namespace
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postService;

        public PostsController(IPostsService postsService)
        {
            _postService = postsService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            if(post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await _postService.CreatePost(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost (int id, Post post)
        {
            if(id != post.Id)
            {
                return BadRequest();
            }

            var updatePost = await _postService.UpdatePost(id, post);

            if(updatePost == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }
    }
}
