using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Обработчик параметров
    /// </summary>
    public class Parameters
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

        /// <summary>
        /// Обработчик параметров
        /// </summary>
        public Parameters()
        {
            InitializeParameters();
        }

        /// <summary>
        /// Делегат события возникновения ошибки
        /// </summary>
        public event EventHandler<ErrorArgs> ErrorMessageEvent;

        /// <summary>
        /// Делегат события обновления параметра
        /// </summary>
        public event EventHandler<List<ParametersTypes>> UpdateParametersEvent;

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
        /// Событие обновления параметра на ошибки и значения
        /// </summary>
        /// <param name="parametersList">Список изменённых параметров</param>
        protected virtual void UpdateParameters(List<ParametersTypes> parametersList)
        {
            UpdateParametersEvent?.Invoke(this, parametersList);
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
            //Сначала фиксируем изменение
            _parameters[parameter].Value = value;
			UpdateParameters(new List<ParametersTypes> { parameter });
            Validate(_parameters[parameter]);

			//Запуск внутренних валидаций по необходимости
			switch (parameter)
            {
                case ParametersTypes.Length:
                case ParametersTypes.StepProjectionLength:
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
        /// Передаёт все параметры по событию обновления
        /// </summary>
        public void FullUpdateParameters()
        {
            UpdateParameters(_parameters.Keys.ToList());
        }

        /// <summary>
        /// Инициализирует параметры модели
        /// </summary>
        private void InitializeParameters()
        {
            _parameters = new Dictionary<ParametersTypes, Parameter>

            {

            {ParametersTypes.Height, new Parameter(ParametersTypes.Height, 8000, 500, 3200) },
            {ParametersTypes.Length, new Parameter(ParametersTypes.Length, 12000, 500, 5000) },
            {ParametersTypes.PlatformLengthUp, new Parameter(ParametersTypes.PlatformLengthUp, 5000, 1000, 1500) },
            {ParametersTypes.PlatformLengthDown, new Parameter(ParametersTypes.PlatformLengthDown, 5000, 1000, 1500) },
            {ParametersTypes.PlatformHeight, new Parameter(ParametersTypes.PlatformHeight, 500, 100, 200) },
            {ParametersTypes.StepAmount, new Parameter(ParametersTypes.StepAmount, 60, 1, 20) },
            {ParametersTypes.StepHeight, new Parameter(ParametersTypes.StepHeight, 250, 120, 160) },
            {ParametersTypes.StepProjectionHeight, new Parameter(ParametersTypes.StepProjectionHeight, 80, 0, 25) },
            {ParametersTypes.StepProjectionLength, new Parameter(ParametersTypes.StepProjectionLength, 80, 0, 20) },
            {ParametersTypes.Width, new Parameter(ParametersTypes.Width, 2500, 800, 1000)}

            };

        }

        /// <summary>
        /// Валидация параметра в рамках границ
        /// </summary>
        /// <param name="parameter">Параметр</param>
        /// <param name="value">Значение параметра</param>
        /// <exception cref="Exception">Ошибка валидации</exception>
        private void Validate(Parameter parameter)
        {
            double value = parameter.Value;

            //Проверка целочисленного параметра
            if (parameter.Name == ParametersTypes.StepAmount && Math.Truncate(value) != value)
            {
                ErrorMessage(" параметр должен быть целочисленным",
                    new List<ParametersTypes> { ParametersTypes.StepAmount });

                //throw new Exception("Количество ступеней: параметр должен быть целочисленным");

            }
            else 
            //Проверка границ
            if (value < parameter.Min || value > parameter.Max)
            {
                //Для случаев параметров с плавающей границей и целочисленного
                switch (parameter.Name)
                {
                    case ParametersTypes.StepProjectionHeight:
                    case ParametersTypes.StepProjectionLength:
                        ErrorMessage(": параметр не должен быть больше половины высоты ступени",
                            new List<ParametersTypes> { parameter.Name });
                        break;
                    case ParametersTypes.StepAmount:
                        ErrorMessage(": параметр не должен выходить за диапазон от " +
                            parameter.Min.ToString() + " до " + parameter.Max.ToString(),
                            new List<ParametersTypes> { parameter.Name });
                        break;
                    default:
                        ErrorMessage(": параметр не должен выходить за диапазон от " +
                            parameter.Min.ToString() + " до " + parameter.Max.ToString() + " мм",
                            new List<ParametersTypes> { parameter.Name });
                        //throw new Exception(parameter + ": параметр не должен выходить за диапазон от" +
                        //    parameter.Min.ToString() + "до" + parameter.Max.ToString() + "мм");
                        break;
                }
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
                    //Меняем значение на экране
                    _parameters[ParametersTypes.StepHeight].Value = newValue;
                    UpdateParameters(new List<ParametersTypes> { ParametersTypes.StepHeight });
					Validate(_parameters[ParametersTypes.StepHeight]);
					break;
                case ParametersTypes.StepHeight:
                    newValue = _parameters[ParametersTypes.StepHeight].Value *
                        _parameters[ParametersTypes.StepAmount].Value;

                    _parameters[ParametersTypes.Height].Value = newValue;
                    UpdateParameters(new List<ParametersTypes> { ParametersTypes.Height });
                    Validate(_parameters[ParametersTypes.Height]);
                    break;
            }

        }

        /// <summary>
        /// Внутренняя валидация между параметрами
        /// </summary>
        /// <param name="entered">Введённый параметр</param>
        private void InternalValidation(ParametersTypes entered)
        {
            // 1 Изменение границ глубины и толщины выступа
            if (entered == ParametersTypes.StepHeight)
            {
                // Глубина
                _parameters[ParametersTypes.StepProjectionLength].Max = _parameters[ParametersTypes.StepHeight].Value / 2;
                UpdateParameters(new List<ParametersTypes> { ParametersTypes.StepProjectionLength });
                Validate(_parameters[ParametersTypes.StepProjectionLength]);

                // Толщина
                _parameters[ParametersTypes.StepProjectionHeight].Max = _parameters[ParametersTypes.StepHeight].Value / 2;
                UpdateParameters(new List<ParametersTypes> { ParametersTypes.StepProjectionHeight });
                Validate(_parameters[ParametersTypes.StepProjectionHeight]);
            }

            // 2 Проверка угла наклона
            if (entered == ParametersTypes.Height || entered == ParametersTypes.Length)
            {
                _stairCorner = Math.Atan(_parameters[ParametersTypes.Height].Value / 
                    _parameters[ParametersTypes.Length].Value) * 180 / Math.PI;
                if (_stairCorner < 30 || _stairCorner > 50)
                    ErrorMessage("Угол лестницы: arctan(H / L) не должен выходить " +
                        "за диапазон от 30 до 50 градусов (сейчас " + _stairCorner + ")",
                        new List<ParametersTypes> { ParametersTypes.Height, ParametersTypes.Length});
            }

            // 3 Проверка глубины проступи
            if (entered == ParametersTypes.Length || entered == ParametersTypes.StepAmount || 
                entered == ParametersTypes.StepProjectionLength)
            {
                _stepsTread = _parameters[ParametersTypes.Length].Value /
                    _parameters[ParametersTypes.StepAmount].Value + _parameters[ParametersTypes.StepProjectionLength].Value;
                if (_stepsTread < 250 || _stepsTread > 400)
                    ErrorMessage("Глубина проступи: L / N + t  не должен выходить " +
                        "за диапазон от 250 до 400 мм (сейчас " + _stepsTread + ")",
                        new List<ParametersTypes> { ParametersTypes.StepAmount, 
                            ParametersTypes.Length, ParametersTypes.StepProjectionLength });
            }
        }




    }
}
