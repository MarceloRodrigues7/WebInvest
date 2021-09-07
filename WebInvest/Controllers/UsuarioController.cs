using Dapper;
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
        private readonly string _connectionString;

        public UsuarioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
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
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"INSERT INTO Usuarios(Username,Password,Email,Nome,Sobrenome,DataNascimento,Saldo,DataAlteracao)
                              VALUES(@Username,@Password,@Email,@Nome,@Sobrenome,@DataNascimento,1000,getdate())";
                    var data = connection.Execute(query, new { usuario.Username, usuario.Password, usuario.Email, usuario.Nome, usuario.Sobrenome, usuario.DataNascimento });
                    connection.Close();
                    return View("Login");
                };
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Usuarios WHERE Id=@Id";
                var data = connection.QueryFirst<Usuario>(query, new { Id = User.Identity.Name });
                connection.Close();
                return View(data);
            };
        }
                
        public IActionResult AutenticacaoUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var query = "SELECT Id FROM Usuarios WITH(NOLOCK) WHERE Username=@Username AND Password=@Password";
                        connection.Open();
                        var res = connection.QueryFirstOrDefault<int>(query, new { usuario.Username, usuario.Password });
                        connection.Close();
                        if (res > 0)
                        {
                            usuario.Id = res;
                            GeraIdentity(usuario);
                            return RedirectToAction("Index","Home");
                        }
                    };
                }
                TempData["Message"] = "Login Falhou. Username ou Senha inválido";
                return View("Login");
            }
            catch (Exception)
            {
                TempData["Message"] = "Ocorreu algum erro, tente novamente!";
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

    }
}
