using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Mesa
{
    public int Idmesa { get; set; }

    public string? Nombre { get; set; }

    public string? Comentario { get; set; }

    public string? TipoMesa { get; set; }

    public int? NumeroInvitados { get; set; }

    public string? TipoInvitado { get; set; }

    public int Idevento { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual Evento IdeventoNavigation { get; set; } = null!;

    public virtual ICollection<Invitado> Invitados { get; set; } = new List<Invitado>();
    */
}

public partial class MesaDTO:Mesa
{
}