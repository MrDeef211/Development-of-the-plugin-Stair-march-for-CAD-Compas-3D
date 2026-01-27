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
        //TODO: RSDN (вроде сделал)
        /// <summary>
        /// Класс-обёртка для работы с САПР
        /// </summary>
        private Wrapper _wrapper;

        /// <summary>
        /// Текущий этаж
        /// </summary>
        private int _currentFloor;

        /// <summary>
        /// Строитель модели
        /// </summary>
        public Builder() 
        {
            _wrapper = new Wrapper();
        }

        /// <summary>
        /// Закрыть текущий документ
        /// </summary>
        public void CloseDocument()
        {
            _wrapper.CloseCurrentFile();
        }

        /// <summary>
        /// Построить модель
        /// </summary>
        /// <param name="parameters">Параметры модели</param>
        public void Build(Dictionary<ParametersTypes, double> parameters, bool IsMultiFlight)
        {
            //TODO: {} (исправил)
            if (!_wrapper.KompasIsDefined())
            { 
            _wrapper.CreateCADWindow();
            }

            _wrapper.CreateFile();

            _currentFloor = 0;

            int floorCount = (int)parameters[ParametersTypes.FloorsCount];

            if (IsMultiFlight)
            {
                for (_currentFloor = 0; _currentFloor < floorCount; _currentFloor++)
                {
                    _wrapper.CreateSketch();

                    CreateMainPlatforms(
                        parameters[ParametersTypes.PlatformLengthUp],
                        parameters[ParametersTypes.PlatformLengthDown],
                        parameters[ParametersTypes.PlatformHeight],
                        parameters[ParametersTypes.Height],
                        parameters[ParametersTypes.Length]);
                    CreateMainStair(
                        (int)parameters[ParametersTypes.StepAmount],
                        parameters[ParametersTypes.StepHeight],
                        parameters[ParametersTypes.StepProjectionLength],
                        parameters[ParametersTypes.StepProjectionHeight],
                        parameters[ParametersTypes.Height],
                        parameters[ParametersTypes.Length]);

                    _wrapper.Extrusion(
                        true,
                        0,
                        parameters[ParametersTypes.Width],
                        false);

                    _wrapper.CreateSketch();

                    CreateSidePlatforms(
                        parameters[ParametersTypes.PlatformLengthUp],
                        parameters[ParametersTypes.PlatformLengthDown],
                        parameters[ParametersTypes.PlatformHeight],
                        parameters[ParametersTypes.Height],
                        parameters[ParametersTypes.Length]);
                    CreateSideStair(
                        (int)parameters[ParametersTypes.StepAmount],
                        parameters[ParametersTypes.StepHeight],
                        parameters[ParametersTypes.StepProjectionLength],
                        parameters[ParametersTypes.StepProjectionHeight],
                        parameters[ParametersTypes.Height],
                        parameters[ParametersTypes.Length]);

                    _wrapper.Extrusion(
                        false,
                        0,
                        parameters[ParametersTypes.Width],
                        false);
                }
            }
            else
            {
                _wrapper.CreateSketch();

                CreateMainPlatforms(
                    parameters[ParametersTypes.PlatformLengthUp],
                    parameters[ParametersTypes.PlatformLengthDown],
                    parameters[ParametersTypes.PlatformHeight],
                    parameters[ParametersTypes.Height],
                    parameters[ParametersTypes.Length]);
                CreateMainStair(
                    (int)parameters[ParametersTypes.StepAmount],
                    parameters[ParametersTypes.StepHeight],
                    parameters[ParametersTypes.StepProjectionLength],
                    parameters[ParametersTypes.StepProjectionHeight],
                    parameters[ParametersTypes.Height],
                    parameters[ParametersTypes.Length]);

                _wrapper.Extrusion(
                    true,
                    0,
                    parameters[ParametersTypes.Width],
                    false);
            }

            _wrapper.DocumentZoomOut();
        }

        /// <summary>
        /// Создать платформы
        /// </summary>
        /// <param name="platformLengthUp">Длина верхней платформы</param>
        /// <param name="platformLengthDown">Длина нижней платформы</param>
        /// <param name="platformHeight">Ширина платформы</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        private void CreateMainPlatforms(
            double platformLengthUp, 
            double platformLengthDown, 
            double platformHeight,
            double height,
            double length)
        {
            // 2 - коофициент чётности
            double h = 2 * height * _currentFloor;

            _wrapper.Createline(
                0 - length / 2,
                h + 0 - height / 2,
                0 - length / 2 - platformLengthUp,
                h + 0 - height / 2);
            _wrapper.Createline(
                0 - length / 2 - platformLengthUp,
                h + 0 - height / 2,
                0 - length / 2 - platformLengthUp,
                h + 0 - height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2 - platformLengthUp,
                h + 0 - height / 2 + platformHeight,
                0 - length / 2,
                h + 0 - height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2,
                h + 0 - height / 2 + platformHeight,
                length / 2,
                h + height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2,
                h + height / 2 + platformHeight,
                length / 2 + platformLengthDown,
                h + height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2 + platformLengthDown,
                h + height / 2 + platformHeight,
                length / 2 + platformLengthDown,
                h + height / 2);
            _wrapper.Createline(
                length / 2 + platformLengthDown,
                h + height / 2,
                length / 2,
                h + height / 2);
        }

        /// <summary>
        /// Создать платформы вторых лестниц
        /// </summary>
        /// <param name="platformLengthUp"></param>
        /// <param name="platformLengthDown"></param>
        /// <param name="platformHeight"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        private void CreateSidePlatforms(
            double platformLengthUp,
            double platformLengthDown,
            double platformHeight,
            double height,
            double length)
        {
            // 2 - коофициент чётности
            double h = 2 * height * _currentFloor + height;

            _wrapper.Createline(
                length / 2, 
                h + 0 - height / 2,
                length / 2 + platformLengthUp,  
                h + 0 - height / 2);
            _wrapper.Createline(
                length / 2 + platformLengthUp,
                h + 0 - height / 2,
                length / 2 + platformLengthUp,  
                h + 0 - height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2 + platformLengthUp, 
                h + 0 - height / 2 + platformHeight,
                length / 2,  
                h + 0 - height / 2 + platformHeight);
            _wrapper.Createline(
                length / 2,  
                h + 0 - height / 2 + platformHeight,
                0 - length / 2, 
                h + height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2,  
                h + height / 2 + platformHeight,
                0 - length / 2 - platformLengthDown,  
                h + height / 2 + platformHeight);
            _wrapper.Createline(
                0 - length / 2 - platformLengthDown, 
                h + height / 2 + platformHeight,
                0 - length / 2 - platformLengthDown,  
                h + height / 2);
            _wrapper.Createline(
                0 - length / 2 - platformLengthDown,  
                h + height / 2,
                0 - length / 2,  
                h + height / 2);
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
        private void CreateMainStair(
            int stepAmount, 
            double stepHeight, 
            double stepProjectionLength, 
            double stepProjectionHeight,
            double height,
            double length)
        {
            // 2 - коэффициент чётности
            double h = 2 * height * _currentFloor;
            double stepLength = length / stepAmount;

            for (int i = 0; i < stepAmount - 1; i++)
            {
                _wrapper.Createline(
                    length / 2 - i * stepLength,
                    h + height / 2 - i * stepHeight,
                    length / 2 - i * stepLength,
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight);

                _wrapper.Createline(
                    length / 2 - i * stepLength,
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight,
                    length / 2 - i * stepLength + stepProjectionLength,
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight);

                _wrapper.Createline(
                    length / 2 - i * stepLength + stepProjectionLength,
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight,
                    length / 2 - i * stepLength + stepProjectionLength,
                    h + height / 2 - (i + 1) * stepHeight);

                _wrapper.Createline(
                    length / 2 - i * stepLength + stepProjectionLength,
                    h + height / 2 - (i + 1) * stepHeight,
                    length / 2 - (i + 1) * stepLength,
                    h + height / 2 - (i + 1) * stepHeight);
            }
            //Последнюю ступеньку рисуем вручную,
            //чтобы устранить накопительные погрешности

            _wrapper.Createline(
                length / 2 - (stepAmount - 1) * stepLength,
                h + height / 2 - (stepAmount - 1) * stepHeight,
                length / 2 - (stepAmount - 1) * stepLength,
                h + 0 - height / 2 + stepProjectionHeight);

            _wrapper.Createline(
                length / 2 - (stepAmount - 1) * stepLength,
                h + 0 - height / 2 + stepProjectionHeight,
                0 - length / 2 + stepProjectionLength + stepLength,
                h + 0 - height / 2 + stepProjectionHeight);

            _wrapper.Createline(
                0 - length / 2 + stepProjectionLength + stepLength,
                h + 0 - height / 2 + stepProjectionHeight,
                0 - length / 2 + stepProjectionLength + stepLength,
                h + 0 - height / 2);

            _wrapper.Createline(
                0 - length / 2 + stepProjectionLength + stepLength,
                h + 0 - height / 2,
                0 - length / 2,
                h + 0 - height / 2);
        }

        /// <summary>
        /// Создать ступеньки вторых лестниц
        /// </summary>
        /// <param name="stepAmount">Количество ступеней</param>
        /// <param name="stepHeight">Высота ступени</param>
        /// <param name="stepProjectionLength">Глубина выступа</param>
        /// <param name="stepProjectionHenght">Высота выступа</param>
        /// <param name="height">Высота</param>
        /// <param name="length">Длина</param>
        private void CreateSideStair(
            int stepAmount,
            double stepHeight,
            double stepProjectionLength,
            double stepProjectionHeight,
            double height,
            double length)
        {
            // 2 - коэффициент чётности
            double h = 2 * height * _currentFloor + height;
            double stepLength = length / stepAmount;

            for (int i = 0; i < stepAmount - 1; i++)
            {
                _wrapper.Createline(
                    0 - length / 2 + i * stepLength,  
                    h + height / 2 - i * stepHeight,
                    0 - length / 2 + i * stepLength, 
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight);

                _wrapper.Createline(
                    0 - length / 2 + i * stepLength,  
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight,
                    0 - length / 2 + i * stepLength - stepProjectionLength, 
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight);

                _wrapper.Createline(
                    0 - length / 2 + i * stepLength - stepProjectionLength, 
                    h + height / 2 - (i + 1) * stepHeight + stepProjectionHeight,
                    0 - length / 2 + i * stepLength - stepProjectionLength, 
                    h + height / 2 - (i + 1) * stepHeight);

                _wrapper.Createline(
                    0 - length / 2 + i * stepLength - stepProjectionLength,  
                    h + height / 2 - (i + 1) * stepHeight,
                    0 - length / 2 + (i + 1) * stepLength, 
                    h + height / 2 - (i + 1) * stepHeight);
            }

            // Последнюю ступеньку
            _wrapper.Createline(
                0 - length / 2 + (stepAmount - 1) * stepLength,
                h + height / 2 - (stepAmount - 1) * stepHeight,
                0 - length / 2 + (stepAmount - 1) * stepLength,
                h + 0 - height / 2 + stepProjectionHeight);

            _wrapper.Createline(
                0 - length / 2 + (stepAmount - 1) * stepLength,  
                h + 0 - height / 2 + stepProjectionHeight,
                length / 2 - stepProjectionLength - stepLength, 
                h + 0 - height / 2 + stepProjectionHeight);

            _wrapper.Createline(
                length / 2 - stepProjectionLength - stepLength,
                h + 0 - height / 2 + stepProjectionHeight,
                length / 2 - stepProjectionLength - stepLength, 
                h + 0 - height / 2);

            _wrapper.Createline(
                length / 2 - stepProjectionLength - stepLength,  
                h + 0 - height / 2,
                length / 2,  
                h + 0 - height / 2);
        }
    }
}
