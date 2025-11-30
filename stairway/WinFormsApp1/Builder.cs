using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stairway
{
    /// <summary>
    /// Строитель модели
    /// </summary>
    internal class Builder
    {
        //private Wrapper _wrapper;

        /// <summary>
        /// Строитель модели
        /// </summary>
        public Builder() 
        {
            
        }

        /// <summary>
        /// Построить модель
        /// </summary>
        /// <param name="parameters">Параметры модели</param>
        public void Build(Dictionary<ParametersTypes, Parameter> parameters)
        {

        }

        /// <summary>
        /// Создать платформы
        /// </summary>
        /// <param name="PalatformLengthUp">Длина верхней платформы</param>
        /// <param name="PalatformLengthDown">Длина нижней платформы</param>
        /// <param name="PalatformHeight">Ширина платформы</param>
        private void CreatePlatforms(double PalatformLengthUp, double PalatformLengthDown, double PalatformHeight)
        {

        }

        /// <summary>
        /// Создать ступеньки
        /// </summary>
        /// <param name="stepAmount">Количество ступеней</param>
        /// <param name="stepHeight">Высота ступени</param>
        /// <param name="stepProjectionLength">Глубина выступа</param>
        /// <param name="stepProjectionHenght">Высота выступа</param>
        private void CreateStair(int stepAmount, double stepHeight, 
            double stepProjectionLength, double stepProjectionHenght)
        {

        }
    }
}
