namespace Ejercicio3.Models;

public partial class Reunion
{
    // Esta clase es el modelo creado por Scaffold-DbContext de la tabla reunion.
    
    public int IdReunion { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? Lugar { get; set; }

    public string? Tema { get; set; }

    public virtual ICollection<Alumno> IdAlumnos { get; set; } = new List<Alumno>();

    public virtual ICollection<Instructor> IdInstructors { get; set; } = new List<Instructor>();
    
}