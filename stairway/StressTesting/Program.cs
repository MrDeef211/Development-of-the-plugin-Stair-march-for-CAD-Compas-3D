using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.Devices;
using Builders;
using Model;

/// <summary>
/// Программа для стресс-тестирования построителя модели
/// </summary>
class Program
{
    /// <summary>
    /// Точка входа в программу
    /// </summary>
    static void Main()
    {
        var builder = new Builder();
        var parameters = new Parameters();
        
        var stopWatch = new Stopwatch();
        var currentProcess = Process.GetCurrentProcess();
        var computerInfo = new ComputerInfo();

        using var streamWriter = new StreamWriter("log.txt");

        int count = 0;
        const double gigabyteInByte = 1.0 / 1073741824.0;

        // Для расчёта времени 
        TimeSpan lastTotalProcessorTime = currentProcess.TotalProcessorTime;
        DateTime lastTime = DateTime.Now;

        while (true)
        {
            var inputParameters = parameters.GetParameters();

            var outputParameters =
                new Dictionary<ParametersTypes, double>();

            foreach (var parameter in inputParameters.Keys)
                outputParameters.Add(
                    parameter,
                    inputParameters[parameter].Value);

            stopWatch.Start();
            builder.Build(outputParameters, parameters.IsMultiFlight);
            stopWatch.Stop();

            // ОЗУ
            double usedMemory =
                (computerInfo.TotalPhysicalMemory - 
                computerInfo.AvailablePhysicalMemory) *
                gigabyteInByte;

            // Время
            TimeSpan currentTotalProcessorTime = 
                currentProcess.TotalProcessorTime;
            DateTime currentTime = DateTime.Now;

            lastTotalProcessorTime = currentTotalProcessorTime;
            lastTime = currentTime;

            streamWriter.WriteLine(
                $"{++count}\t" +
                $"{stopWatch.Elapsed:hh\\:mm\\:ss\\.fff}\t" +
                $"{usedMemory:F3}"
            );

            streamWriter.Flush();
            stopWatch.Reset();
        }
    }
}