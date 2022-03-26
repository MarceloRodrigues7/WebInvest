using Dapper;
using DatabaseLib.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebInvest.Controllers
{
    public class InvestimentoController : Controller
    {
        private readonly IInvestimentosUsuarioRepository _investimentosUsuarioRepository;
        private readonly ILogger<InvestimentoController> _logger;

        public InvestimentoController(ILogger<InvestimentoController> logger)
        {
            _investimentosUsuarioRepository = new InvestimentosUsuarioRepository();
            _logger = logger;            
        }

        [Authorize]
        public IActionResult Index()
        {
            var data = _investimentosUsuarioRepository.GetInvestimentoUsuarios(long.Parse(User.Identity.Name));
            return View(data);
        }
    }
}
