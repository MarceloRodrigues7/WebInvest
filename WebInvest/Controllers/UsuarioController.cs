using Dapper;
using DatabaseLib.Domain;
using DatabaseLib.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ILevelUsuariosRepository _levelUsuariosRepository;
        private readonly string _connectionString;

        public UsuarioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
            _usuariosRepository = new UsuariosRepository();
            _levelUsuariosRepository = new LevelUsuariosRepository();
        }

        public IActionResult Cadastrar()
        {
            if (User.IsInRole("Usuario_Comum"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Login()
        {
            if (User.IsInRole("Usuario_Comum"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult PostUsuario(Usuario usuario)
        {
            try
            {
                usuario.Saldo = 1000;
                usuario.DataAlteracao = DateTime.UtcNow.AddHours(-3);
                _usuariosRepository.Post(usuario);
                return View("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Message"] = "Ocorreu algum erro, tente novamente! " + ex.Message;
                return View("Cadastrar");
            }
        }

        [Authorize]
        public IActionResult Cadastro()
        {
            var data = _usuariosRepository.GetUsuario(long.Parse(User.Identity.Name));
            ViewBag.LevelUsuario = _levelUsuariosRepository.GetLevelECategoriaUsuario(long.Parse(User.Identity.Name));
            return View(data);
        }

        public IActionResult AutenticacaoUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _usuariosRepository.GetUsuario(usuario);
                    if (res != null)
                    {
                        usuario.Id = res.Id;
                        GeraIdentity(usuario);
                        ValidaLevelUsuario(usuario.Id);
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Message"] = "Login Falhou. Username ou Senha inválido";
                return View("Login");
            }
            catch (Exception e)
            {
                TempData["Message"] = $"Ocorreu algum erro, tente novamente! {e.Message}";
                return View("Login");
            }
        }

        private void GeraIdentity(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, "Usuario_Comum")
            };
            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimPrincipal = new(identidadeDeUsuario);
            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true,
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }

        private void ValidaLevelUsuario(long name)
        {
            if (!_levelUsuariosRepository.ValidaUsuarioTabelaLevel(name))
            {
                _levelUsuariosRepository.PostNovoUsuario(name);
            }
        }

    }
}
