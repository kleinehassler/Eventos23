using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Evento
{
    public int Idevento { get; set; }

    public string? Nombre { get; set; }

    public int IdtipoEvento { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Presupuesto { get; set; }

    public string? Comentario { get; set; }

    public string? Estado { get; set; }

    public int? Idusuario { get; set; }

    /*public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual TipoEvento IdtipoEventoNavigation { get; set; } = null!;

    public virtual ICollection<Invitado> Invitados { get; set; } = new List<Invitado>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();*/
}


public partial class EventoDTO:Evento
{
    public string? TipoEvento { get; set; }
    public string? FechaFormato { get; set; }
    public string? HoraFormato { get; set; }

    public List<InvitadoDTO>? invitados { get; set; }
    public List<GrupoDTO>? grupos { get; set; }
    public List<MesaDTO>? mesas { get; set; }
    public List<MenuDTO>? menus { get; set; }

}