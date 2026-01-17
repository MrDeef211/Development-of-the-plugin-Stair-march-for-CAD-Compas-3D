using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public class ErrorArgs : EventArgs
    {
        /// <summary>
        /// Переменные связанные с ошибкой
        /// </summary>
        private readonly List<ParametersTypes> _parametersList;
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        /// <param name="sizes">Переменные связанные с ошибкой</param>
        public ErrorArgs(
            string message, 
            List<ParametersTypes> parametersList)
        {
            _message = message;
            _parametersList = parametersList;
        }

        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        public string Message
        {
            get { return _message; }
        }
        /// <summary>
        /// Переменные связанные с ошибкой
        /// </summary>
        public List<ParametersTypes> ParametersList
        {
            get { return _parametersList; }
        }
    }
}
