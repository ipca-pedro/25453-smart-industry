using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFactory.Client.ServiceRef; 

namespace SmartFactory.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Painel de Controlo Industrial";
            Console.WriteLine("=== SMART INDUSTRY CLIENT (SOAP) ===");

            try
            {
                // 1. Instanciar o cliente (o 'proxy' para o servidor)
                // O nome 'MachineServiceClient' foi gerado automaticamente pelo Visual Studio
                MachineServiceClient robot = new MachineServiceClient();

                Console.WriteLine("\n[1] A contactar o servidor WCF...");

                // 2. Chamar o método remoto que vai ao PostgreSQL
                var listaSensores = robot.GetCurrentSensors();

                Console.WriteLine($"[2] Resposta recebida! Total de sensores: {listaSensores.Length}\n");

                // 3. Mostrar os dados no ecrã
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("| ID   | POLO       | VALOR      | UNIDADE | DATA/HORA      |");
                Console.WriteLine("-------------------------------------------------------------");

                foreach (var s in listaSensores)
                {
                    Console.WriteLine($"| {s.SensorId,-4} | {s.Polo,-10} | {s.Valor,-10} | {s.Unidade,-7} | {s.DataHora.ToShortTimeString(),-10} |");
                }
                Console.WriteLine("-------------------------------------------------------------");

                // Fechar a ligação
                robot.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERRO] Não foi possível comunicar com o serviço.");
                Console.WriteLine("Dica: Verifica se o projeto 'SmartFactory.Soap' está a correr!");
                Console.WriteLine("Detalhe: " + ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("\nPrime [ENTER] para sair.");
            Console.ReadLine();
        }
    }
}