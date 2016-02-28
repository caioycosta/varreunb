using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VarreUnB
{
    public class Turma
    {
        public List<Aula> Aulas { get; set; }
        public string Nome { get; set; }
        public int? Total { get; set; }
        public int? Ocupadas { get; set; }
        public int? Disponiveis { get; set; }
    }
}
