using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;
using WebInvest.Repositorys;

namespace WebInvest.Controllers
{
    public class AcoesController : Controller
    {
        private readonly IAcoesRepository _acoesRepository;
        private readonly IOrdensRepository _ordensRepository;
        private readonly IInvestimentosRepository _investimentosRepository;
        private readonly IGameficacaoRepository _gameficacaoRepository;
        private readonly IUsuariosRepository _usuariosRepository;

        public AcoesController(IAcoesRepository acoesRepository, IOrdensRepository ordensRepository, IInvestimentosRepository investimentosRepository, IGameficacaoRepository gameficacaoRepository, IUsuariosRepository usuariosRepository)
        {
            _acoesRepository = acoesRepository;
            _ordensRepository = ordensRepository;
            _investimentosRepository = investimentosRepository;
            _gameficacaoRepository = gameficacaoRepository;
            _usuariosRepository = usuariosRepository;
        }

        public IActionResult Index()
        {
            var data = _acoesRepository.GetAcoes();
            return View(data);
        }

        public IActionResult Informacao(BaseAcao baseAcao)
        {
            var data = _acoesRepository.GetHistoricoAcao(baseAcao.Id);
            return View(data);
        }

        public IActionResult Historico(BaseAcao baseAcao)
        {
            var data = _acoesRepository.GetHistoricoAcao(baseAcao.Id);
            return View(data);
        }

        [Authorize]
        public IActionResult Negociar(BaseAcao baseAcao)
        {
            var data = _acoesRepository.GetAcao(baseAcao.Id);
            var transferencia = new Transferencia
            {
                _BaseAcao = data,
                Quantidade = 0
            };
            return View(transferencia);
        }

        [Authorize]
        public IActionResult Comprar(Transferencia transferencia)
        {
            if (transferencia._BaseAcao.ValorAtual < 0)
            {
                transferencia._BaseAcao.ValorAtual = transferencia._BaseAcao.ValorAtual * -1;
            }
            try
            {
                var valorTotal = transferencia.Quantidade * transferencia._BaseAcao.ValorAtual;
                var ordem = new Ordem()
                            .NovaOrdem(int.Parse(User.Identity.Name),
                                        transferencia._BaseAcao.Id,
                                        transferencia.Quantidade,
                                        transferencia._BaseAcao.ValorAtual,
                                        valorTotal,
                                        DateTime.UtcNow.AddHours(-3),
                                        "Enviado",
                                        true
                             );
                _ordensRepository.PostOrdem(ordem);
                FuncGameficacao();
                return RedirectToAction("Index", "Transacao");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["Message"] = "Ocorreu algum erro, tente novamente! " + e.Message;
                return View("Negociar", transferencia);
            }
        }

        [Authorize]
        public IActionResult Vender(Transferencia transferencia)
        {
            var saldo = _usuariosRepository.GetSaldoUsuario(int.Parse(User.Identity.Name));
            var valorTotal = transferencia.Quantidade * transferencia._BaseAcao.ValorAtual;
            try
            {
                var res = _investimentosRepository.ValidacaoInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name));
                if (res)
                {
                    var quantidadeAtual = _investimentosRepository.GetQuantidadeAcaoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name));
                    if (transferencia.Quantidade <= quantidadeAtual)
                    {
                        var ordem = new Ordem().NovaOrdem(int.Parse(User.Identity.Name), transferencia._BaseAcao.Id, transferencia.Quantidade, transferencia._BaseAcao.ValorAtual, valorTotal, DateTime.UtcNow.AddHours(-3), "Sucesso", false);
                        _ordensRepository.PostOrdem(ordem);
                        _investimentosRepository.PutInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name), quantidadeAtual - transferencia.Quantidade);
                        _usuariosRepository.PutSaldoUsuario(int.Parse(User.Identity.Name), saldo + valorTotal);
                        return RedirectToAction("Index", "Transacao");
                    }
                }
                TempData["Message"] = "Quantidade insuficiente para realizar venda!";
                return View("Negociar", transferencia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Message"] = "Ocorreu algum erro, tente novamente! " + ex.Message;
                return View("Negociar", transferencia);
            }
        }

        private void FuncGameficacao()
        {
            var infoUsuario = _gameficacaoRepository.GetLevelUsuario(User.Identity.Name);
            var expGanha = infoUsuario.ExpAtual + LevelUsuario.GanhaExp;
            _gameficacaoRepository.PutExpAtual(infoUsuario.Id, expGanha);
            if (infoUsuario.ExpProximo <= expGanha)
            {
                var novoLevel = infoUsuario.LevelAtual + 1;
                var novoExpProximo = infoUsuario.ExpProximo * 3;
                var novaCategoria = _gameficacaoRepository.GetIdCategoriaPorLevel(novoLevel);
                _gameficacaoRepository.AtualizaNovoLevel(infoUsuario.Id, novoLevel, novoExpProximo, novaCategoria);
            }
        }
    }
}
