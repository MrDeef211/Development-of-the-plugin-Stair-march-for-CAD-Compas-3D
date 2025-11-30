using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace stairway
{
    /// <summary>
    /// Обработчик параметров
    /// </summary>
    internal class Parameters
    {
        /// <summary>
        /// Список вводимых параметров
        /// </summary>
        private Dictionary<ParametersTypes, Parameter> _parameters;
        /// <summary>
        /// Угол лестницы в градусах
        /// </summary>
        private double _stairCorner;
        /// <summary>
        /// Длина проступи ступени
        /// </summary>
        private double _stepsTread;


        public Parameters()
        {
            
        }

        /// <summary>
        /// Делегат события возникновения ошибки
        /// </summary>
        public event EventHandler<ErrorArgs> ErrorMessageEvent;

        /// <summary>
        /// Событие отправки ошибки в MainForm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parametersList"></param>
        protected virtual void ErrorMessage(string message, List<ParametersTypes> parametersList)
        {
            ErrorMessageEvent?.Invoke(this, new ErrorArgs(message, parametersList));
        }

        /// <summary>
        /// Вернуть значение параметра
        /// </summary>
        /// <param name="parameter">Возвращаемый параметр</param>
        /// <returns></returns>
        public double GetParameter(ParametersTypes parameter)
        {
            return _parameters[parameter].Value;
        }

        /// <summary>
        /// Установить значение параметра
        /// </summary>
        /// <param name="parameter">Изменяемый параметр</param>
        /// <param name="value">Новое значение параметра</param>
        public void SetParameter(ParametersTypes parameter, double value)
        {
            Validate(_parameters[parameter], value);
            _parameters[parameter].Value = value;

            //Запуск внутренних валидаций по необходимости
            switch (parameter)
            {
                case ParametersTypes.Length:
                    InternalValidation(parameter);
                    break;
                case ParametersTypes.StepAmount:
                case ParametersTypes.Height:
                case ParametersTypes.StepHeight:
                    CalculateDependent(parameter);
                    InternalValidation(parameter);
                    break;
            }

        }

        /// <summary>
        /// Вернуть список параметров
        /// </summary>
        /// <returns>Список параметров</returns>
        public Dictionary<ParametersTypes, Parameter> GetParameters()
        {
            return _parameters;
        }

        /// <summary>
        /// Валидация параметра в рамках границ
        /// </summary>
        /// <param name="parameter">Параметр</param>
        /// <param name="value">Значение параметра</param>
        /// <exception cref="Exception">Ошибка валидации</exception>
        private void Validate(Parameter parameter, double value)
        {
            //Проверка целочисленного параметра
            if (parameter.Name == ParametersTypes.StepAmount && Math.Truncate(value) != value)
            {
                ErrorMessage("Количество ступеней: параметр должен быть целочисленным",
                    new List<ParametersTypes> { ParametersTypes.StepAmount });
                throw new Exception("Количество ступеней: параметр должен быть целочисленным");

            }
            //Проверка границ
            if (value < parameter.Min || value > parameter.Max)
            {
                ErrorMessage("параметр не должен выходить за диапазон от" +
                    parameter.Min.ToString() + "до" + parameter.Max.ToString() + "мм",
                    new List<ParametersTypes> { parameter.Name });
                throw new Exception(parameter + ": параметр не должен выходить за диапазон от" +
                    parameter.Min.ToString() + "до" + parameter.Max.ToString() + "мм");
            }

        }

        /// <summary>
        /// Вычисление взаимосвязанных переменных
        /// </summary>
        /// <param name="entered">Введённый параметр</param>
        private void CalculateDependent(ParametersTypes entered)
        {
            double newValue;
            switch (entered)
            {
                case ParametersTypes.StepAmount:
                case ParametersTypes.Height:
                    newValue = _parameters[ParametersTypes.Height].Value /
                        _parameters[ParametersTypes.StepAmount].Value;
                    /* Сначала ставим новое значение, чтобы его можно было вывести на экран
                    Пользователь должен понимать откуда появилась ошибка */
                    _parameters[ParametersTypes.StepHeight].Value = newValue;
                    //Проверяем полученное значение
                    Validate(_parameters[ParametersTypes.StepHeight], newValue);
                    break;
                case ParametersTypes.StepHeight:
                    newValue = _parameters[ParametersTypes.StepHeight].Value *
                        _parameters[ParametersTypes.StepAmount].Value;

                    _parameters[ParametersTypes.Height].Value = newValue;
                    Validate(_parameters[ParametersTypes.StepHeight], newValue);
                    break;
            }

        }

        /// <summary>
        /// Внутренняя валидация между параметрами
        /// </summary>
        private void InternalValidation(ParametersTypes entered)
        {
            // 1 Изменение границ глубины и толщины выступа
            if (entered == ParametersTypes.StepHeight)
            {
                // Глубина
                _parameters[ParametersTypes.StepProjectionLength].Max = _parameters[ParametersTypes.StepHeight].Value / 2;
                if (_parameters[ParametersTypes.StepProjectionLength].Value > 
                    _parameters[ParametersTypes.StepProjectionLength].Max)
                    ErrorMessage("Высота ступени должна быть не меньше чем две длины выступа", 
                        new List<ParametersTypes> { ParametersTypes.StepProjectionLength, ParametersTypes.StepHeight });
                // Толщина
                _parameters[ParametersTypes.StepProjectionHeight].Max = _parameters[ParametersTypes.StepHeight].Value / 2;
                if (_parameters[ParametersTypes.StepProjectionHeight].Value > 
                    _parameters[ParametersTypes.StepProjectionHeight].Max)
                    ErrorMessage("Высота ступени должна быть не меньше чем две высоты выступа", 
                        new List<ParametersTypes> { ParametersTypes.StepProjectionHeight, ParametersTypes.StepHeight });
            }

            // 2 Проверка угла наклона
            if (entered == ParametersTypes.Height || entered == ParametersTypes.Length)
            {
                _stairCorner = Math.Atan(_parameters[ParametersTypes.Height].Value / 
                    _parameters[ParametersTypes.Length].Value) * 180 / Math.PI;
                if (_stairCorner < 30 || _stairCorner > 50)
                    ErrorMessage("Угол лестницы: arctan(H / L) не должен выходить за диапазон от 30 до 50 градусов",
                        new List<ParametersTypes> { ParametersTypes.Height, ParametersTypes.Length});
            }

            // 3 Проверка глубины проступи
            if (entered == ParametersTypes.Length || entered == ParametersTypes.StepAmount || 
                entered == ParametersTypes.StepProjectionLength)
            {
                _stepsTread = _parameters[ParametersTypes.Length].Value /
                    _parameters[ParametersTypes.StepAmount].Value + _parameters[ParametersTypes.StepProjectionLength].Value;
                if (_stairCorner < 250 || _stairCorner > 400)
                    ErrorMessage("Глубина проступи: L / N + t  не должен выходить за диапазон от 250 до 400 мм",
                        new List<ParametersTypes> { ParametersTypes.StepAmount, 
                            ParametersTypes.Length, ParametersTypes.StepProjectionLength });
            }
        }




    }
}
