using Ejercicio3.Models;
using Ejercicio3.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Controllers;

public class AlumnoController : Controller {
    private readonly PracticaCSharpContext _context; // Inicializamos el DbContext.
    private Encriptacion _encrypt; // Inicializamos una variable de la clase Encriptacion.

    public AlumnoController(PracticaCSharpContext context) { // Constructor que recibe un objeto DbContext.
        _context = context;
        _encrypt = new Encriptacion();
    }

    public async Task<IActionResult> Index() { // Index de la página para alumnos.
        ViewData["ActivePage"] = "Alumno"; // Indicamos que la página activa es Alumno. Con esto podemos aplicar la clase 'active' al link en el navbar.
        var alumnos = await _context.Personas
            .Where(persona => persona.Alumno != null)
            .Include(persona => persona.Alumno)
            .ToListAsync();
        // Declaramos a la variable alumnos 
        // await _context.Personas es una operación asincrónica para obtener todos los registros de la
        // tabla "Personas" de la base de datos.
        
        // Where(persona => persona.Alumno != null) filtra la colección de personas seleccionando a
        // aquellas personas cuya propiedad Alumno no es nula. Es decir, filtramos a los que no son alumnos.
        
        //  El método .Include() se utiliza para cargar de manera anticipada la propiedad de navegación "Alumno"
        // de las personas seleccionadas. Esto significa que, cuando se recuperen los estudiantes, también se cargarán
        // en memoria las propiedades relacionadas de "Alumno" para evitar cargarlas de manera diferida más adelante.
        // La carga anticipada se conoce como "eager loading" y la carga diferida como "lazy loading".
        
        // .ToListAsync() convierte la consulta en una lista de objetos y la carga de manera asíncrona.
        // Cuando se completa esta operación, la variable "alumnos" contendrá una lista de objetos que
        // representan estudiantes junto con sus propiedades relacionadas "Alumno" ya cargadas.
        return View(alumnos); // Retornamos la vista y le pasamos la variable alumnos.
    }
    
    public async Task<IActionResult> Edit(int id)
    { // Esta función trae los datos correspondientes al alumno con el ID que recibe la función y los inyecta al formulario en la vista Edit.
        ViewData["ActivePage"] = "Alumno"; // Indicamos que la página activa es Alumno. Con esto podemos aplicar la clase 'active' al link en el navbar.
        var alumno = await _context.Personas
            .Where(persona => persona.IdPersona == id)
            .Include(persona => persona.Alumno)
            .FirstOrDefaultAsync();

        // Declaramos a la variable alumno 
        // await _context.Personas es una operación asincrónica para obtener todos los registros de la
        // tabla "Personas" de la base de datos.
        
        // Where(persona => persona.IdPersona == id) filtra la colección de personas seleccionando a aquella
        // cuyo id_persona sea igual al id que está recibiendo la función Edit.
        
        //  El método .Include() se utiliza para cargar de manera anticipada la propiedad de navegación "Alumno"
        // de las personas seleccionadas. Esto significa que, cuando se recuperen los estudiantes, también se cargarán
        // en memoria las propiedades relacionadas de "Alumno" para evitar cargarlas de manera diferida más adelante.
        // La carga anticipada se conoce como "eager loading" y la carga diferida como "lazy loading".
        
        // .FirstOrDefaultAsync(): Este método se utiliza para seleccionar la primera persona que cumple con el
        // filtro establecido. La operación se realiza de manera asincrónica, lo que significa que se espera la
        // respuesta de la base de datos de manera asincrónica y, una vez que se completa, se obtiene el primer
        // objeto que cumple con el filtro o null si no se encuentra ninguna coincidencia.
        
        if (alumno != null)
        { // Si alumno no es núlo...
            var model = new AlumnoViewModel // Declaramos una variable llamada model y usando la clase AlumnoViewModel
            {
                // Los campos de AlumnoViewModel(lado izquierdo) serán iguales a lo recibido en la variable alumno(lado derecho).
                IDPersona = alumno.IdPersona,
                NombreUno = _encrypt.Desencriptar(alumno.NombreUno),
                NombreDos = alumno.NombreDos,
                ApellidoUno = alumno.ApellidoUno,
                ApellidoDos = alumno.ApellidoDos,
                FechaNacimiento = alumno.DNacimiento,
                Matricula = alumno.Alumno.Matricula,
                Carrera = alumno.Alumno.Carrera,
                Semestre = alumno.Alumno.Semestre,
                Especialidad = alumno.Alumno.Especialidad
            };

            return View("Edit", model); // Generamos la vista de nombre Edit y redirigimos a ella y además, le pasamos el modelo con los datos ya asignados.
        }

        return RedirectToAction("Index"); // Retornamos al usuario a la acción Index.
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AlumnoViewModel model)
    { // Esta función actualiza los datos del alumno en la base de datos usando el modelo que recibe.
        if (ModelState.IsValid)
        { // Si el estado del modelo es válido...
            var alumno = await _context.Personas
                .Where(persona => persona.IdPersona == model.IDPersona)
                .Include(persona => persona.Alumno)
                .FirstOrDefaultAsync();
            
            // Declaramos a la variable alumno 
            // await _context.Personas es una operación asincrónica para obtener todos los registros de la
            // tabla "Personas" de la base de datos.
            
            // Where(persona => persona.IdPersona == model.IDPersona) filtra la colección de personas seleccionando
            // a aquella cuyo id_persona sea igual al id_persona que está recibiendo en el modelo.
            
            //  El método .Include() se utiliza para cargar de manera anticipada la propiedad de navegación "Alumno"
            // de las personas seleccionadas. Esto significa que, cuando se recuperen los estudiantes, también se cargarán
            // en memoria las propiedades relacionadas de "Alumno" para evitar cargarlas de manera diferida más adelante.
            // La carga anticipada se conoce como "eager loading" y la carga diferida como "lazy loading".
            
            // .FirstOrDefaultAsync(): Este método se utiliza para seleccionar la primera persona que cumple con el
            // filtro establecido. La operación se realiza de manera asincrónica, lo que significa que se espera la
            // respuesta de la base de datos de manera asincrónica y, una vez que se completa, se obtiene el primer
            // objeto que cumple con el filtro o null si no se encuentra ninguna coincidencia.
            
            if (alumno != null)
            { // Si alumno es distinto de núlo...
                // Los campos de la variable alumno(lado izquierdo), que recibe los datos directamente de la base de datos,
                // serán igual a los datos que se recibieron en el modelo(lado derecho).
                alumno.NombreUno = _encrypt.Encriptar(model.NombreUno);
                alumno.NombreDos = model.NombreDos;
                alumno.ApellidoUno = model.ApellidoUno;
                alumno.ApellidoDos = model.ApellidoDos;
                alumno.DNacimiento = model.FechaNacimiento;

                alumno.Alumno.Matricula = model.Matricula;
                alumno.Alumno.Carrera = model.Carrera;
                alumno.Alumno.Semestre = model.Semestre;
                alumno.Alumno.Especialidad = model.Especialidad;

                await _context.SaveChangesAsync();
                // await _context.SaveChangesAsync(); significa que se están guardando todos los cambios pendientes
                // en el contexto de datos de Entity Framework de manera asincrónica. Esto significa que que no bloqueará
                // el hilo de ejecución principal, permitiendo que el programa continúe ejecutándose de manera eficiente
                // mientras se espera la respuesta de la base de datos.

                return RedirectToAction("Index"); // Retornamos al usuario a la acción Index.
            }
        }

        return View("Edit", model); // Si hay errores de validación, muestra la vista de edición nuevamente.
    }

    public IActionResult add()
    {
        ViewData["ActivePage"] = "Alumno"; // Indicamos que la página activa es Alumno. Con esto podemos aplicar la clase 'active' al link en el navbar.
        return View(); // Retornamos la vista.
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> add(AlumnoViewModel model)
    { // Esta función agrega un nuevo alumno a la base de datos utilizando el modelo que le estamos pasando.
        if (ModelState.IsValid)
        { // Si el estatus del modelo es válido...
            var persona = new Persona // Declaramos la variable persona e inicializamos la instancia de la clase.
            {
                // Los campos de Persona(lado izquierdo) serán igual a los datos recibidos en el modelo(lado derecho).
                
                NombreUno = _encrypt.Encriptar(model.NombreUno),
                NombreDos = model.NombreDos,
                ApellidoUno = model.ApellidoUno,
                ApellidoDos = model.ApellidoDos,
                DNacimiento = model.FechaNacimiento,
                TipoRol = "Alumno" // Asignamos el rol directamente.
            };
            
            _context.Personas.Add(persona); // Agregamos el objeto Persona al contexto
            await _context.SaveChangesAsync();
    
            // Obtener el ID generado para la nueva persona
            var idPersona = persona.IdPersona;
    
            // Crear una instancia de Alumno y llenarla con los datos del modelo AlumnoViewModel
            var alumno = new Alumno
            {
                IdPersona = idPersona, // Asociar el ID de la persona con el alumno
                Matricula = model.Matricula,
                Carrera = model.Carrera,
                Semestre = model.Semestre,
                Especialidad = model.Especialidad
            };
    
            // Agregar el objeto Alumno al contexto y guardar los cambios en la base de datos
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            // await _context.SaveChangesAsync(); significa que se están guardando todos los cambios pendientes
            // en el contexto de datos de Entity Framework de manera asincrónica. Esto significa que que no bloqueará
            // el hilo de ejecución principal, permitiendo que el programa continúe ejecutándose de manera eficiente
            // mientras se espera la respuesta de la base de datos.
    
            return RedirectToAction(nameof(Index));
            // Después de que se haya realizado la operación, el usuario será redirigido a la acción "Index".
            // nameof(Index) se utiliza para obtener el nombre de la acción "Index" de forma segura en tiempo de compilación,
            // lo que evita errores de escritura del nombre de la acción.
        }
    
        return View(model); // Retornamos la vista pasándole el modelo
    }

    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    { // Esta función elimina de la base de datos los datos seleccionados.
        var alumno = await _context.Personas
            .Where(persona => persona.IdPersona == id)
            .Include(persona => persona.Alumno)
            .FirstOrDefaultAsync();
        
        // Declaramos a la variable alumno 
        // await _context.Personas es una operación asincrónica para obtener todos los registros de la
        // tabla "Personas" de la base de datos.
            
        // Where(persona => persona.IdPersona == id) filtra la colección de personas seleccionando
        // a aquella cuyo id_persona sea igual al id que está recibiendo en la función.
            
        //  El método .Include() se utiliza para cargar de manera anticipada la propiedad de navegación "Alumno"
        // de las personas seleccionadas. Esto significa que, cuando se recuperen los estudiantes, también se cargarán
        // en memoria las propiedades relacionadas de "Alumno" para evitar cargarlas de manera diferida más adelante.
        // La carga anticipada se conoce como "eager loading" y la carga diferida como "lazy loading".
            
        // .FirstOrDefaultAsync(): Este método se utiliza para seleccionar la primera persona que cumple con el
        // filtro establecido. La operación se realiza de manera asincrónica, lo que significa que se espera la
        // respuesta de la base de datos de manera asincrónica y, una vez que se completa, se obtiene el primer
        // objeto que cumple con el filtro o null si no se encuentra ninguna coincidencia.
        
        if (alumno != null)
        { // Si alumno es distinto de núlo...
            _context.Personas.Remove(alumno); // Marcamos el objeto alumno para su eliminación en la base de datos.
            await _context.SaveChangesAsync();
            // await _context.SaveChangesAsync(); significa que se están guardando todos los cambios pendientes
            // en el contexto de datos de Entity Framework de manera asincrónica. Esto significa que que no bloqueará
            // el hilo de ejecución principal, permitiendo que el programa continúe ejecutándose de manera eficiente
            // mientras se espera la respuesta de la base de datos.
        }
        
        return RedirectToAction("Index"); // Retornamos al usuario a la acción Index.
    }
}