using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VarreUnB
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeMQ.CodeMQManager.LoadQueue(typeof(FilaDisciplinas));

            //Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);

            //Console.Beep(658, 125); Console.Beep(1320, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 250); Console.Beep(1320, 125); Console.Beep(1188, 125); Console.Beep(1056, 250); Console.Beep(990, 250); Console.Beep(880, 500); Console.Beep(880, 250); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 750); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(250); Console.Beep(1188, 500); Console.Beep(1408, 250); Console.Beep(1760, 500); Console.Beep(1584, 250); Console.Beep(1408, 250); Console.Beep(1320, 750); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(500); Console.Beep(1320, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 250); Console.Beep(1320, 125); Console.Beep(1188, 125); Console.Beep(1056, 250); Console.Beep(990, 250); Console.Beep(880, 500); Console.Beep(880, 250); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 750); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(250); Console.Beep(1188, 500); Console.Beep(1408, 250); Console.Beep(1760, 500); Console.Beep(1584, 250); Console.Beep(1408, 250); Console.Beep(1320, 750); Console.Beep(1056, 250); Console.Beep(1320, 500); Console.Beep(1188, 250); Console.Beep(1056, 250); Console.Beep(990, 500); Console.Beep(990, 250); Console.Beep(1056, 250); Console.Beep(1188, 500); Console.Beep(1320, 500); Console.Beep(1056, 500); Console.Beep(880, 500); Console.Beep(880, 500); Thread.Sleep(500); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 1000); Console.Beep(440, 1000); Console.Beep(419, 1000); Console.Beep(495, 1000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 500); Console.Beep(660, 500); Console.Beep(880, 1000); Console.Beep(838, 2000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 1000); Console.Beep(440, 1000); Console.Beep(419, 1000); Console.Beep(495, 1000); Console.Beep(660, 1000); Console.Beep(528, 1000); Console.Beep(594, 1000); Console.Beep(495, 1000); Console.Beep(528, 500); Console.Beep(660, 500); Console.Beep(880, 1000); Console.Beep(838, 2000);


            //    var dst = DateTime.Now.IsDaylightSavingTime();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Departamento>));

			if (false)
            {
                List<Thread> ts = new List<Thread>();
                for (int i = 0; i < 20; i++)
                {
                    var t = new Thread(() => RoboUnb.WorkerFilaDisciplina());
                    ts.Add(t);
                    t.Start();
                }

                var departamentos = RoboUnb.LerDepartamentos("https://wwwsec.serverweb.unb.br/matriculaweb/graduacao/oferta_dep.aspx?cod=1");

                ts.ForEach(n => n.Join());

                foreach (var item in departamentos)
                {
                    Console.WriteLine(item.Nome);
                    item.DisciplinasList = item.Disciplinas.ToList();
                }

                var sw = new System.IO.StringWriter();
                xmlSerializer.Serialize(sw, departamentos);
                string xml = sw.ToString();
                System.IO.File.WriteAllText("disciplinas.xml", xml);
            }
            else
            {
                var sr = new System.IO.StringReader(System.IO.File.ReadAllText("disciplinas.xml"));
                List<Departamento> depsDes = (List<Departamento>)xmlSerializer.Deserialize(sr);

                var sw2 = new System.IO.StringWriter();
                foreach (var item in depsDes)
                {

                    item.DisciplinasList.RemoveAll(n =>                         
                    !(n.Turmas.Any(k =>
                        {
								bool condicaovagas = k.Disponiveis > 0;
                            bool condicaoaulas = k.Aulas.Count > 0;
                            bool condicaohoras = (
                                // gambira: repita a condicao no loop mais abaixo
									k.Aulas.All(au => au.HoraInicio >= new TimeSpan(17,0,0) && au.Dia != DayOfWeek.Saturday));

                            return
                                condicaoaulas && condicaohoras && condicaovagas;
                        })));
                }

                foreach (var item in depsDes.Where(n => n.DisciplinasList.Any()))
                {
                    sw2.WriteLine(item.Nome);
                    foreach (var d in item.DisciplinasList)
                    {
						foreach (var t in d.Turmas.Where(k => 
							{
								bool condicaovagas = k.Disponiveis > 0;
								bool condicaoaulas = k.Aulas.Count > 0;
								bool condicaohoras = (
									// gambira: repita a condicao no loop mais abaixo
									k.Aulas.All(au => au.HoraInicio >= new TimeSpan(17,0,0) && au.Dia != DayOfWeek.Saturday));

								return
									condicaoaulas && condicaohoras && condicaovagas;
							}
						
						))
						{


							sw2.WriteLine("        " + d.Nome + "--" + t.Nome + " Disp:" + t.Disponiveis);
						}

                    }
                    sw2.WriteLine();
                    sw2.WriteLine();
                }
                System.IO.File.WriteAllText("resumo.txt", sw2.ToString());

                //var teste = depsDes.SelectMany(n => n.Disciplinas);

                //var disciplinasElegiveis = depsDes
                //    .SelectMany(n => n.Disciplinas)
                //    .Where(n => n.Turmas.Any(k => k.Aulas.All(aula =>
                //    (aula.Dia != DayOfWeek.Friday && aula.HoraInicio > new TimeSpan(20, 0, 0)) ||
                //    (aula.Dia == DayOfWeek.Friday && aula.HoraInicio > new TimeSpan(18, 0, 0)))));

                //var m = depsDes.Where(n => !n.Disciplinas.Any());
                //var ok = depsDes.Where(n => n.Disciplinas.Any(k => !k.Turmas.Any() || k.Turmas.Any(l => !l.Aulas.Any() || l.Aulas.Any(o => o.HoraFim == TimeSpan.Zero || o.HoraInicio == TimeSpan.Zero))));
            }

        }
    }
}
