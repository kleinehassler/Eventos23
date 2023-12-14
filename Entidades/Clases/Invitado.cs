using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Invitado
{
    public int Idinvitado { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public string? Sexo { get; set; }

    public string? TipoInvitado { get; set; }

    public string? InvitacionEnviada { get; set; }

    public int Idevento { get; set; }

    public int Idgrupo { get; set; }

    public int Idmesa { get; set; }

    public int Idmenu { get; set; }

    public string? Comentario { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual Evento IdeventoNavigation { get; set; } = null!;

    public virtual Grupo IdgrupoNavigation { get; set; } = null!;

    public virtual Menu IdmenuNavigation { get; set; } = null!;

    public virtual Mesa IdmesaNavigation { get; set; } = null!;
    */
}


public partial class InvitadoDTO:Invitado
{

}