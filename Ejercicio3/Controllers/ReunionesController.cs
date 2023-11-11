using Ejercicio3.Models;
using System.Linq;
using Ejercicio3.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Controllers;

public class ReunionesController : Controller {
    private readonly PracticaCSharpContext _context;


    public ReunionesController(PracticaCSharpContext context) {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        ViewData["ActivePage"] = "Reuniones"; // Indicamos que la página activa es Instructor. Con esto podemos aplicar la clase 'active' al link en el navbar.
        
        var reunionData = _context.AlumnoReunionViews
            .Select(a => new AlumnoReunionView
            {
                id_persona = a.id_persona,
                nombre_uno = a.nombre_uno,
                nombre_dos = a.nombre_dos,
                apellido_uno = a.apellido_uno,
                apellido_dos = a.apellido_dos,
                D_nacimiento = a.D_nacimiento,
                matricula = a.matricula,
                carrera = a.carrera,
                semestre = a.semestre,
                especialidad = a.especialidad,
                id_reunion = a.id_reunion,
                fecha = a.fecha,
                hora = a.hora,
                lugar = a.lugar,
                tema = a.tema
            })
            .ToList();
        
        // Declaramos la variable reunionData para almacenar el resultado de la consulta con Entity Framework
        
        // _context.AlumnoReunionViews accede a la propiedad llamada AlumnoReunionViews en el objeto _context,
        // siendo este último el contexto de Entity Framework que proporciona acceso a la base de datos.
        
        // Utilizamos el método .Select() para proyectar los datos de la entidad AlumnoReunionViews en una nueva entidad AlumnoReunionView.
        // O sea, creamos una lista de objetos AlumnoReunionView a partir de los objetos AlumnoReunionViews originales.
        
        // Después hacemos las asignaciones. Los campos de AlumnoReunionView(lado izquierdo) serán iguales a lo recibido en la variable
        // a(lado derecho).
        
        // .ToList() se utiliza para ejecutar la consulta y convertir el resultado en una lista de objetos AlumnoReunionView.

        return View(reunionData); // Retornamos la vista pásandole la variable.
    }
}