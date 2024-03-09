using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {


        private readonly Parcial1aContext _parcial1Adb;

        public AutoresController(Parcial1aContext parcial1Adb)
        {
            _parcial1Adb = parcial1Adb;
        }

        //lectura
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Autor> listadoAutor = (from e in _parcial1Adb.Autores select e).ToList();

            if (listadoAutor.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutor);
        }

        //Agregar
        [HttpPost]
        [Route("Add")]


        public IActionResult GuardarAutorL([FromBody] Autor autor)
        {
            try
            {
                _parcial1Adb.Autores.Add(autor);
                _parcial1Adb.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        //modificar
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult actualizarAutor(int id, [FromBody] Autor modificarAutor)
        {
            AutorLibro? AutorActual = (from e in _parcial1Adb.AutorLibros where e.AutorId == id select e).FirstOrDefault();

            if (AutorActual == null) { return NotFound(); }

            AutorActual.Orden = AutorActual.Orden;
            AutorActual.Orden = AutorActual.Orden;



            _parcial1Adb.Entry(AutorActual).State = EntityState.Modified;
            _parcial1Adb.SaveChanges();

            return Ok(modificarAutor);

        }

        //borrar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarAutor(int id)
        {

            Autor? eliminarAutor = (from e in _parcial1Adb.Autores where e.Id  == id select e).FirstOrDefault();

            if (eliminarAutor == null)
                return NotFound();

            _parcial1Adb.Autores.Attach(eliminarAutor);
            _parcial1Adb.Autores.Remove(eliminarAutor);
            _parcial1Adb.SaveChanges();

            return Ok(eliminarAutor);
        }
    }
}
