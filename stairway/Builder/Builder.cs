using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Builders
{
    /// <summary>
    /// Строитель модели
    /// </summary>
    public class Builder
    {
        private Wrapper _wrapper;

        /// <summary>
        /// Строитель модели
        /// </summary>
        public Builder() 
        {
            _wrapper = new Wrapper();
        }

        /// <summary>
        /// Построить модель
        /// </summary>
        /// <param name="parameters">Параметры модели</param>
        public void Build(Dictionary<ParametersTypes, double> parameters)
        {
            if (!_wrapper.KompasIsDefined())
                _wrapper.CreateCADWindow();

            _wrapper.CreateFile();
            _wrapper.CreateSketch();

            CreatePlatforms(
                parameters[ParametersTypes.PlatformLengthUp],
                parameters[ParametersTypes.PlatformLengthDown],
                parameters[ParametersTypes.PlatformHeight],
                parameters[ParametersTypes.Height],
                parameters[ParametersTypes.Length]);
            CreateStair(
                (int)parameters[ParametersTypes.StepAmount],
                parameters[ParametersTypes.StepHeight],
                parameters[ParametersTypes.StepProjectionLength],
                parameters[ParametersTypes.StepProjectionHeight],
                parameters[ParametersTypes.Height],
                parameters[ParametersTypes.Length]);


            _wrapper.Extrusion(true, 0, parameters[ParametersTypes.Width], false);
        }

        /// <summary>
        /// Создать платформы
        /// </summary>
        /// <param name="platformLengthUp">Длина верхней платформы</param>
        /// <param name="platformLengthDown">Длина нижней платформы</param>
        /// <param name="platformHeight">Ширина платформы</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        private void CreatePlatforms(
            double platformLengthUp, 
            double platformLengthDown, 
            double platformHeight,
            double height,
            double length)
        {
            _wrapper.Createline(
                0 - length / 2,
                0 - height / 2,
                0 - length / 2 - platformLengthUp,
                0 - height / 2);
            _wrapper.Createline(
                0 - length / 2 - platformLengthUp,
                0 - height / 2,
                0 - length / 2 - platformLengthUp,
                0 - height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2 - platformLengthUp,
                0 - height / 2 + platformHeight,
                0 - length / 2,
                0 - height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2,
                0 - height / 2 + platformHeight,
                length / 2,
                height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2,
                height / 2 + platformHeight,
                length / 2 + platformLengthDown,
                height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2 + platformLengthDown,
                height / 2 + platformHeight,
                length / 2 + platformLengthDown,
                height / 2);
            _wrapper.Createline(
                length / 2 + platformLengthDown,
                height / 2,
                length / 2,
                height / 2);
        }

        /// <summary>
        /// Создать ступеньки
        /// </summary>
        /// <param name="stepAmount">Количество ступеней</param>
        /// <param name="stepHeight">Высота ступени</param>
        /// <param name="stepProjectionLength">Глубина выступа</param>
        /// <param name="stepProjectionHenght">Высота выступа</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        private void CreateStair(
            int stepAmount, 
            double stepHeight, 
            double stepProjectionLength, 
            double stepProjectionHenght,
            double height,
            double length)
        {
            double stepLength = length / stepAmount;

            for (int i = 0; i < stepAmount - 1; i++)
            {
                _wrapper.Createline(
                    length / 2 - i * stepLength,
                    height / 2 - i * stepHeight,
                    length / 2 - i * stepLength,
                    height / 2 - (i + 1) * stepHeight + stepProjectionHenght);

                _wrapper.Createline(
                    length / 2 - i * stepLength,
                    height / 2 - (i + 1) * stepHeight + stepProjectionHenght,
                    length / 2 - i * stepLength + stepProjectionLength,
                    height / 2 - (i + 1) * stepHeight + stepProjectionHenght);

                _wrapper.Createline(
                    length / 2 - i * stepLength + stepProjectionLength,
                    height / 2 - (i + 1) * stepHeight + stepProjectionHenght,
                    length / 2 - i * stepLength + stepProjectionLength,
                    height / 2 - (i + 1) * stepHeight);

                _wrapper.Createline(
                    length / 2 - i * stepLength + stepProjectionLength,
                    height / 2 - (i + 1) * stepHeight,
                    length / 2 - (i + 1) * stepLength,
                    height / 2 - (i + 1) * stepHeight);
            }
            //Последнюю ступеньку рисуем вручную, чтобы устранить накопительные погрешности

            _wrapper.Createline(
                length / 2 - (stepAmount - 1) * stepLength,
                height / 2 - (stepAmount - 1) * stepHeight,
                length / 2 - (stepAmount - 1) * stepLength,
                0 - height / 2 + stepProjectionHenght);

            _wrapper.Createline(
                length / 2 - (stepAmount - 1) * stepLength,
                0 - height / 2 + stepProjectionHenght,
                0 - length / 2 + stepProjectionLength + stepLength,
                0 - height / 2 + stepProjectionHenght);

            _wrapper.Createline(
                0 - length / 2 + stepProjectionLength + stepLength,
                0 - height / 2 + stepProjectionHenght,
                0 - length / 2 + stepProjectionLength + stepLength,
                0 - height / 2);

            _wrapper.Createline(
                0 - length / 2 + stepProjectionLength + stepLength,
                0 - height / 2,
                0 - length / 2,
                0 - height / 2);
        }
    }
}
