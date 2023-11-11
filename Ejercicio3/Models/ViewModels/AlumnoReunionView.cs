namespace Ejercicio3.Models.ViewModels;

public class AlumnoReunionView
{
    // Esta clase es el modelo para mostrar la unión de las tablas persona, alumno, alumno_reunion y reunion.
    public int id_persona { get; set; }

    public string? nombre_uno { get; set; }

    public string? nombre_dos { get; set; }

    public string? apellido_uno { get; set; }

    public string? apellido_dos { get; set; }

    public DateTime? D_nacimiento { get; set; }

    public string? matricula { get; set; }

    public string? carrera { get; set; }

    public string? semestre { get; set; }

    public string? especialidad { get; set; }

    public int id_reunion { get; set; }

    public DateTime? fecha { get; set; }

    public TimeSpan? hora { get; set; }

    public string? lugar { get; set; }

    public string? tema { get; set; }
}