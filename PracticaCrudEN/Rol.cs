using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaCrudEN
{
    public  class Rol
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [NotMapped]
        public int top_aux { get; set; } //propiedad auxiliar

        //propiedad de navegacion 
        public List<Usuario> Usuarios { get; set; }

    }
}
