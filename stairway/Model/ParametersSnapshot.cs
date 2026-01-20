using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParametersSnapshot
    {
        /// <summary>
        /// Список параметров
        /// </summary>
        public Dictionary<ParametersTypes, double> Values { get; set; }
        /// <summary>
        /// Многоэтажность
        /// </summary>
        public bool IsMultiFlight { get; set; }
    }
}
