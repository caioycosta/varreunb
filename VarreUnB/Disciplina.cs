using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VarreUnB
{
    public class Disciplina
    {
        public string Nome { get; set; }
        public List<Turma> Turmas { get; set; }

        public override string ToString()
        {
            return String.Format("[ {0} ]", Nome);
        }
    }
}
