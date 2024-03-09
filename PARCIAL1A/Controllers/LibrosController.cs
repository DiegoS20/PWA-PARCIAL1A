using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly Parcial1aContext _context;

        public LibrosController(Parcial1aContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetLibros()
        {
            return _context.Libros.ToList();
        }

        [HttpGet("Autor/{autor}")]
        public ActionResult<IEnumerable<Libro>> GetLibrosByAutor(string autor)
        {
            return (from autoreslibros in _context.AutorLibros
                    join libros in _context.Libros on autoreslibros.LibroId equals libros.Id
                    join autores in _context.Autores on autoreslibros.AutorId equals autores.Id
                    where autores.Nombre.Contains(autor)
                    select libros).ToList();
        }

        [HttpPut("{id}")]
        public IActionResult PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            if (!LibroExists(id))
            {
                return NotFound();
            }

            try
            {
                _context.Entry(libro).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Libro> PostLibro(Libro libro)
        {
            if (LibroExists(libro.Id))
            {
                return Conflict();
            }

            try
            {
                _context.Libros.Add(libro);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLibro(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
