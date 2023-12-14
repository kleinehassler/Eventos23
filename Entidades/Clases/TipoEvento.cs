using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class TipoEvento
{
    public int? IdtipoEvento { get; set; }

    public string? Nombre { get; set; }

    public string? Comentario { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    */
}


public partial class TipoEventoDTO:TipoEvento
{

}