using Dapper;
using DatabaseLib.Domain;
using DatabaseLib.Repository;
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
        private readonly IProdutosRepository _produtosRepository;
        private readonly IHistoricoPrecosRepository _historicoPrecosRepository;
        private readonly IOrdensRepository _ordensRepository;
        private readonly IInvestimentosRepository _investimentosRepository;
        private readonly IGameficacaoRepository _gameficacaoRepository;
        private readonly IUsuariosRepository _usuariosRepository;

        public AcoesController(IProdutosRepository produtosRepository, IHistoricoPrecosRepository historicoPrecosRepository, IOrdensRepository ordensRepository, IInvestimentosRepository investimentosRepository, IGameficacaoRepository gameficacaoRepository, IUsuariosRepository usuariosRepository)
        {
            _produtosRepository = produtosRepository;
            _historicoPrecosRepository = historicoPrecosRepository;
            _ordensRepository = ordensRepository;
            _investimentosRepository = investimentosRepository;
            _gameficacaoRepository = gameficacaoRepository;
            _usuariosRepository = usuariosRepository;
        }

        public IActionResult Index()
        {
            var data = _produtosRepository.GetProdutos();
            return View(data);
        }

        public IActionResult Informacao(BaseAcao baseAcao)
        {
            var data = _historicoPrecosRepository.GetHistoricoPrecos(baseAcao.Id);
            return View(data);
        }

        public IActionResult Historico(BaseAcao baseAcao)
        {
            var data = _historicoPrecosRepository.GetHistoricoPrecos(baseAcao.Id);
            return View(data);
        }

        [Authorize]
        public IActionResult Negociar(BaseAcao baseAcao)
        {
            var data = _produtosRepository.GetProduto(baseAcao.Id);
            var transferencia = new Transferencia
            {
                BaseProduto = data,
                Quantidade = 0
            };
            return View(transferencia);
        }

        [Authorize]
        public IActionResult Comprar(Transferencia transferencia)
        {
            if (transferencia.BaseProduto.ValorAtual < 0)
            {
                transferencia.BaseProduto.ValorAtual = transferencia.BaseProduto.ValorAtual * -1;
            }
            try
            {
                var valorTotal = transferencia.Quantidade * transferencia.BaseProduto.ValorAtual;
                var ordem = Ordem.NovaOrdem(int.Parse(User.Identity.Name),
                                        transferencia.BaseProduto.Id,
                                        transferencia.Quantidade,
                                        transferencia.BaseProduto.ValorAtual,
                                        valorTotal,
                                        DateTime.UtcNow.AddHours(-3),
                                        "Enviado",
                                        true
                             );
                _ordensRepository.PostOrdem(ordem);
                FuncGameficacao();
                return RedirectToAction("Index", "Home");
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
            var valorTotal = transferencia.Quantidade * transferencia.BaseProduto.ValorAtual;
            try
            {
                var res = _investimentosRepository.ValidacaoInvestimentoUsuario(transferencia.BaseProduto.Id, int.Parse(User.Identity.Name));
                if (res)
                {
                    var quantidadeAtual = _investimentosRepository.GetQuantidadeAcaoUsuario(transferencia.BaseProduto.Id, int.Parse(User.Identity.Name));
                    if (transferencia.Quantidade <= quantidadeAtual)
                    {
                        var ordem = Ordem.NovaOrdem(int.Parse(User.Identity.Name), transferencia.BaseProduto.Id, transferencia.Quantidade, transferencia.BaseProduto.ValorAtual, valorTotal, DateTime.UtcNow.AddHours(-3), "Sucesso", false);
                        _ordensRepository.PostOrdem(ordem);
                        _investimentosRepository.PutInvestimentoUsuario(transferencia.BaseProduto.Id, int.Parse(User.Identity.Name), quantidadeAtual - transferencia.Quantidade);
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
            var infoUsuario = _gameficacaoRepository.GetLevelUsuario(long.Parse(User.Identity.Name));
            var expGanha = infoUsuario.ExpAtual + 1;
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
