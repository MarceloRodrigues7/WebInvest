using ConsoleInvest.Jobs;
using Serilog;
using System;
using System.Reflection;
using System.Timers;

namespace ConsoleInvest
{
    class Program
    {
        public static void Main()
        {
            var versao = Assembly.GetExecutingAssembly().GetName().Version;
            Console.Title = $"ConsoleInvest - versão {versao}";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Usando Serilog...");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log/.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Iniciando tarefas");
            Iniciar();
            Log.Information("Tarefas finalizadas");
        }

        private static void Iniciar()
        {
            TarefaProdutos();
            TarefaTransacoes();
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void TarefaProdutos()
        {
            var jobProdutos = new JobProdutos();
            Timer timer = new(180000);
            timer.AutoReset = true;
            timer.Elapsed += delegate
            {
                jobProdutos.TarefaProdutos();
            };
            timer.Start();
            jobProdutos.TarefaProdutos();
        }

        private static void TarefaTransacoes()
        {
            var jobTransacoes = new JobTransacoes();
            Timer timer = new(60000);
            timer.AutoReset = true;
            timer.Elapsed += delegate
            {
                jobTransacoes.TarefaTransacoes();
            };
            timer.Start();
            jobTransacoes.TarefaTransacoes();
        }
        
    }
}
