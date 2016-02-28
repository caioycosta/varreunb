using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VarreUnB
{
    public class Aula
    {
        public DayOfWeek Dia { get; set; }
        [XmlIgnore]
        public TimeSpan HoraInicio { get; set; }
        [XmlIgnore]
        public TimeSpan HoraFim { get; set; }

        public string HoraInicioStr
        {
            get
            {
                return HoraInicio.ToString();
            }
            set
            {
                HoraInicio = TimeSpan.Parse(value);
            }
        }
        public string HoraFimStr
        {
            get
            {
                return HoraFim.ToString();
            }
            set
            {
                HoraFim = TimeSpan.Parse(value);
            }
        }
    }
}
