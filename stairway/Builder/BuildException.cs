using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builders
{
    /// <summary>
    /// Исключение сборки модели
    /// </summary>
    public class BuildException : Exception
    {
        public BuildException(string message) : base(message)
        {
        }
    }
}
