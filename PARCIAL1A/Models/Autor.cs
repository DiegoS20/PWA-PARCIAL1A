using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Autor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
