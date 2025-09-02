using System;
using System.Collections.Generic;

namespace MINTHOTEL.Models.Db;

public partial class Propietario
{
    public int IdPropietario { get; set; }

    public string? PrimerNom { get; set; }

    public string? SegundoNom { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public DateOnly? FechaNac { get; set; }

    public string? Sexo { get; set; }

    public string? Nacionalidad { get; set; }

    public string? TipoIdentificion { get; set; }

    public string? NumIdentificion { get; set; }

    public int? Telefono { get; set; }

    public string? Correo { get; set; } 

    public int? CodHotl { get; set; }

    public virtual Hotel? CodHotlNavigation { get; set; }
}
