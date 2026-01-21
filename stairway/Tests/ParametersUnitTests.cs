using Model;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ParametersUnitTests
	{
        //TODO: refactor to property
		private Parameters Create()
		{
			return new Parameters();
		}

		[Test]
		[Description("Проверка инициализации параметра " +
            "вне допустимого диапазона")]
		public void CreateOutOfRangeParameter()
		{
			Assert.Throws<Exception>(() =>
				new Parameter(ParametersTypes.Height, 8000, 500, 32000));

			Assert.Throws<Exception>(() =>
				new Parameter(ParametersTypes.Height, 8000, 500, 0));
		}

        [Test]
        [Description("Проверка создания максимального значения параметра, " +
            "меньше минимального")]
        public void CreateParameterMaxLessThanMinRaisesErrorEvent()
        {
            Assert.Throws<Exception>(() =>
                new Parameter(
                    ParametersTypes.Width,
                    500,
                    1000,
                    600));
        }

        [Test]
        [Description("Проверка передачи максимального значения параметра, " +
            "меньше минимального")]
        public void SetParameterMaxLessThanMinRaisesErrorEvent()
        {
            var parameter = new Parameter(
                ParametersTypes.Width,
                1000,
                500,
                750);
            parameter.Max = 400;
            Assert.That(parameter.Max, Is.EqualTo(500));
        }

        [Test]
		[Description("Проверка инициализации словаря параметров")]
		public void ConstructorInitializesAllParameters()
		{
			var parameters = Create();
			var dict = parameters.GetParameters();

			Assert.That(dict, Is.Not.Null);
			Assert.That(dict.Count, Is.GreaterThan(0));
            Assert.That(dict.All(pair => pair.Key == pair.Value.Name),
                Is.True, "Все ключи должны соответствовать значениям Name");
        }

		[TestCase(ParametersTypes.Height, 3200)]
		[TestCase(ParametersTypes.Length, 3200)]
		[TestCase(ParametersTypes.StepProjectionLength, 20)]
		[TestCase(ParametersTypes.StepAmount, 20)]
		[TestCase(ParametersTypes.StepHeight, 160)]
		[TestCase(ParametersTypes.Width, 1000)]
		[Description("Проверка передачи параметра")]
		public void SetParameterChangesValue(
            ParametersTypes parameter, 
            double value)
		{
			var parameters = Create();

			parameters.SetParameter(parameter, value);
			double currentValue = parameters.GetParameter(parameter);

			Assert.That(currentValue, Is.EqualTo(value));
		}

		[Test]
		[Description("Проверка передачи параметра вне диапазона")]
		public void SetParameterOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.Length, 500);
			parameters.SetParameter(ParametersTypes.StepAmount, 2);
			parameters.SetParameter(ParametersTypes.Height, 400);


            Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.Height));
            Assert.That(error.Message, Is.Not.EqualTo(""));
		}

        [Test]
        [Description("Проверка генерации нескольких ошибок подряд")]
        public void MultipleInvalidParametersRaiseMultipleErrors()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.StepAmount, 100);
            parameters.SetParameter(ParametersTypes.Length, 100);

            Assert.That(errors.Count, Is.GreaterThan(1));
        }

        [Test]
        [Description("Проверка отправки события обновления " +
            "при изменении параметра")]
        public void SetParameterRaisesUpdateEvent()
        {
            var parameters = Create();
            ParametersTypes? updated = null;

            parameters.UpdateParameterErrorsEvent += 
                (s, ParametersTypes) => updated = ParametersTypes;

            parameters.SetParameter(ParametersTypes.Width, 900);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated, Is.EqualTo(ParametersTypes.Width));
        }

        [Test]
        [Description("Проверка изменения максимального выступа " +
            "при изменении высоты ступени")]
        public void ChangingStepHeightUpdatesProjectionLimits()
        {
            var parameters = Create();

            parameters.SetParameter(ParametersTypes.StepHeight, 200);

            var projectionLength =
                parameters.GetParameters()[ParametersTypes.StepProjectionLength];

            var projectionHeight =
                parameters.GetParameters()[ParametersTypes.StepProjectionHeight];

            Assert.That(projectionLength.Max, Is.EqualTo(100));
            Assert.That(projectionHeight.Max, Is.EqualTo(100));
        }

        [Test]
        [Description("Исправление ошибки выступа " +
            "при изменении высоты ступени")]
        public void StepProjectionErrorIsFixedAfterStepHeightChange()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            //TODO: тут и ниже комменты НАД строкой
            parameters.SetParameter(ParametersTypes.StepProjectionLength, 100); // ошибка

            Assert.That(errors, Is.Not.Empty);

            errors.Clear();

            parameters.SetParameter(ParametersTypes.StepHeight, 200); // Max = 100

            Assert.That(errors, Is.Empty);
        }

        [Test]
		[Description("Проверка передачи неправильного количества ступеней")]
		public void SetStepAmountOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.StepAmount, 100);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepAmount));
		}

		[Test]
		[Description("Проверка передачи неправильного выступа ступени")]
		public void SetStepProjectionOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(
                ParametersTypes.StepProjectionHeight, 
                90);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepProjectionHeight));
		}

		[Test]
		[Description("Проверка передачи " +
            "не целочисленного количества ступеней")]
		public void StepAmountNotIntegerRaisesError()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.StepAmount, 5.5);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepAmount));
		}

		[Test]
		[Description("Проверка пересчета высоты ступени " +
            "при изменении высоты")]
		public void ChangingHeightRecalculatesStepHeight()
		{
			var parameters = Create();

			parameters.SetParameter(ParametersTypes.StepAmount, 10);
			parameters.SetParameter(ParametersTypes.Height, 2000);

			double stepHeight = parameters.GetParameter(
                ParametersTypes.StepHeight);

			Assert.That(stepHeight, Is.EqualTo(200));
		}

        [Test]
        [Description("Полная проверка кросс-валидации " +
            "после пересчёта высоты ступени")]
        public void HeightChangeTriggersFullRevalidationChain()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.StepAmount, 15);
            parameters.SetParameter(ParametersTypes.Length, 5000);
            parameters.SetParameter(ParametersTypes.StepProjectionLength, 50);

            errors.Clear();

            parameters.SetParameter(ParametersTypes.Height, 6000);

            // Проверка пересчёта
            Assert.That(
                parameters.GetParameter(ParametersTypes.StepHeight),
                Is.EqualTo(400));

            // Проверка обновления границ выступа
            var projection =
                parameters.GetParameters()[ParametersTypes.StepProjectionLength];

            Assert.That(projection.Max, Is.EqualTo(200));

            // Проверка кросс-валидации угла
            Assert.That(errors, Has.Some.Matches<ErrorArgs>(e =>
                e.ParametersList.Contains(ParametersTypes.Height) &&
                e.ParametersList.Contains(ParametersTypes.Length)));
        }

        [Test]
        [Description("Исправление ошибки высоты ступени после пересчёта")]
        public void StepHeightErrorIsFixedAfterRecalculation()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            // Создаём ошибку
            parameters.SetParameter(ParametersTypes.Length, 12000);
            parameters.SetParameter(ParametersTypes.Height, 8000); // StepHeight = 400 (ошибка)

            Assert.That(errors, Is.Not.Empty);

            errors.Clear();

            // Исправляем
            parameters.SetParameter(ParametersTypes.StepAmount, 50); // StepHeight = 160

            Assert.That(errors, Is.Empty);
        }

        [Test]
		[Description("Проверка пересчета высоты марша " +
            "при изменении высоты ступени")]
		public void ChangingStepHeightRecalculatesHeight()
		{
			var parameters = Create();

			parameters.SetParameter(ParametersTypes.StepAmount, 10);
			parameters.SetParameter(ParametersTypes.StepHeight, 180);

			double height = parameters.GetParameter(ParametersTypes.Height);

			Assert.That(height, Is.EqualTo(1800));
		}

        [Test]
        [Description("Кросс-валидация угла марша " +
            "после пересчёта высоты ступени")]
        public void StepHeightChangeTriggersStairAngleValidation()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.StepAmount, 10);
            parameters.SetParameter(ParametersTypes.Length, 2000);
            parameters.SetParameter(ParametersTypes.StepHeight, 300);

            Assert.That(errors, Has.Some.Matches<ErrorArgs>(e =>
                e.ParametersList.Contains(ParametersTypes.Height) &&
                e.ParametersList.Contains(ParametersTypes.Length)));
        }

        [Test]
        [Description("Исправление ошибки высоты марша " +
            "после изменения высоты ступени")]
        public void HeightErrorIsFixedAfterStepHeightChange()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.StepHeight, 500); // Height = 10000 (ошибка)

            Assert.That(errors, Is.Not.Empty);

            errors.Clear();

            parameters.SetParameter(ParametersTypes.StepHeight, 150); // Height = 1500

            Assert.That(errors, Is.Empty);
        }

        [Test]
        [Description("Проверка валидации глубины проступи")]
        public void StepTreadOutOfRangeRaisesError()
        {
            var parameters = Create();
            ErrorArgs? error = null;

            parameters.ErrorMessageEvent += (s, e) => error = e;

            parameters.SetParameter(ParametersTypes.Length, 1000);
            parameters.SetParameter(ParametersTypes.StepAmount, 2);
            parameters.SetParameter(ParametersTypes.StepProjectionLength, 10);

            Assert.That(error, Is.Not.Null);
            Assert.That(
                error.ParametersList,
                Does.Contain(ParametersTypes.StepAmount)
                    .And.Contain(ParametersTypes.Length)
                    .And.Contain(ParametersTypes.StepProjectionLength));
        }

        [Test]
        [Description("Исправление ошибки глубины проступи после пересчёта")]
        public void StepTreadErrorIsFixedAfterCorrection()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.Height, 2000);
            parameters.SetParameter(ParametersTypes.Length, 2000);
            parameters.SetParameter(ParametersTypes.StepAmount, 10);

            Assert.That(errors, Is.Not.Empty);

            errors.Clear();

            parameters.SetParameter(ParametersTypes.StepAmount, 8); 

            Assert.That(errors, Is.Empty);
        }

        [Test]
        [Description("Проверка ввода допустимой глубины проступи")]
        public void ValidStepTreadDoesNotRaiseError()
        {
            var parameters = Create();
            bool errorRaised = false;

            parameters.ErrorMessageEvent += (s, e) => errorRaised = true;

            parameters.SetParameter(ParametersTypes.Length, 3000);
            parameters.SetParameter(ParametersTypes.StepAmount, 10);

            //Сбрасываем т.к предыдущие значения могли вызвать ошибку
            errorRaised = false;
            parameters.SetParameter(ParametersTypes.StepProjectionLength, 50);

            Assert.That(errorRaised, Is.False);
        }

        [Test]
		[Description("Проверка валидации угла марша")]
		public void StairAngleOutOfRangeRaisesError()
		{
			var parameters = new Parameters();
			var errors = new List<ErrorArgs>();

			parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

			parameters.SetParameter(ParametersTypes.Height, 8000);
			parameters.SetParameter(ParametersTypes.Length, 1000);

			Assert.That(errors, Has.Some.Matches<ErrorArgs>(e =>
				e.ParametersList.Contains(ParametersTypes.Height) &&
				e.ParametersList.Contains(ParametersTypes.Length)));
		}

        [Test]
        [Description("Исправление ошибки угла марша после изменения длины")]
        public void StairAngleErrorIsFixedAfterLengthChange()
        {
            var parameters = Create();
            var errors = new List<ErrorArgs>();

            parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

            parameters.SetParameter(ParametersTypes.Height, 4000);
            parameters.SetParameter(ParametersTypes.Length, 1000); // угол > 50

            Assert.That(errors, Is.Not.Empty);

            errors.Clear();

            parameters.SetParameter(ParametersTypes.Length, 6000); // нормальный угол

            Assert.That(errors, Is.Empty);
        }

        [Test]
        [Description("Проверка ввода допустимого угла марша")]
        public void StairAngleInRangeDoesNotRaiseError()
        {
            var parameters = Create();
            bool errorRaised = false;

            parameters.ErrorMessageEvent += (s, e) => errorRaised = true;

            parameters.SetParameter(ParametersTypes.Height, 3200);
            parameters.SetParameter(ParametersTypes.Length, 5000);

            Assert.That(errorRaised, Is.False);
        }


        [Test]
		[Description("Проверка обновления всех параметров")]
		public void FullUpdateParametersSendsAllParameters()
		{
			var parameters = Create();
			List<ParametersTypes>? updatedErrors = new List<ParametersTypes>();
            List<ParametersTypes>? updatedValues = new List<ParametersTypes>();

			parameters.UpdateParameterErrorsEvent += 
                (s, ParametersTypes) => updatedErrors.Add(ParametersTypes);
            parameters.UpdateParameterValueEvent +=
                (s, ParametersTypes) => updatedValues.Add(ParametersTypes);

			parameters.FullUpdateParameters();

			Assert.That(updatedErrors, Is.Not.Null);
			Assert.That(
                updatedErrors.Count, 
                Is.EqualTo(parameters.GetParameters().Count));

            Assert.That(updatedValues, Is.Not.Null);
            Assert.That(
                updatedValues.Count,
                Is.EqualTo(parameters.GetParameters().Count));
        }

    }
}