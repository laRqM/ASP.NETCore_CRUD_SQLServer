namespace Ejercicio3.Models;

public partial class Alumno
{
    
    // Esta clase es el modelo creado por Scaffold-DbContext de la tabla alumno. 
    
    public int IdPersona { get; set; }

    public string? Matricula { get; set; }

    public string? Carrera { get; set; }

    public string? Semestre { get; set; }

    public string? Especialidad { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> IdReunions { get; set; } = new List<Reunion>();
    //public List<AlumnoReunion> AlumnoReunions { get; set; }
}