using ConsoleInvest.Utils;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobInvest : IJobInvest
    {
        public bool TarefaInvest()
        {
            List<Task> task = new();
            task.Add(Task.Factory.StartNew(() =>
            {
                DeletaQtdZero();
            }));
            Task.WaitAll(task.ToArray());
            task.Clear();
            Log.Information($"Tarefa Invest finalizada.");
            return true;
        }

        private static void DeletaQtdZero()
        {
            try
            {
                var query = "delete InvestimentosUsuarios where Quantidade=0";
                using (var connection = new SqlConnection(Services.ConnectionString))
                {
                    connection.Open();
                    connection.Execute(query);
                    connection.Close();
                };
            }
            catch (Exception e)
            {
                Log.Error($"Erro ao deletar investimentos zerados. " + e.Message);
            }
        }
        
    }
}
