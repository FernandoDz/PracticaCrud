using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticaCrudEN;

namespace PracticaCrudDAL
{
    public class BDContexto:DbContext
    {
        public DbSet<Rol> Roles{get; set;}
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer(@"Data Source=DESKTOP-5D189V7;Initial Catalog=Practica_Crud;Integrated Security=True");

        }
    }
}
