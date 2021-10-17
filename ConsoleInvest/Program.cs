using ConsoleInvest.Jobs;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

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
            JobTransacoes();
            JobAcoes();
            //JobCriptomoeda();
            JobInvestimentos();
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void JobTransacoes()
        {
            IJobTransacoes jobTransacoes = new JobTransacoes();
            Timer timer = new(60000);
            timer.AutoReset = true;
            timer.Elapsed += delegate
            {                
                jobTransacoes.TarefaTransacoes();
            };
            timer.Start();
            jobTransacoes.TarefaTransacoes();
        }

        private static void JobAcoes()
        {
            IJobAcoes JobAcoes = new JobAcoes();
            Timer timer = new(180000);
            timer.AutoReset = true;
            timer.Elapsed += delegate
            {
                JobAcoes.TarefaAcoes();
            };
            timer.Start();
            JobAcoes.TarefaAcoes();
        }

        private static void JobInvestimentos()
        {
            IJobInvest JobInvest = new JobInvest();
            Timer timer = new(300000);
            timer.AutoReset = true;
            timer.Elapsed += delegate
            {
                JobInvest.TarefaInvest();
            };
            timer.Start();
            JobInvest.TarefaInvest();
        }

    }
}
