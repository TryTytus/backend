using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();
            posts.ForEach(post => post.User.Posts.Clear());
            return posts;
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            
            
            // var post = await _context.Posts
            //     .Where(p => p.Id == id)
            //     .Include(p => p.User)
            //     .FirstOrDefaultAsync();
            
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
                
            if (post == null)
            {
                return NotFound();
            }
            
            post.User.Posts.Clear();
            
         

            // await _context.Entry(post).Reference(p => p.User).LoadAsync();

            return post;
        }

        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostModel postModel)
        {
            
            ClaimsPrincipal currentUser = this.User;
            string? currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.Where(u => u.Email == currentUserId).FirstOrDefaultAsync();
            
            if (user == null)
                return Unauthorized();
            
            var post = await _context.Posts.FindAsync(id);
  
            if (post == null)
                return NotFound();
            
            if (post.UserId!= user.Id)
                return Unauthorized();
            
            
            post.Content = postModel.Content;
            post.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(PostModel postModel)
        {
            ClaimsPrincipal currentUser = this.User;
            string? currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
                return Unauthorized();

            var user = await _context.Users.Where(u => u.Email == currentUserId).FirstOrDefaultAsync();
            
            if (user == null)
                return Unauthorized();

            var post = new Post
            {
                Content = postModel.Content,
                UserId = user.Id,
                UpdatedAt = DateTime.Now,
            };
            
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, new { id = post.Id });
        }

        // DELETE: api/Post/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            
            ClaimsPrincipal currentUser = this.User;
            string? currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.Where(u => u.Email == currentUserId).FirstOrDefaultAsync();
            
            if (user == null)
                return Unauthorized();
            
            var post = await _context.Posts.FindAsync(id);
  
            if (post == null)
                return NotFound();
            
            if (post.UserId!= user.Id)
                return Unauthorized();
            

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}


public class PostModel
{
    public string Content { get; set; }
}

public class PostRespone
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public int LikesCount { get; set; }

    public int ViewsCount { get; set; }

    public int CommentsCount { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public UserModel User { get; set; }
}

public class UserModel
{
    public string Name { get; set; }
    public string Nickname { get; set; }
} 

