using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stairway
{
    internal class Parameters
    {
        public event EventHandler<ErrorArgs> ErrorMessageEvent;

        protected virtual void ErrorMessage(string message, List<StairSizes> sizes)
        {
            ErrorMessageEvent?.Invoke(this, new ErrorArgs(message, sizes));
        }

    }
}
