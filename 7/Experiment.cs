using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract7.database
{
    public class Experiment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string UsedMaterial { get; set; }
        public string Result { get; set; }
    }
}
