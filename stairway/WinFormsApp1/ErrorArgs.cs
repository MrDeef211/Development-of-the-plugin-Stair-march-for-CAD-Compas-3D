using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stairway
{
    internal class ErrorArgs : EventArgs
    {
        public string Message { get; }
        public List<StairSizes> Sizes { get; }

        public ErrorArgs(string message, List<StairSizes> sizes)
        {
            Message = message;
            Sizes = sizes;
        }
    }
}
