using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Параметр модели
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        private readonly ParametersTypes _name;
        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        private double _max;
        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        private double _min;
        /// <summary>
        /// Значение параметра
        /// </summary>
        private double _value;
        ///// <summary>
        ///// Параметр успешно прошёл валидацию
        ///// </summary>
        //private bool _isValid;

        /// <summary>
        /// Параметр модели
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="max">Максимальное значение параметра</param>
        /// <param name="min">Минимальное значение параметра</param>
        /// <param name="value">Значение параметра</param>
        public Parameter(
            ParametersTypes name, 
            double max, 
            double min, 
            double value)
        {
            _name = name;
            _max = max;
            _min = min;

            if (min >= max)
                throw new Exception(name +
                    " Минимальное значение параметра " +
                    "должно быть меньше максимального");
            if (value <= min || value >= max)
                throw new Exception(name + 
                    " Значение параметра не совпадает " +
                    "с выбранными границами");

            _value = value;
        }

        /// <summary>
        /// Имя параметра
        /// </summary>
        public ParametersTypes Name
        { 
            get { return _name; } 
        }
        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public double Max
        { 
            get { return _max; } 
            set {  _max = value; }

        }
        /// <summary>
        /// Значение параметра
        /// </summary>
        public double Min
        {
            get { return _min; }
        }
        /// <summary>
        /// Значение параметра
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
        ///// <summary>
        ///// Параметр успешно прошёл валидацию
        ///// </summary>
        //public bool IsValid
        //{
        //    get { return _isValid; }
        //    set { _isValid = value; }
        //}

    }
}
