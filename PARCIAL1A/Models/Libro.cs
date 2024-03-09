using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public virtual ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();
}
