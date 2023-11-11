namespace Ejercicio3.Models;

public partial class Instructor
{
    
    // Esta clase es el modelo creado por Scaffold-DbContext de la tabla instructor.
    
    public int IdPersona { get; set; }

    public string? Folio { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> IdReunions { get; set; } = new List<Reunion>();
}