using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Dynamic;
using System.Text.RegularExpressions;
using CodeMQ;
using System.Collections.Concurrent;

namespace VarreUnB
{
    [Capacity(99999)]
    public class FilaDisciplinas : BaseQueue<Tuple<string, string, ConcurrentBag<Disciplina>, string>>
    { }

    public static class RoboUnb
    {
        public static List<Departamento> LerDepartamentos(string urlCampus)
        {
            List<Departamento> deps = new List<Departamento>();
            ClienteWeb wc = new ClienteWeb();
            wc.Encoding = Encoding.UTF8;
            var html = wc.DownloadString(urlCampus);
            var dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(html);
            var navigator = dom.CreateNavigator();
            var tabela = navigator.Select("//table[@class='FrameCinza']");

            List<Tuple<Departamento, string, string>> deptoTemp = new List<Tuple<Departamento, string, string>>();


            while (tabela.MoveNext())
            {
                var trs = tabela.Current.Select(".//tr");
                trs.MoveNext();
                while (trs.MoveNext())
                {
                    var td = trs.Current.Select(".//td[3]");
                    td.MoveNext();
                    // estamos no TD que queremos
                    var url = td.Current.Select(".//a/@href");
                    url.MoveNext();
                    var nome = td.Current.Select(".//a/text()");
                    nome.MoveNext();

                    var dep = new Departamento();
                    dep.Nome = nome.Current.Value;
                    deptoTemp.Add(Tuple.Create(dep, urlCampus, url.Current.Value));
                }
            }

  /*          deptoTemp.RemoveAll(n =>
                !@"Departamento de Administração 
Departamento de Ciência da Computação  
Departamento de Economia 
Departamento de Engenharia Elétrica.
Departamento de Engenharia Mecânica 
Departamento de Estatística 
Departamento de Filosofia 
Departamento de Matemática. 
Departamento de Planejamento e Administração Departamento de Sociologia 
Departamento de Teoria e Fundamentos 
Depto de Ciências Contábeis e Atuariais 
Direção da Faculdade de Tecnologia 
Faculd. de Economia, Administração e Contabilidade 
Faculdade de Ciência da Informação 
Faculdade de Tecnologia
".Contains(n.Item1.Nome.Trim()));*/

            foreach (var depi in deptoTemp)
            {

                var dep = depi.Item1;
                Console.WriteLine("Lendo dpto. " + dep.Nome);
                dep.Disciplinas = LerDisciplinas(depi.Item2, depi.Item3, null);
            }

            var f = new FilaDisciplinas();
            f.Put(null);

            return deptoTemp.Select(n => n.Item1).ToList();
        }

        private static ConcurrentBag<Disciplina> LerDisciplinas(string urlCampus, string p, ClienteWeb wc)
        {
            var discs = new ConcurrentBag<Disciplina>();
            wc = new ClienteWeb();
            wc.Encoding = Encoding.UTF8;
            var newUrl = urlCampus.Substring(0, urlCampus.LastIndexOf("/") + 1) + p;
            string html;
            try
            {
                html = wc.DownloadString(newUrl);
            }
            catch (Exception)
            {
                return LerDisciplinas(urlCampus, p, wc);
            }
            var dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(html);
            var navigator = dom.CreateNavigator();
            var tabela = navigator.Select("//table[@class='FrameCinza']");
            while (tabela.MoveNext())
            {
                var trs = tabela.Current.Select(".//tr");
                trs.MoveNext();
                var f = new FilaDisciplinas();
                while (trs.MoveNext())
                {
                    var td = trs.Current.Select(".//td[2]");
                    td.MoveNext();
                    // estamos no TD que queremos
                    var url = td.Current.Select(".//a/@href");
                    url.MoveNext();
                    var nome = td.Current.Select(".//a/text()");
                    nome.MoveNext();

                    f.Put(Tuple.Create(urlCampus, url.Current.Value, discs, nome.Current.Value));
                }
            }

            return discs;
        }

        public static void WorkerFilaDisciplina()
        {
            var f = new FilaDisciplinas();
            Tuple<string, string, ConcurrentBag<Disciplina>, string> msg;
            ClienteWeb wc = new ClienteWeb();
            wc.Encoding = Encoding.UTF8;
            while ((msg = f.Get()) != null)
            {
                // f.Put(Tuple.Create(urlCampus, url.Current.Value, discs, nome.Current.Value));
                var dep = new Disciplina();
                dep.Nome = msg.Item4;
                var discs = msg.Item3;
                var urlCampus = msg.Item1;
                Console.WriteLine("\tLendo disc. " + dep.Nome);
                dep.Turmas = LerTurmas(urlCampus, msg.Item2, wc);
                Console.WriteLine("depth: " + CodeMQManager.GetQueueDepth(typeof(FilaDisciplinas)));
                discs.Add(dep);
            }

            f.Put(null);
        }

        private static List<Turma> LerTurmas(string urlCampus, string p, ClienteWeb wc)
        {
            var newUrl = urlCampus.Substring(0, urlCampus.LastIndexOf("/") + 1) + p;
            return LerTurmas2(wc, newUrl);
        }

        public static List<Turma> LerTurmas2(ClienteWeb wc, string newUrl)
        {
            var turmas = new List<Turma>();
            string html;
            try
            {
                html = wc.DownloadString(newUrl);
            }
            catch (Exception)
            {
                return LerTurmas2(wc, newUrl);
            }

            var dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(html);
            var navigator = dom.CreateNavigator();
            var tabela = navigator.Select("//table[@class='framecinza']");
            tabela.MoveNext();
            tabela.MoveNext();

            var trs = tabela.Current.Select(".//tr[@bgcolor='#FFFFFF']");
            while (trs.MoveNext())
            {
                if (!(trs.Current.Select(".//td").Count > 3))
                    continue;
                var nome = trs.Current.Select(".//td[1]/div/font/b/text()");
                nome.MoveNext();
                var turma = new Turma();
                turma.Nome = nome.Current.Value;


                var vagas = trs.Current.Select(".//td[2]");
                vagas.MoveNext();
                string text = vagas.Current.Value;

                var rgx = new Regex("\\d+");
                foreach (Match mt in rgx.Matches(text))
                {
                    if (turma.Total == null) turma.Total = int.Parse(mt.ToString());
                    else if (turma.Ocupadas == null) turma.Ocupadas = int.Parse(mt.ToString());
                    else if (turma.Disponiveis == null) turma.Disponiveis = int.Parse(mt.ToString());
                }

                turma.Aulas = new List<Aula>();

                var horas = trs.Current.Select(".//td[4]/div");
                while (horas.MoveNext())
                {
                    var dictSemana = new Dictionary<string, DayOfWeek>()
                    {
                        { "Segunda", DayOfWeek.Monday },
                        { "Terça", DayOfWeek.Tuesday },
                        { "Quarta", DayOfWeek.Wednesday },
                        { "Quinta", DayOfWeek.Thursday },
                        { "Sexta", DayOfWeek.Friday },
                        { "Sábado", DayOfWeek.Saturday },
                        { "Domingo", DayOfWeek.Sunday },
                    };

                    var diaSemana = horas.Current.Select("./b");
                    diaSemana.MoveNext();
                    string diaText = diaSemana.Current.Value;
                    // 1o font. com b dentro hora de inicio
                    var horaInicio = horas.Current.Select("./font[1]/b/text()");
                    horaInicio.MoveNext();
                    string horaIniciStr = horaInicio.Current.Value;
                    // 2o font. texto direto dentro hora de fim
                    var horaFim = horas.Current.Select(".//font[2]/text()");
                    horaFim.MoveNext();
                    string horaFimStr = horaFim.Current.Value;
                    turma.Aulas.Add(new Aula()
                    {
                        Dia = dictSemana[diaText],
                        HoraFim = TimeSpan.Parse(horaFimStr),
                        HoraInicio = TimeSpan.Parse(horaIniciStr)
                    });
                }

                turmas.Add(turma);
            }

            return turmas;
        }
    }
}
