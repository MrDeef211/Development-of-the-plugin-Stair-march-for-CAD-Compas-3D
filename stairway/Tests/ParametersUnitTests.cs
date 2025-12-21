using System.Reflection.Metadata;
using Model;

namespace Tests
{
    public class ParametersUnitTests
    {
        /// <summary>
        /// —оздать класс параметров
        /// </summary>
        /// <returns>—сылка на созданный класс</returns>
        private Parameters Create()
        {
            return new Parameters();
        }

        /// <summary>
        /// ѕроверка инициализации параметрa
        /// </summary>
        [Fact]
        public void CreateOutOfRangeParameter()
        {

            Assert.Throws<Exception>(() =>
            {
                var parameter = new Model.Parameter(ParametersTypes.Height, 8000, 500, 32000);
            });

            Assert.Throws<Exception>(() =>
            {
                var parameter = new Model.Parameter(ParametersTypes.Height, 8000, 500, 0);
            });
        }

        /// <summary>
        /// ѕроверка инициализации словар€ параметров
        /// </summary>
        [Fact]
        public void ConstructorInitializesAllParameters()
        {
            var parameters = Create();

            var dict = parameters.GetParameters();

            Assert.NotNull(dict);
            Assert.True(dict.Count > 0);
            Assert.Contains(ParametersTypes.Height, dict.Keys);
        }

        /// <summary>
        /// ѕроверка передачи параметра
        /// </summary>
        [Theory]
        [InlineData(ParametersTypes.Height, 3200)]
        [InlineData(ParametersTypes.Length, 3200)]
        [InlineData(ParametersTypes.StepProjectionLength, 20)]
        [InlineData(ParametersTypes.StepAmount, 20)]
        [InlineData(ParametersTypes.StepHeight, 160)]
        [InlineData(ParametersTypes.Width, 1000)]
        public void SetParameterChangesValue(ParametersTypes parameter, double value)
        {
            var parameters = Create();

            parameters.SetParameter(parameter, value);

            double currentValue = parameters.GetParameter(parameter);

            Assert.Equal(value, currentValue);
        }

        /// <summary>
        /// ѕроверка передачи неправильного параметра
        /// </summary>
        [Fact]
        public void SetParameterOutOfRangeRaisesErrorEvent()
        {
            var parameters = Create();
            ErrorArgs? error = null;

            parameters.ErrorMessageEvent += (s, e) =>
            {
                error = e;
            };

            parameters.SetParameter(ParametersTypes.Length, 500);
            parameters.SetParameter(ParametersTypes.StepAmount, 2);
            parameters.SetParameter(ParametersTypes.Height, 400); // меньше Min

            Assert.NotNull(error);
            Assert.Contains(ParametersTypes.Height, error.ParametersList);
            Assert.Equal(": параметр не должен выходить за диапазон от " +
                            parameters.GetParameters()[ParametersTypes.Height].Min.ToString() +
                            " до " + parameters.GetParameters()[ParametersTypes.Height].Max.ToString() + " мм", error.Message);
        }

        /// <summary>
        /// ѕроверка передачи неправильного параметра количества ступеней
        /// </summary>
        [Fact]
        public void SetStepAmountOutOfRangeRaisesErrorEvent()
        {
            var parameters = Create();
            ErrorArgs? error = null;

            parameters.ErrorMessageEvent += (s, e) =>
            {
                error = e;
            };

            parameters.SetParameter(ParametersTypes.StepAmount , 100);

            Assert.NotNull(error);
            Assert.Contains(ParametersTypes.StepAmount, error.ParametersList);
        }

        /// <summary>
        /// ѕроверка передачи неправильных параметров выступа
        /// </summary>
        [Fact]
        public void SetStepProjectionOutOfRangeRaisesErrorEvent()
        {
            var parameters = Create();
            ErrorArgs? error = null;

            parameters.ErrorMessageEvent += (s, e) =>
            {
                error = e;
            };
            parameters.SetParameter(ParametersTypes.StepProjectionHeight, 90); 

            Assert.NotNull(error);
            Assert.Contains(ParametersTypes.StepProjectionHeight, error.ParametersList);
        }


        /// <summary>
        /// ѕроверка передачи не целочисленного параметра в "количество ступеней"
        /// </summary>
        [Fact]
        public void StepAmountNotIntegerRaisesError()
        {
            var parameters = Create();
            ErrorArgs? error = null;

            parameters.ErrorMessageEvent += (s, e) => error = e;

            parameters.SetParameter(ParametersTypes.StepAmount, 5.5);

            Assert.NotNull(error);
            Assert.Contains(ParametersTypes.StepAmount, error.ParametersList);
        }

        /// <summary>
        /// ѕроверка  внутреннего вычислени€ высоты ступени
        /// </summary>
        [Fact]
        public void ChangingHeightRecalculatesStepHeight()
        {
            var parameters = Create();

            parameters.SetParameter(ParametersTypes.StepAmount, 10);
            parameters.SetParameter(ParametersTypes.Height, 2000);

            double stepHeight = parameters.GetParameter(ParametersTypes.StepHeight);

            Assert.Equal(200, stepHeight);
        }

        /// <summary>
        /// ѕроверка внутреннего вычислени€ высоты марша
        /// </summary>
        [Fact]
        public void ChangingStepHeightRecalculatesHeight()
        {
            var parameters = Create();

            parameters.SetParameter(ParametersTypes.StepAmount, 10);
            parameters.SetParameter(ParametersTypes.StepHeight, 180);

            double height = parameters.GetParameter(ParametersTypes.Height);

            Assert.Equal(1800, height);
        }

        /// <summary>
        /// ѕроверка внутренней валидации угла марша
        /// </summary>
        [Fact]
        public void StairAngleOutOfRangeRaisesError()
        {
            var parameters = new Parameters();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.Height, 8000);
            parameters.SetParameter(ParametersTypes.Length, 1000);

            Assert.Contains(errors, e =>
                e.ParametersList.Contains(ParametersTypes.Height) &&
                e.ParametersList.Contains(ParametersTypes.Length));
        }

        /// <summary>
        /// ѕроверка обновлени€ всех параметров
        /// </summary>
        [Fact]
        public void FullUpdateParametersSendsAllParameters()
        {
            var parameters = Create();
            List<ParametersTypes>? updated = null;

            parameters.UpdateParametersEvent += (s, list) => updated = list;

            parameters.FullUpdateParameters();

            Assert.NotNull(updated);
            Assert.Equal(parameters.GetParameters().Count, updated.Count);
        }


    }
}