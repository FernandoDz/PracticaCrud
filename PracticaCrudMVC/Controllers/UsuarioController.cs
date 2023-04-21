using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaCrudBL;
using PracticaCrudEN;

namespace PracticaCrudMVC.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBL  usuarioBL = new UsuarioBL(); //instancia de acceso a datos a la clase RolBL
        RolBL rolBL = new RolBL();

        // accion que muestra la pagina principal de roles
        public async Task<IActionResult> Index(Usuario pUsuario = null)
        {

            if (pUsuario == null)
                pUsuario = new Usuario();
            if (pUsuario.top_aux == 0)
                pUsuario.top_aux = 10;
            else if (pUsuario.top_aux == -1) //si el valor es -1 se regresa a 1//
                pUsuario.top_aux = 0;
            var usuarios = await usuarioBL.BuscarAsync(pUsuario);
            ViewBag.Top = pUsuario.top_aux;
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            return View(usuarios);
        }

        // accion que muestra el detalle de un registro existente
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = id });
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
            return View(usuario);
        }

        //   accion para mostrar el formulario 
        public async Task <IActionResult> Create()
        {
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos del formulario y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken] //evita inyecciones en sql 
        public async Task<IActionResult> Create(Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.CrearAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View();
            }
        }
        public async Task<IActionResult> Edit(Usuario pUsuario)
        {

            var taskObtenerTodosRoles = rolBL.ObtenerTodosAsync();
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            ViewBag.Roles = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            ViewBag.Error = "";
            return View(usuario);
        }

        //accion que recibe los datos modificados para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.ModificarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View(pUsuario);
            }
        }

        // opcion que muestra los datos del registro para confirmar los datos de eliminacion 
        public async Task<IActionResult> Delete(Usuario pUsuario)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
            ViewBag.Error = "";
            return View(usuario);
        }

        // accion que recibe la confirmacion oara eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.EliminarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
                if (usuario == null)
                    usuario = new Usuario();
                if (usuario.Id > 0)
                    usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
                return View(usuario);
            }
        }


    }
}
