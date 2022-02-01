using Dapper;
using DatabaseLib.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Controllers
{
    public class InvestimentoController : Controller
    {
        private readonly IInvestimentosUsuarioRepository _investimentosUsuarioRepository;
        private readonly ILogger<InvestimentoController> _logger;

        private readonly string _connectionString;

        public InvestimentoController(ILogger<InvestimentoController> logger, IConfiguration configuration)
        {
            _investimentosUsuarioRepository = new InvestimentosUsuarioRepository();
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        [Authorize]
        public IActionResult Index()
        {
            var data = _investimentosUsuarioRepository.GetInvestimentoUsuarios(long.Parse(User.Identity.Name));
            return View(data);
        }
    }
}
