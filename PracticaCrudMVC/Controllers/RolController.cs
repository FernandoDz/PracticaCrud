using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaCrudBL;
using PracticaCrudDAL;
using PracticaCrudEN;

namespace PracticaCrudMVC.Controllers
{
    public class RolController : Controller
    {
        RolBL rolBL = new RolBL(); //instancia de acceso a datos a la clase RolBL

        // accion que muestra la pagina principal de roles
        public async Task<IActionResult> Index(Rol pRol = null)
        {

            if (pRol == null)
                pRol = new Rol();
            if (pRol.top_aux == 0)
                pRol.top_aux = 10;
            else if (pRol.top_aux == -1) //si el valor es -1 se regresa a 1//
                pRol.top_aux = 0;
            var roles = await rolBL.BuscarAsync(pRol);
            ViewBag.Top = pRol.top_aux;
            return View(roles);
        }

        // accion que muestra el detalle de un registro existente
        public async Task<IActionResult> Details(int id)
        {
            var rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = id });
            return View(rol);
        }

        //   accion para mostrar el formulario 
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos del formulario y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken] //evita inyecciones en sql 
        public async Task<IActionResult> Create(Rol Prol)
        {
            try
            {
                int result = await rolBL.CrearAsync(Prol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // accion que  muestra el formulario con los datos cargados para modificarlos
        public async Task<IActionResult> Edit(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol);
        }

        // accion que recibe los datos modificados y los envia a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Rol pRol)
        {
            try

            {
                int result = await rolBL.ModificarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // accion que muestra los datos a borrar
        public async Task<ActionResult> Delete(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol);
        }

        // accion que elimina el formulario de la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try

            {
                int result = await rolBL.EliminarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

    }
}
