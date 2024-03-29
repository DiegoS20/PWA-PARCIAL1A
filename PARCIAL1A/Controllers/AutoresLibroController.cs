﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresLibroController : ControllerBase
    {

        private readonly Parcial1aContext _parcial1Adb;

        public AutoresLibroController(Parcial1aContext parcial1Adb)
        {
            _parcial1Adb = parcial1Adb;
        }

        //lectura
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<AutorLibro> listadoAutorL = (from e in _parcial1Adb.AutorLibros select e).ToList();

            if (listadoAutorL.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutorL);
        }

        //Agregar
        [HttpPost]
        [Route("Add")]


        public IActionResult GuardarAutorL([FromBody] AutorLibro autorLibro)
        {
            try
            {
                _parcial1Adb.AutorLibros.Add(autorLibro);
                _parcial1Adb.SaveChanges();
                return Ok(autorLibro);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        //modificar
        [HttpPut]
        [Route("Actualizar/{authorId}/{libroId}")]

        public IActionResult actualizarAutorL(int authorId, int libroId, [FromBody] AutorLibro modificarAutorL)
        {
            bool AutorlActual = _parcial1Adb.AutorLibros.Any(al => al.AutorId == authorId && al.LibroId == libroId);

            if (!AutorlActual) { return NotFound(); }

            _parcial1Adb.Entry(modificarAutorL).State = EntityState.Modified;
            _parcial1Adb.SaveChanges();

            return Ok(modificarAutorL);

        }

        //borrar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarAutor(int id)
        {

            AutorLibro? eliminarAutor = (from e in _parcial1Adb.AutorLibros where e.AutorId == id select e).FirstOrDefault();

            if (eliminarAutor == null)
                return NotFound();

            _parcial1Adb.AutorLibros.Attach(eliminarAutor);
            _parcial1Adb.AutorLibros.Remove(eliminarAutor);
            _parcial1Adb.SaveChanges();

            return Ok(eliminarAutor);
        }

    }
}
