using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaCrudEN
{
    public class Usuario
    {
        [Key]
        public int  Id { get; set; }
        public int RolId { get; set; }
        public string Nombre { get; set; }

        public string Correo{ get; set; }

        public string Password { get; set; }

        public Rol Rol { get; set; }
        [NotMapped]
        public int top_aux { get; set; }



    }
}
