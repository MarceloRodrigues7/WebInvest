using ConsoleInvest.Models;
using ConsoleInvest.Utils;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobCriptomoeda : IJobCriptomoeda
    {
        public bool TarefaCriptomoeda()
        {
            try
            {
                List<Task> task = new();
                var criptomoedas = GetCriptomoedas();
                foreach (var criptomoeda in criptomoedas)
                {
                    task.Add(Task.Run(() =>
                    {
                        criptomoeda.ValorAtual = NovoValor(criptomoeda.ValorAtual);
                        criptomoeda.DataAtualizacao = DateTime.UtcNow.AddHours(-3);
                        PostHistorico(criptomoeda);
                        PutCriptomoeda(criptomoeda);
                        Log.Information($"Criptomoeda[{criptomoeda.Id}] - Novo Valor[{criptomoeda.ValorAtual}]");
                    }));
                }
                Task.WaitAll(task.ToArray());
                task.Clear();
                Log.Information($"Tarefa Criptomoeda finalizada.");
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Erro Criptomoeda - " + e);
                return false;
            }
        }

        private List<Criptomoeda> GetCriptomoedas()
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Criptomoedas";
                var data = connection.Query<Criptomoeda>(query).ToList();
                connection.Close();
                return data;
            };
        }

        private decimal NovoValor(decimal valorAtual)
        {
            var valorGerado = GeraNovoValor();
            if (valorAtual < 0 || (int)valorGerado == 0)
            {
                return valorAtual + valorGerado;
            }
            else
            {
                return valorAtual - valorGerado;
            }
        }
        private static decimal GeraNovoValor()
        {
            var rand = new Random();
            var inteiro = rand.Next(0, 1);
            var decimalUm = rand.Next(0, 9);
            var decimalDois = rand.Next(1, 9);
            return decimal.Parse($"{inteiro},{decimalUm}{decimalDois}");
        }

        private static void PutCriptomoeda(Criptomoeda criptomoeda)
        {
            try
            {
                using (var connection = new SqlConnection(Services.ConnectionString))
                {
                    connection.Open();
                    var query = "UPDATE Criptomoedas SET ValorAtual=@ValorAtual,DataAtualizacao=@DataAtualizacao WHERE Id=@Id";
                    connection.Execute(query, new
                    {
                        criptomoeda.ValorAtual,
                        criptomoeda.DataAtualizacao,
                        criptomoeda.Id
                    });
                    connection.Close();
                };
                    
            }
            catch (Exception e)
            {
                Log.Error("Erro Criptomoeda - " + e);
            }
            
        }

        private static void PostHistorico(Criptomoeda criptomoeda)
        {
            try
            {
                using (var connection = new SqlConnection(Services.ConnectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO HistoricoPrecoCriptomoedas(IdCriptomoeda,Valor,DataHora)VALUES(@Id,@ValorAtual,@DataAtualizacao)";
                    connection.Execute(query, new
                    {
                        criptomoeda.Id,
                        criptomoeda.ValorAtual,
                        criptomoeda.DataAtualizacao
                    });
                    connection.Close();
                };
                    
            }
            catch (Exception e)
            {
                Log.Error("Erro Criptomoeda - " + e);
            }
        }

    }
}
