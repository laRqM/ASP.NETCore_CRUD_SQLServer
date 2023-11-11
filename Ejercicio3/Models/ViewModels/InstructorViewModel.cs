using System.ComponentModel.DataAnnotations;

namespace Ejercicio3.Models.ViewModels;

public class InstructorViewModel
{
    
    // Esta clase es el modelo para mostrar la unión de las tablas persona e instructor.
    // De igual forma se utiliza para actualizar y crear datos en esas tablas.
    
    public int IdPersona { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Primer Nombre")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? NombreUno { get; set; }
    
    [Display(Name = "Segundo Nombre")]
    public string? NombreDos { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Apellido Paterno")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? ApellidoUno { get; set; }
    
    [Display(Name = "Apellido Materno")]
    public string? ApellidoDos { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Fecha de Nacimiento")]
    public DateTime? FechaNacimiento { get; set; }
    
    [Display(Name = "Tipo de Rol")]
    public string? Rol { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Folio")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? Folio { get; set; }
}