using System;
using System.Collections.Generic;

namespace MINTHOTEL.Models.Db;

public partial class Visitum
{
    public int IdVisita { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public DateOnly? FechaNac { get; set; }

    public string? Sexo { get; set; }

    public string? Nacionalidad { get; set; }

    public string? TipoIdentificion { get; set; }

    public string? NumIdentificion { get; set; }

    public string? NumHabitacion { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaSalida { get; set; }

    public int? CodHotel { get; set; }

    public virtual Hotel? CodHotelNavigation { get; set; }
}
