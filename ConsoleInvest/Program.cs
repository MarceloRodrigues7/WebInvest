using ConsoleInvest.Jobs;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleInvest
{
    class Program
    {

        public static void Main()
        {
            Console.Title = "Worker Invest - version 1.0.0";
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Usando Serilog...");
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log/.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("Iniciando worker");
            StartWorkers();
            Log.Information("Worker finalizado");
        }

        private static void StartWorkers()
        {
            IJobAcoes JobAcoes = new JobAcoes();
            IJobInvest JobInvest = new JobInvest();
            while (true)
            {
                JobAcoes.TarefaAcoes();
                JobInvest.TarefaInvest();
                Thread.Sleep(180000);
            }
        }


    }
}
