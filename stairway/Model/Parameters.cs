using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
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
        /// Строить дополнительные секции
        /// </summary>
        private bool _isMultiFlight;

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
            InitializeNewParameters();
        }

        /// <summary>
        /// Делегат события возникновения ошибки
        /// </summary>
        public event EventHandler<ErrorArgs> ErrorMessageEvent;

        /// <summary>
        /// Делегат события обновления ошибок параметра
        /// </summary>
        public event EventHandler<ParametersTypes> UpdateParameterErrorsEvent;

        /// <summary>
        /// Делегат события обновления значения параметра
        /// </summary>
        public event EventHandler<ParametersTypes> UpdateParameterValueEvent;

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
			UpdateParameterErrors(parameter);
            Validate(_parameters[parameter]);

			//Запуск внутренних валидаций по необходимости
			switch (parameter)
            {
                //TODO: {}
                // Я не понял зачем здесь нужны фигурные скобки в RSDN их нет
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
                default:
                    break;
            }

        }

        /// <summary>
        /// Строить дополнительные секции
        /// </summary>
        public bool IsMultiFlight 
        {
            get 
            {
                return _isMultiFlight;
            } 
            set
            {
                _isMultiFlight = value; 
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
        /// Вернуть все данные класса
        /// </summary>
        /// <returns></returns>
        public ParametersSnapshot CreateSnapshot()
        {
            return new ParametersSnapshot
            {
                IsMultiFlight = IsMultiFlight,
                Values = _parameters.ToDictionary(
                    p => p.Key,
                    p => p.Value.Value
                )
            };
        }

        /// <summary>
        /// Загрузить все данные класса из снимка
        /// </summary>
        /// <param name="snapshot"></param>
        public void RestoreFromSnapshot(ParametersSnapshot snapshot)
        {
            IsMultiFlight = snapshot.IsMultiFlight;

            foreach (var (type, value) in snapshot.Values)
            {
                SetParameter(type, value);
            }
        }

        /// <summary>
        /// Передаёт все параметры по событию обновления
        /// </summary>
        public void FullUpdateParameters()
        {
            foreach (var parameter in _parameters.Keys.ToList())
            {
                UpdateParameterErrors(parameter);
                UpdateParameterValue(parameter);
            }
        }

        /// <summary>
        /// Событие отправки ошибки в MainForm
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parametersList"></param>
        private void ErrorMessage(
            string message,
            List<ParametersTypes> parametersList)
        {
            ErrorMessageEvent?.Invoke(
                this, new ErrorArgs(message, parametersList));
        }

        /// <summary>
        /// Событие обновления параметра на ошибки
        /// </summary>
        /// <param name="parametersList">Изменённый параметр</param>
        private void UpdateParameterErrors(
            ParametersTypes parameter)
        {
            UpdateParameterErrorsEvent?.Invoke(this, parameter);
        }

        /// <summary>
        /// Событие обновления значения параметра на форме
        /// </summary>
        /// <param name="parameters">Изменённый параметр</param>
        private void UpdateParameterValue(
            ParametersTypes parameters)
        {
            UpdateParameterValueEvent?.Invoke(this, parameters);
        }

        /// <summary>
        /// Инициализирует параметры модели
        /// </summary>
        private void InitializeNewParameters()
        {
            _parameters = new Dictionary<ParametersTypes, Parameter>
            {
                //TODO: отступы (сделал)
			    {ParametersTypes.Height,
                    new Parameter(
					    ParametersTypes.Height, 
                        8000, 
					    500, 
					    3200) },
                {ParametersTypes.Length, 
                    new Parameter(
					    ParametersTypes.Length, 
                        12000, 
						500, 
						5000) },
                {ParametersTypes.PlatformLengthUp, 
                    new Parameter(
					    ParametersTypes.PlatformLengthUp, 
                        5000, 
						1000, 
						1500) },
                {ParametersTypes.PlatformLengthDown, 
                    new Parameter(
					    ParametersTypes.PlatformLengthDown, 
                        5000, 
						1000, 
						1500) },
                {ParametersTypes.PlatformHeight, 
                    new Parameter(
					    ParametersTypes.PlatformHeight, 
                        500, 
						100, 
						200) },
                {ParametersTypes.StepAmount, 
                    new Parameter(
					    ParametersTypes.StepAmount, 
                        60, 
						1, 
						20) },
                {ParametersTypes.StepHeight, 
                    new Parameter(
					    ParametersTypes.StepHeight, 
                        250, 
						120, 
						160) },
                {ParametersTypes.StepProjectionHeight, 
                    new Parameter(
					    ParametersTypes.StepProjectionHeight, 
						80, 
						0, 
						25) },
                {ParametersTypes.StepProjectionLength, 
                    new Parameter(
						ParametersTypes.StepProjectionLength, 
						80, 
						0, 
						20) },
                {ParametersTypes.Width, 
                    new Parameter(
						ParametersTypes.Width, 
						2500, 
						800, 
						1000)},
                {ParametersTypes.FloorsCount,
                    new Parameter(
						ParametersTypes.FloorsCount,
						25, 
						0, 
						1)}
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
            if ((parameter.Name == ParametersTypes.StepAmount ||
                parameter.Name == ParametersTypes.FloorsCount) && 
                Math.Truncate(value) != value)
            {
                ErrorMessage(" параметр должен быть целочисленным",
                    new List<ParametersTypes> { parameter.Name });

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
                        ErrorMessage(
                            ": параметр не должен быть " +
                            "больше половины высоты ступени",
                            new List<ParametersTypes> { parameter.Name });
                        break;
                    case ParametersTypes.StepAmount:
                        ErrorMessage(
                            ": параметр не должен выходить за диапазон от " +
                            parameter.Min.ToString() + " до " + 
                            parameter.Max.ToString(),
                            new List<ParametersTypes> { parameter.Name });
                        break;
                    default:
                        ErrorMessage(
                            ": параметр не должен выходить за диапазон от " +
                            parameter.Min.ToString() + " до " + 
                            parameter.Max.ToString() + " мм",
                            new List<ParametersTypes> { parameter.Name });
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
                    newValue = 
                        _parameters[ParametersTypes.Height].Value /
                        _parameters[ParametersTypes.StepAmount].Value;

                    // Обновляем параметр
                    _parameters[ParametersTypes.StepHeight].Value = newValue;
                    UpdateParameterValue(ParametersTypes.StepHeight);

                    // Валидируем отдельно, чтобы не создавать рекурсию
                    UpdateParameterErrors(ParametersTypes.StepHeight);
					Validate(_parameters[ParametersTypes.StepHeight]);
                    InternalValidation(ParametersTypes.StepHeight);
					break;
                case ParametersTypes.StepHeight:
                    newValue = 
                        _parameters[ParametersTypes.StepHeight].Value *
                        _parameters[ParametersTypes.StepAmount].Value;

                    _parameters[ParametersTypes.Height].Value = newValue;
                    UpdateParameterValue(ParametersTypes.Height);

                    UpdateParameterErrors(ParametersTypes.Height);
                    Validate(_parameters[ParametersTypes.Height]);
                    InternalValidation(ParametersTypes.Height);
                    break;
            }

        }

        /// <summary>
        /// Внутренняя валидация между параметрами
        /// </summary>
        /// <param name="entered">Введённый параметр</param>
        private void InternalValidation(ParametersTypes entered)
        {
            // Часть ступени, которую не должен превышать выступ
            double stepBorder = 2;

            // Граница угла лестницы
            double maxStairCorner = 50;
            double minStairCorner = 30;

            // Граница глубины проступи
            double maxStepsTread = 400;
            double minStepsTread = 250;

            // 1 Изменение границ глубины и толщины выступа
            if (entered == ParametersTypes.StepHeight)
            {
                // Новые границы для параметров выступа
                // Глубина не больше части высоты ступени
                _parameters[ParametersTypes.StepProjectionLength].Max = 
                    _parameters[ParametersTypes.StepHeight].Value / 
                    stepBorder;

                // Валидируем внутри новых границ
                UpdateParameterErrors(ParametersTypes.StepProjectionLength);
                Validate(_parameters[ParametersTypes.StepProjectionLength]);

                // Толщина не больше половины высоты ступени
                _parameters[ParametersTypes.StepProjectionHeight].Max = 
                    _parameters[ParametersTypes.StepHeight].Value / 
                    stepBorder;

                // Валидируем внутри новых границ
                UpdateParameterErrors(ParametersTypes.StepProjectionHeight);
                Validate(_parameters[ParametersTypes.StepProjectionHeight]);
            }

            // 2 Проверка угла наклона
            if (entered == ParametersTypes.Height || 
                entered == ParametersTypes.Length)
            {
                _stairCorner = 
                    Math.Atan(_parameters[ParametersTypes.Height].Value / 
                    _parameters[ParametersTypes.Length].Value) * 180 / Math.PI;

                if (_stairCorner < minStairCorner || 
                    _stairCorner > maxStairCorner)
                    ErrorMessage(
                        "Угол лестницы: arctan(H / L) " +
                        "не должен выходить за диапазон от " +
                        minStairCorner + " до " + maxStairCorner +  
                        " градусов (сейчас " + _stairCorner + ")",
                        new List<ParametersTypes> { 
                            ParametersTypes.Height, 
                            ParametersTypes.Length});
            }

            // 3 Проверка глубины проступи
            if (entered == ParametersTypes.Length || 
                entered == ParametersTypes.StepAmount || 
                entered == ParametersTypes.StepProjectionLength)
            {
                _stepsTread = _parameters[ParametersTypes.Length].Value /
                    _parameters[ParametersTypes.StepAmount].Value + 
                    _parameters[ParametersTypes.StepProjectionLength].Value;

                if (_stepsTread < minStepsTread || 
                    _stepsTread > maxStepsTread)
                    ErrorMessage(
                        "Глубина проступи: L / N + t  " +
                        "не должен выходить за диапазон от " +
                        minStepsTread + " до " + maxStepsTread + 
                        " мм (сейчас " + _stepsTread + ")",
                        new List<ParametersTypes> { 
                            ParametersTypes.StepAmount, 
                            ParametersTypes.Length, 
                            ParametersTypes.StepProjectionLength });
            }
        }
    }
}
