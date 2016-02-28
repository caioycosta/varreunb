using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace VarreUnB
{
    public class Departamento
    {
        public string Nome { get; set; }

        internal ConcurrentBag<Disciplina> Disciplinas;

        public List<Disciplina> DisciplinasList { get; set; }
    }
}
