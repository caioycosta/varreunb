using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VarreUnB;
using System.Net;
using System.Threading;

namespace Monitoramento_vagas
{
    class Program
    {
        static void Main(string[] args)
        {
            bool executou = false;
            int total, ocupadas, disponiveis;
            var rnd = new Random();

            total = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Total"]);
            ocupadas = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Ocupadas"]);
            disponiveis = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Disponiveis"]);

            string disciplina = System.Configuration.ConfigurationManager.AppSettings["Disciplina"];
            string turma = System.Configuration.ConfigurationManager.AppSettings["Turma"];
            string codigo = System.Configuration.ConfigurationManager.AppSettings["Codigo"];

            while (true)
            {
                try
                {
                    Console.WriteLine("Consultando disciplina. {0}", DateTime.Now);
                    var wc = new ClienteWeb();

                    wc.Encoding = Encoding.UTF8;
                    var turmas = VarreUnB.RoboUnb.LerTurmas2(wc, disciplina);

                    var turmaDesejada = turmas.Single(n => n.Nome == turma);

                    Console.WriteLine(@"valores de vaga:
Total : {0}
Ocupadas: {1}
Disponiveis: {2}", turmaDesejada.Total, turmaDesejada.Ocupadas, turmaDesejada.Disponiveis);

                    if (turmaDesejada.Total != total || turmaDesejada.Ocupadas != ocupadas || turmaDesejada.Disponiveis != disponiveis)
                    {
                        total = (int)turmaDesejada.Total;
                        ocupadas = (int)turmaDesejada.Ocupadas;
                        disponiveis = (int)turmaDesejada.Disponiveis;

                        EnviarEmailVagas(total, ocupadas, disponiveis, codigo);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message + " " + DateTime.Now.ToString()); }

                // intervalo exponencial. média 1 evento a cada 5 minutos => lambda 10/3e6 .

                int intervalo = (int)(Math.Log(1.0 - rnd.NextDouble()) / (-10.0 / 3000000.0));
                
                if (!executou)
                {
					EnviarEmail(DateTime.Now.Ticks + " monitoração iniciada com sucesso.", DateTime.Now.Ticks + "monitoração MW", "seumail@aqui.com");
                    executou = true;
                }
                Thread.Sleep(intervalo);
            }
        }

        private static void EnviarEmailVagas(int total, int ocupadas, int disponiveis, string codigo)
        {
           
            string texto = String.Format(@"{3} - Disciplina {4} monitorada foi modificada! 
Novos valores de vaga:
Total : {0}
Ocupadas: {1}
Disponiveis: {2}

Corra e faça sua matricula!
", total, ocupadas, disponiveis, DateTime.Now.Ticks, codigo);
            string assunto = DateTime.Now.Ticks + " Disciplina modificada no matrícula Web!";
            string para = "seumail@aqui.com";

            EnviarEmail(texto, assunto, para);
        }

        private static void EnviarEmail(string texto, string assunto, string para)
        {
			throw new NotImplementedException ();
        }
    }
}
