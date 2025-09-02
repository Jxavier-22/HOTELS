using System;
using System.Collections.Generic;

namespace MINTHOTEL.Models.Db;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? NomRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
