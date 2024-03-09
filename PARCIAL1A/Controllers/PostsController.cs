using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Parcial1aContext _context;

        public PostsController(Parcial1aContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            return _context.Posts.ToList();
        }

        [HttpGet("Autor/{autor}")]
        public ActionResult<IEnumerable<Post>> GetPostByAuthor(string autor)
        {
            if (!AutorExists(autor))
            {
                return BadRequest("Este autor no existe");
            }

            int autorId = _context.Autores.Where(a => a.Nombre.Contains(autor)).ToList()[0].Id;
            return _context.Posts.Where(p => p.AutorId == autorId).OrderByDescending(p => p.Id).Take(20).ToList();
        }

        [HttpGet("Libro/{libro}")]
        public ActionResult<IEnumerable<Post>> GetPostByBook(string libro)
        {
            if (!BookExists(libro))
            {
                return BadRequest("Este libro no existe");
            }

            return (from autoreslibro in _context.AutorLibros
                    join libros in _context.Libros on autoreslibro.LibroId equals libros.Id
                    join posts in _context.Posts on autoreslibro.AutorId equals posts.AutorId
                    where libros.Titulo.Contains(libro)
                    select posts).ToList();
        }

        [HttpPut("{id}")]
        public IActionResult PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            if (!PostExists(id))
            {
                return NotFound();
            }

            try
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Post> PostPost(Post post)
        {
            if (PostExists(post.Id))
            {
                return Conflict();
            }
            
            try
            {
                _context.Posts.Add(post);
                _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private bool AutorExists(string name)
        {
            return _context.Autores.Any(e => e.Nombre.Contains(name));
        }

        private bool BookExists(string title)
        {
            return _context.Libros.Any(l => l.Titulo.Contains(title));
        }
    }
}
