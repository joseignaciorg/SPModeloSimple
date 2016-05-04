using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos.Alumnos
{
    //clase de muestra donde definimos todas las properties 
    /// <summary>
    /// This class contains the properties for Alumno. The properties keep the data for Alumno.
    /// If you want to rename the class, don't forget to rename the entity in the model xml as well.
    /// </summary>
    public partial class Alumno
    {
        //TODO: Implement additional properties here. The property Message is just a sample how a property could look like.
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
    }
}
