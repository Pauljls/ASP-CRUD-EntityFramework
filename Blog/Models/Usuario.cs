using System;
using System.Collections.Generic;

namespace Blog.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int? Edad { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
