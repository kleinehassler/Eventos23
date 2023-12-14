using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Grupo
{
    public int Idgrupo { get; set; }

    public string? Nombre { get; set; }

    public string? Comentario { get; set; }

    public int Idevento { get; set; }

    public string? Estado { get; set; }

    /*public virtual Evento IdeventoNavigation { get; set; } = null!;

    public virtual ICollection<Invitado> Invitados { get; set; } = new List<Invitado>();*/
}


public partial class GrupoDTO : Grupo
{
}