using System;
using System.Collections.Generic;

namespace MINTHOTEL.Models.Db;

public partial class Hotel
{
    public int IdHotel { get; set; }

    public string? NomHotel { get; set; }

    public string? Departamento { get; set; }

    public string? Municipio { get; set; }

    public string? Direccion { get; set; }

    public string? CodRuc { get; set; }

    public int? Telefono { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Propietario> Propietarios { get; set; } = new List<Propietario>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Visitum> Visita { get; set; } = new List<Visitum>();
}
