using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class OpcionMenu
{
    public int IdopcionMenu { get; set; }

    public string? Nombre { get; set; }

    public string? Link { get; set; }

    public int? IdopcionMenuRef { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    */
}

public partial class OpcionMenuDTO:OpcionMenu
{
}